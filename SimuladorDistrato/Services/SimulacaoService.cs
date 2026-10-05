using SimuladorDistrato.Models;

namespace SimuladorDistrato.Services;

public class SimulacaoService : ISimulacaoService
{
    public SimulacaoResultado Calcular(SimulacaoDados dadosSimulacao)
    {
        int diasTotaisContrato = (dadosSimulacao.DataFimContrato - dadosSimulacao.DataInicioContrato).Days;

        if (diasTotaisContrato <= 0)
            throw new ArgumentException("O período do contrato é inválido.");

        int diasRestantesContrato = (dadosSimulacao.DataFimContrato - dadosSimulacao.DataDesocupacao).Days;
        if (diasRestantesContrato < 0) diasRestantesContrato = 0;

        double multaProporcional = Math.Round((dadosSimulacao.MultaContratual / diasTotaisContrato) * diasRestantesContrato, 2);

        var itens = new List<ItemExtrato>();

        if (multaProporcional > 0)
        {
            itens.Add(new ItemExtrato
            {
                Descricao = $"Multa Rescisória Proporcional ({diasRestantesContrato} dias restantes)",
                Valor = multaProporcional,
                EhDebito= true
            });
        }

        if (dadosSimulacao.OutrosDebitos > 0)
        {
            itens.Add(new ItemExtrato
            {
                Descricao = "Outros Débitos",
                Valor = dadosSimulacao.OutrosDebitos,
                EhDebito = true
            });
        }

        if (dadosSimulacao.ValorCaucao > 0)
        {
            itens.Add(new ItemExtrato
            {
                Descricao = "Caução Retida",
                Valor = dadosSimulacao.ValorCaucao,
                EhDebito = false
            });
        }

        if (dadosSimulacao.OutrosCreditos > 0)
        {
            itens.Add(new ItemExtrato
            {
                Descricao = "Outros Créditos",
                Valor = dadosSimulacao.OutrosCreditos,
                EhDebito = false
            });
        }

        double saldoFinal = Math.Round(multaProporcional + dadosSimulacao.OutrosDebitos - dadosSimulacao.ValorCaucao - dadosSimulacao.OutrosCreditos, 2);

        string mensagem;
        if (saldoFinal > 0)
        {
            mensagem = $"O LOCATÁRIO deve pagar ao LOCADOR o valor de {saldoFinal:C2}.";
        }
        else if (saldoFinal < 0)
        {
            mensagem = $"O LOCADOR deve devolver/pagar ao LOCATÁRIO o valor de {Math.Abs(saldoFinal):C2}.";
        }
        else
        {
            mensagem = "NÃO HÁ VALORES PENDENTES entre as partes (Saldo zerado).";
        }

        return new SimulacaoResultado
        {
            DiasTotaisContrato = diasTotaisContrato,
            DiasRestantesContrato = diasRestantesContrato,
            MultaProporcional = multaProporcional,
            ItensExtrato = itens,
            SaldoFinal = saldoFinal,
            FormulaMultaDetalhada = $"Multa Proporcional = (R$ {dadosSimulacao.MultaContratual:N2} / {diasTotaisContrato} dias) × {diasRestantesContrato} dias = R$ {multaProporcional:N2}",
            LocatarioDevePagar = saldoFinal > 0,
            MensagemResponsabilidadePagamento = mensagem
        };
    }
}