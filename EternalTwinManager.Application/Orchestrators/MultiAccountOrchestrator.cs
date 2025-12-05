using Microsoft.Extensions.Logging;

namespace EternalTwinManager.Application.Orchestrators;

public class MultiAccountOrchestrator(DailyTasksOrchestrator daily, ILogger<MultiAccountOrchestrator> logger)
{
    private readonly DailyTasksOrchestrator _daily = daily;
    private readonly ILogger<MultiAccountOrchestrator> _logger = logger;

    public async Task RunAsync(IEnumerable<(string user, string pass)> accounts, CancellationToken ct)
    {
        foreach (var (user, pass) in accounts)
        {
            _logger.LogInformation("Iniciando daily tasks para la cuenta {user}", user);
            await _daily.RunFourAccountAsync(user, pass, ct);
        }
        _logger.LogInformation("Finalizadas daily tasks para todas las cuentas");
    }
}
