document.addEventListener('DOMContentLoaded', function () {
    // Initialize all favorite buttons
    document.querySelectorAll('.favourite-btn').forEach(btn => {
        const icon = btn.querySelector('i');
        if (btn.classList.contains('active')) {
            icon.className = 'fa-solid fa-heart';
            icon.style.color = '#dc3545'; // Red fill
            icon.style.webkitTextStroke = '1.5px #dc3545';
        } else {
            icon.className = 'fa-regular fa-heart';
            icon.style.color = 'white'; // White fill
            icon.style.webkitTextStroke = '1.5px #dc3545';
        }
    });

    // Click handler
    document.addEventListener('click', async function (e) {
        const btn = e.target.closest('.favourite-btn');
        if (!btn) return;

        e.preventDefault();
        e.stopPropagation();

        const icon = btn.querySelector('i');
        const productId = btn.dataset.productId;
        const wasActive = btn.classList.contains('active');

        // Visual feedback
        btn.disabled = true;
        icon.className = 'fa fa-spinner fa-spin';
        icon.style.color = '';
        icon.style.webkitTextStroke = '';

        try {
            const response = await fetch(`/Wishlist/Toggle?productId=${productId}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': document.querySelector('[name="__RequestVerificationToken"]')?.value
                }
            });

            const result = await response.json();
            if (result.success) {
                btn.classList.toggle('active');
                if (result.isFavorite) {
                    icon.className = 'fa-solid fa-heart';
                    icon.style.color = '#dc3545';
                    icon.style.webkitTextStroke = '1.5px #dc3545';
                    showSweetAlert('Added to favorites!');
                } else {
                    icon.className = 'fa-regular fa-heart';
                    icon.style.color = 'white';
                    icon.style.webkitTextStroke = '1.5px #dc3545';
                    showSweetAlert('Removed from favorites');
                }
            }
        } catch (error) {
            console.error('Error:', error);
            icon.className = wasActive ? 'fa-solid fa-heart' : 'fa-regular fa-heart';
            icon.style.color = wasActive ? '#dc3545' : 'white';
            icon.style.webkitTextStroke = '1.5px #dc3545';
            showSweetAlert('Operation failed', 'error');
        } finally {
            btn.disabled = false;
        }
    });
});

function showSweetAlert(title, icon = 'success') {
    Swal.fire({
        position: 'top-end',
        icon: icon,
        title: title,
        showConfirmButton: false,
        timer: 1500,
        toast: true,
        background: '#f8f9fa',
        backdrop: false
    });
}