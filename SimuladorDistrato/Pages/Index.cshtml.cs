using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimuladorDistrato.Models;
using SimuladorDistrato.Services;

namespace SimuladorDistrato.Pages;

public class IndexModel : PageModel
{
    private readonly ISimulacaoService _simulacaoService;

    public IndexModel(ISimulacaoService simulacaoService)
    {
        _simulacaoService = simulacaoService;
    }

    [BindProperty]
    public SimulacaoDados Input { get; set; } = new();

    public SimulacaoResultado? Resultado { get; set; }

    public string? MensagemErro { get; set; }

    public void OnGet()
    {
        DateTime hoje = DateTime.Today;

        Input.DataInicioContrato = hoje;
        Input.DataFimContrato = hoje;
        Input.DataDesocupacao = hoje;
    }

    public void OnPost()
    {
        if (!ModelState.IsValid)
            return;

        try
        {
            Resultado = _simulacaoService.Calcular(Input);
        }
        catch (ArgumentException ex)
        {
            MensagemErro = ex.Message;
        }
    }
}