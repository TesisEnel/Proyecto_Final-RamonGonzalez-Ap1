function registerScrollListener(dotNetHelper, methodName) {
    window.addEventListener('scroll', () => {
        const scrollPosition = window.scrollY;
        const totalHeight = document.documentElement.scrollHeight - window.innerHeight;

        if (scrollPosition > totalHeight * 0.8) {
            dotNetHelper.invokeMethodAsync(methodName);
        }
    });
}