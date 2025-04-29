
// Cart and favorite functionality
console.log("site.js loaded successfully!");

document.addEventListener('DOMContentLoaded', function () {
    updateCartCounter();

    // Modified click handler with proper event propagation control
    document.body.addEventListener('click', async function (e) {
        // Handle Add to Cart button clicks
        const cartBtn = e.target.closest('.add-to-cart-btn');
        if (cartBtn) {
            e.preventDefault();
            e.stopImmediatePropagation(); // Crucial for preventing card click

            const productId = parseInt(cartBtn.getAttribute('data-product-id'));
            const price = parseFloat(cartBtn.getAttribute('data-product-price'));
            const quantity = parseInt(cartBtn.getAttribute('data-product-quantity')) || 1;
            const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;

            // Find product name - works with multiple product card structures
            const productCard = cartBtn.closest('.fruite-item, .border-primary, .rounded, .p-4');
            const productName = productCard ? productCard.querySelector('h4')?.textContent : 'Product';

            const originalHtml = cartBtn.innerHTML;
            cartBtn.innerHTML = '<i class="fa fa-spinner fa-spin me-2"></i> Adding...';
            cartBtn.disabled = true;

            try {
                const response = await fetch('/Cart/AddToCart', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken': token
                    },
                    body: JSON.stringify({
                        productId,
                        price,
                        quantity
                    })
                });

                const data = await response.json();
                if (data.success) {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: `${productName} added to cart!`,
                        showConfirmButton: false,
                        timer: 2000,
                        toast: true,
                        background: '#f8f9fa',
                        backdrop: false,
                        width: '400px'
                    });
                    updateCartCounter();
                } else {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'warning',
                        title: data.message,
                        showConfirmButton: true,
                        background: '#f8f9fa',
                        width: '400px'
                    });
                }
            } catch (error) {
                console.error('Error:', error);
                Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: 'Failed to add to cart',
                    text: 'Please try again or login first',
                    showConfirmButton: false,
                    timer: 2000,
                    toast: true
                });
            } finally {
                cartBtn.innerHTML = originalHtml;
                cartBtn.disabled = false;
            }
            return; // Exit early to prevent card click handling
        }

        // Handle product card clicks (for navigation)
        const card = e.target.closest('.card-clickable');
        if (card && !e.target.closest('.add-to-cart-btn, .favourite-btn')) {
            const url = card.getAttribute('data-url');
            if (url) {
                window.location.href = url;
            }
        }
    });

    // Cart counter update function
    async function updateCartCounter() {
        try {
            const response = await fetch('/Cart/GetCartItemCount');
            const data = await response.json();
            const cartCounter = document.querySelector('#cart-counter');
            if (cartCounter) cartCounter.textContent = data.count;
        } catch (error) {
            console.error('Error updating cart counter:', error);
        }
    }
});
