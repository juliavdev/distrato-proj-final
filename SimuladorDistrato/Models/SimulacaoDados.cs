namespace SimuladorDistrato.Models;

public class SimulacaoDados
{
    public DateTime DataInicioContrato { get; set; }

    public DateTime DataFimContrato { get; set; }

    public DateTime DataDesocupacao { get; set; }

    public double MultaContratual { get; set; }

    public double ValorCaucao { get; set; }

    public double OutrosDebitos { get; set; }

    public double OutrosCreditos { get; set; }
}