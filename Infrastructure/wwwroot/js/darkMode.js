function toggleDarkTheme() {

    var element = document.body;
    element.classList.toggle('dark');

    const isDarkMode = element.classList.contains('dark');
    document.getElementById('switch-mode').checked = isDarkMode;

    localStorage.setItem('darkMode', isDarkMode);

    var imgElement = document.getElementById('silicon-logo-img');
    if (isDarkMode) {
        imgElement.src = '/images/silicone-logo-dark_theme.svg';
    } else {
        imgElement.src = '/images/silicone-logo-light_theme.svg';
    }
}

const darkModeFromStorage = localStorage.getItem('darkMode') === 'true';

var element = document.body;
element.classList.toggle('dark', darkModeFromStorage);
document.getElementById('switch-mode').checked = darkModeFromStorage;

var imgElement = document.getElementById('silicon-logo-img');
if (darkModeFromStorage) {
    imgElement.src = '/images/silicone-logo-dark_theme.svg';
} else {
    imgElement.src = '/images/silicone-logo-light_theme.svg';
}