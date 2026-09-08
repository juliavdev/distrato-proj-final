using SimuladorDistrato.Models;

namespace SimuladorDistrato.Services;

public class SimulacaoService : ISimulacaoService
{
    public SimulacaoResultado Calcular(SimulacaoDados dadosSimulacao)
    {
        int diasTotaisContrato =
            (dadosSimulacao.DataFimContrato - dadosSimulacao.DataInicioContrato).Days;

        int diasRestantesContrato =
            (dadosSimulacao.DataFimContrato - dadosSimulacao.DataDesocupacao).Days;

        if (diasTotaisContrato <= 0)
            throw new ArgumentException("O período do contrato é inválido.");

        if (diasRestantesContrato < 0)
            diasRestantesContrato = 0;

        double multaProporcional =
            dadosSimulacao.MultaContratual / diasTotaisContrato
            * diasRestantesContrato;

        double saldoFinal =
            multaProporcional
            + dadosSimulacao.OutrosDebitos
            - dadosSimulacao.ValorCaucao
            - dadosSimulacao.OutrosCreditos;

        return new SimulacaoResultado
        {
            DiasTotaisContrato = diasTotaisContrato,
            DiasRestantesContrato = diasRestantesContrato,
            MultaProporcional = Math.Round(multaProporcional, 2),
            SaldoFinal = Math.Round(saldoFinal, 2)
        };
    }
}