namespace SimuladorDistrato.Models;
public class SimulacaoResultado
{
    public int DiasTotaisContrato { get; set; }
    public int DiasRestantesContrato { get; set; }

    public string FormulaMultaDetalhada { get; set; } = string.Empty;

    public double MultaProporcional { get; set; }

    public List<ItemExtrato> ItensExtrato { get; set; } = new List<ItemExtrato>();

    public double SaldoFinal { get; set; }
    public bool LocatarioDevePagar { get; set; }
    
    public string MensagemResponsabilidadePagamento { get; set; } = "";
}