function registerScrollListener(dotNetHelper, methodName) {
    window.addEventListener('scroll', () => {
        // Obtenemos la altura del documento y la posición actual del scroll
        const scrollPosition = window.scrollY;
        const totalHeight = document.documentElement.scrollHeight - window.innerHeight;

        // Comprobamos si el usuario ha llegado cerca del final de la página (por ejemplo, el 80% o más)
        if (scrollPosition > totalHeight * 0.8) {
            // Llamamos al método de Blazor para cargar más productos
            dotNetHelper.invokeMethodAsync(methodName);
        }
    });
}