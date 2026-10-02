document.addEventListener("DOMContentLoaded", function () {

    const camposMoeda = document.querySelectorAll(".campo-moeda");

    camposMoeda.forEach(campo => {

        const hiddenId = campo.dataset.hidden;
        const hidden = document.getElementById(hiddenId);

        if (hidden && hidden.value) {

            const valorInicial = parseFloat(
                hidden.value.replace(",", ".")
            );

            if (!isNaN(valorInicial)) {
                campo.value = valorInicial.toLocaleString("pt-BR", {
                    style: "currency",
                    currency: "BRL"
                });
            }
        }

        if (hidden.value !== "") {

            const valorInicial = Number(hidden.value);

            if (!isNaN(valorInicial)) {
                campo.value = valorInicial.toLocaleString("pt-BR", {
                    style: "currency",
                    currency: "BRL"
                });
            }
        }

        campo.addEventListener("input", function () {

            const numeros = this.value.replace(/\D/g, "");

            if (numeros === "") {
                this.value = "";
                hidden.value = "";
                return;
            }

            const centavos = parseInt(numeros, 10);

            const valor = centavos / 100;

            this.value = valor.toLocaleString("pt-BR", {
                style: "currency",
                currency: "BRL"
            });

            hidden.value = valor.toFixed(2);
        });

    });

});