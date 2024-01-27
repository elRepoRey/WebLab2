document.addEventListener('DOMContentLoaded', function () {
    fetch('navbar.html')
        .then(response => response.text())
        .then(html => {
            document.getElementById('navbar-container').innerHTML = html;
        })
        .then(() => {
            const menuToggle = document.getElementById('menu-toggle');
            if (menuToggle) {
                menuToggle.addEventListener('click', function () {
                    document.getElementById('mobile-menu').classList.toggle('hidden');
                });
            }
        })
        .catch(error => console.error('Error loading the navbar:', error));

    fetch('footer.html')
        .then(response => response.text())
        .then(html => document.getElementById('footer-container').innerHTML = html)
        .catch(error => console.error('Error loading the footer:', error));
});
