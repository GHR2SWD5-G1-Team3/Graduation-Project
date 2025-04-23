//Card and favourite start
console.log("site.js loaded successfully!");
document.addEventListener('DOMContentLoaded', function () {
        updateCartCounter();
        document.querySelectorAll('.favourite-btn').forEach(btn => {
            // Check initial state from server if needed
            btn.addEventListener('click', function (e) {
                e.stopPropagation();
                e.preventDefault();

                this.classList.toggle('active');

                const icon = this.querySelector('i');
                icon.classList.toggle('fa-solid');
                icon.classList.toggle('fa-regular');

                const productId = this.getAttribute('data-product-id');
                fetch(`/Favorite/Toggle?productId=${productId}`, { method: 'POST' });
            });
        });

        document.querySelectorAll('.card-clickable').forEach(card => {
            card.addEventListener('click', function () {
                const url = this.getAttribute('data-url');
                if (url) {
                    window.location.href = url;
                }
            });
        });

        document.querySelectorAll('.add-to-cart-btn').forEach(btn => {
            btn.addEventListener('click', function (e) {
                e.preventDefault();
                e.stopPropagation();

                const productId = parseInt(this.getAttribute('data-product-id'));
                const price = parseFloat(this.getAttribute('data-product-price'));
                const quantity = parseFloat(this.getAttribute('data-product-quantity'));
                const token = document.querySelector('#anti-forgery-form input[name="__RequestVerificationToken"]').value;
                const productName = this.closest('.fruite-item').querySelector('h4').textContent;

                const originalHtml = this.innerHTML;
                this.innerHTML = '<i class="fa fa-spinner fa-spin me-2"></i> Adding...';
                this.disabled = true;

                console.log({ productId, price, quantity });

                fetch('/Cart/AddToCart', {
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
                })
                    .then(response => response.json())
                    .then(data => {
                        this.innerHTML = originalHtml;
                        this.disabled = false;

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
                            // Show SweetAlert for login prompt
                            Swal.fire({
                                position: 'top-end',
                                icon: 'warning',
                                title: data.message,
                                showConfirmButton: true,
                                background: '#f8f9fa',
                                width: '400px'
                            });

                            this.innerHTML = originalHtml;
                            this.disabled = false;
                        }
                    })
                    .catch(error => {
                        console.error('Error:', error);
                        this.innerHTML = originalHtml;
                        this.disabled = false;

                        Swal.fire({
                            position: 'top-end',
                            icon: 'error',
                            title: 'Failed to add to cart',
                            text: 'Please try again',
                            showConfirmButton: false,
                            timer: 2000,
                            toast: true
                        });
                    });
            });
        });
        function updateCartCounter() {
            fetch('/Cart/GetCartItemCount')
                .then(response => response.json())
                .then(data => {
                    const cartCounter = document.querySelector('#cart-counter');
                    if (cartCounter) {
                        cartCounter.textContent = data.count;
                    }
                })
                .catch(error => console.error('Error updating cart counter:', error));
        }


 });

//Card and favourit end
