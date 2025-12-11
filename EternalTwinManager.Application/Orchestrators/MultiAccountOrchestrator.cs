using EternalTwinManager.Core.Events;
using Microsoft.Extensions.Logging;

namespace EternalTwinManager.Application.Orchestrators;

public class MultiAccountOrchestrator(DailyTasksOrchestrator daily, ILogger<MultiAccountOrchestrator> logger)
{
    private readonly DailyTasksOrchestrator _daily = daily;
    private readonly ILogger<MultiAccountOrchestrator> _logger = logger;
    private readonly SemaphoreSlim _semaphore = new(10);

    public async Task RunAsync(IEnumerable<(string user, string pass)> accounts, IProgress<AccountProgressUpdate> progress, CancellationToken ct)
    {
        var tasks = accounts.Select(async account =>
        {
            try
            {
                await RunAccountAsync(account.user, account.pass, progress, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar daily tasks para la cuenta {user}", account.user[..3] + "***");
            }
        });

        await Task.WhenAll(tasks);

        _logger.LogInformation("Finalizadas daily tasks para todas las cuentas");
    }
    private async Task RunAccountAsync(string user, string pass, IProgress<AccountProgressUpdate> progress, CancellationToken ct)
    {
        await _semaphore.WaitAsync(ct);
        try
        {
            _logger.LogInformation("Iniciando daily tasks para la cuenta {user}", user[..3] + "***");

            await _daily.RunForAccountAsync(user, pass, progress, ct);

            _logger.LogInformation("Finalizadas daily tasks para la cuenta {user}", user[..3]+ "***");
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
