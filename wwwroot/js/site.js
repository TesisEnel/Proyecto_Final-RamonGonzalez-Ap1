document.addEventListener('DOMContentLoaded', function () {
    const filterButtons = document.querySelectorAll('.category-filter button');
    filterButtons.forEach(button => {
        button.addEventListener('click', function () {
            filterButtons.forEach(btn => btn.classList.remove('active'));
            this.classList.add('active');
        });
    });

    console.log("Sitio cargado correctamente");
});


function createAccount() {
    alert("Funcionalidad de crear cuenta aún no implementada.");
}

document.getElementById("loginForm").addEventListener("submit", function(event) {
    event.preventDefault();
    alert("Iniciar sesión con: " + document.getElementById("email").value);
});
