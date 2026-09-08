using SimuladorDistrato.Models;

namespace SimuladorDistrato.Services;

public interface ISimulacaoService
{
    SimulacaoResultado Calcular(SimulacaoDados input);
}