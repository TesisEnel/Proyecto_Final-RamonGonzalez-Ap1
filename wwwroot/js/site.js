document.addEventListener('DOMContentLoaded', function () {
    const filterButtons = document.querySelectorAll('.category-filter button');
    filterButtons.forEach(button => {
        button.addEventListener('click', function () {
            filterButtons.forEach(btn => btn.classList.remove('active'));
            this.classList.add('active');
        });
    });

document.addEventListener('DOMContentLoaded', function () {
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
};

window.registerScrollListener = (dotnetHelper, methodName) => {
    window.addEventListener('scroll', () => {
        if (window.innerHeight + window.scrollY >= document.body.offsetHeight - 500) {
            dotnetHelper.invokeMethodAsync(methodName);
        }
    });
};