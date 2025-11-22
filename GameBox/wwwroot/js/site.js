// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// User dropdown toggle
document.addEventListener('DOMContentLoaded', function () {
    const userGreeting = document.querySelector('.user-greeting');
    const userDropdown = document.getElementById('user-dropdown');

    if (userGreeting && userDropdown) {
        // Toggle dropdown on click
        userGreeting.addEventListener('click', function (e) {
            e.stopPropagation();
            userDropdown.classList.toggle('show');
        });

        // Close dropdown when clicking outside
        document.addEventListener('click', function (e) {
            if (!userDropdown.contains(e.target) && !userGreeting.contains(e.target)) {
                userDropdown.classList.remove('show');
            }
        });
    }
});
