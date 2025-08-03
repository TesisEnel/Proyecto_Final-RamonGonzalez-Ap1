// Ruta: wwwroot/js/site.js

document.addEventListener('DOMContentLoaded', function () {
    // ... tu código existente ...
    console.log("Sitio cargado correctamente");
});

function createAccount() {
    alert("Funcionalidad de crear cuenta aún no implementada.");
}

document.getElementById("loginForm").addEventListener("submit", function (event) {
    event.preventDefault();
    alert("Iniciar sesión con: " + document.getElementById("email").value);
});

window.cartFunctions = {
    // ... tu código de carrito existente ...
};

// 🎉 Solución: Agrega esta nueva función
window.registerScrollListener = (dotnetHelper, methodName) => {
    window.addEventListener('scroll', () => {
        // La condición para detectar que el usuario se acerca al final de la página
        if (window.innerHeight + window.scrollY >= document.body.offsetHeight - 500) {
            dotnetHelper.invokeMethodAsync(methodName);
        }
    });
};