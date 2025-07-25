document.addEventListener('DOMContentLoaded', function () {
    // Ejemplo: Cambiar clase activa en filtros
    const filterButtons = document.querySelectorAll('.category-filter button');
    filterButtons.forEach(button => {
        button.addEventListener('click', function () {
            filterButtons.forEach(btn => btn.classList.remove('active'));
            this.classList.add('active');
        });
    });

    // La función focusSearchInput ha sido eliminada, ya que no se necesita actualmente.

    console.log("Sitio cargado correctamente");
});