// El cálculo de importes del formulario de reserva se encuentra en la vista Create.
document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll("[data-confirm]").forEach(element => {
        element.addEventListener("click", event => {
            if (!confirm(element.dataset.confirm)) event.preventDefault();
        });
    });
});
