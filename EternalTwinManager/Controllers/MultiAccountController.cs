using EternalTwinManager.Application.Orchestrators;
using EternalTwinManager.Core.Dtos.Brutes.Shared;
using EternalTwinManager.Core.Events;
using System.Text.Json;

namespace EternalTwinManager.WinForms.Controllers;

public class MultiAccountController(MultiAccountOrchestrator orchestrator)
{
    private readonly MultiAccountOrchestrator _orchestrator = orchestrator;

    public List<LoginDto> LoadAccounts(string path)
    {
        var json = File.ReadAllText(path);
        var accounts = JsonSerializer.Deserialize<List<LoginDto>>(json);
        if(accounts is null || accounts.Count == 0)
            throw new ArgumentException("No se encontraron cuentas en el archivo proporcionado.");

        return accounts;
    }

    public async Task RunAsync(List<(string user, string password)> tuples, IProgress<AccountProgressUpdate> progress, CancellationToken cancellationToken)
         => await _orchestrator.RunAsync(tuples, progress, cancellationToken);
}