// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {

    const camposMoeda = document.querySelectorAll(".campo-moeda");

    camposMoeda.forEach(campo => {

        const hiddenId = campo.dataset.hidden;
        const hidden = document.getElementById(hiddenId);

        campo.addEventListener("input", function () {

            let somenteNumeros = this.value.replace(/\D/g, "");

            if (!somenteNumeros) {
                this.value = "";
                hidden.value = "";
                return;
            }

            const valorNumerico = Number(somenteNumeros) / 100;

            this.value = valorNumerico.toLocaleString("pt-BR", {
                style: "currency",
                currency: "BRL"
            });

            hidden.value = valorNumerico.toFixed(2);
        });

    });

});