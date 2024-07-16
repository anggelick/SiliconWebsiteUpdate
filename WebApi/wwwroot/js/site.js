

const toggleMenu = () => {
    document.getElementById('navigation').classList.toggle('hidden');
    document.getElementById('btn-switch').classList.toggle('hidden');
    document.getElementById('account-buttons').classList.toggle('hidden');

    // Switching icon on the button
    document.getElementById('btn-mobile-icon').classList.toggle('fa-bars');
    document.getElementById('btn-mobile-icon').classList.toggle('fa-x');

    document.getElementById('header-menu').classList.toggle('mobile-toggled');
}

const checkScreenSize = () => {

    if (window.innerWidth >= 992) {
        if (document.getElementById('header-menu').classList.contains('mobile-toggled')) {
            toggleMenu()
        }
    }
};

window.addEventListener('resize', checkScreenSize);