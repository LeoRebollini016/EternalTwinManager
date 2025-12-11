using EternalTwinManager.Application.FeaturesHandlers.Brutes.Login;
using EternalTwinManager.Application.Orchestrators.Pipelines;
using EternalTwinManager.Core.Enums;
using EternalTwinManager.Core.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EternalTwinManager.Application.Orchestrators;

public class DailyTasksOrchestrator(IMediator mediator, AutoFightPipeline autoFightPipeline, ILogger<DailyTasksOrchestrator> logger)
{
    private readonly IMediator _mediator = mediator;
    private readonly AutoFightPipeline _autoFightPipeline = autoFightPipeline;
    private readonly ILogger<DailyTasksOrchestrator> _logger = logger;

    public async Task RunForAccountAsync(string username, string password, IProgress<AccountProgressUpdate> progress, CancellationToken ct)
    {
        progress.Report(new(username, AccountProcessStateEnum.Starting, null, "Iniciando tareas"));
        _logger.LogInformation("DAILY TASKS PARA {user} ", username);

        var authRequest = new LoginCommandRequest(username, password);
        var auth = await _mediator.Send(authRequest, ct);

        if (auth is null) {
            progress.Report(new(username, AccountProcessStateEnum.LoginFailed, null, "Login falló"));
            _logger.LogWarning("Login falló para {user}, se detienen tareas diarias", username);
            return;
        }
        var user = auth.Account;
        var session = auth.Session;
        progress.Report(new(username, AccountProcessStateEnum.LoginSuccess, null, "Login exitoso"));

        _logger.LogWarning("Brutes instancia hash: {b}", username);

        foreach (var brute in user!.Brutes)
        {
            var bruteName = brute.Name;
            progress.Report(new(username, AccountProcessStateEnum.ProcessingBrute, $"{bruteName}", "Inicio de tareas"));
            _logger.LogInformation("Ejecutando tareas para brute {b}", bruteName);

            int? remaining = null;
            int consecutiveFailures = 0;
            int loginFightsLeft = brute.FightsLeft;
            const int maxConsecutiveFailures = 3;
            const int maxRetriesPerFight = 2;

            while (!ct.IsCancellationRequested)
            {
                bool fightExecuted = false;
                int? newRemaining = null;
                Exception? lastEx = null;

                for (int attempt = 1; attempt <= maxRetriesPerFight; attempt++)
                {
                    try
                    {
                        _logger.LogInformation("Brute {b} → Ejecutando pelea (intento {a}/{m})", bruteName, attempt, maxRetriesPerFight);

                        var result = await _autoFightPipeline.ExecuteAsync(session, username, bruteName, brute.Level, progress, ct);

                        if (result.Executed)
                        {
                            progress.Report(new(username, AccountProcessStateEnum.FightFinished, bruteName,$"Quedan {result.RemainingFights} combates."));
                            fightExecuted = true;
                            newRemaining = result.RemainingFights;
                            break;
                        }

                        await Task.Delay(400, ct);
                    }
                    catch (Exception ex) when (ex is not OperationCanceledException)
                    {
                        lastEx = ex;
                        _logger.LogWarning(ex, "Error en pelea para {b} (intento {i})", bruteName, attempt);
                        await Task.Delay(400, ct);
                    }
                }
                if(!fightExecuted && loginFightsLeft == 0)
                {
                    progress.Report(new(username, AccountProcessStateEnum.FightFinished, bruteName, "Finalizó sus combates."));
                    _logger.LogInformation("Brute {b} → Sin peleas restantes. Finalizando.", bruteName);
                    break;
                }

                if (!fightExecuted)
                {
                    consecutiveFailures++;
                    _logger.LogWarning("Brute {b} → pelea NO ejecutada. Fallos consecutivos: {f}", bruteName, consecutiveFailures);

                    if (consecutiveFailures >= maxConsecutiveFailures)
                    {
                        _logger.LogWarning("Brute {b} → se supera el límite de fallos. Se pasa al siguiente brute.", bruteName);
                        break;
                    }

                    continue;
                }

                consecutiveFailures = 0;

                remaining = newRemaining ?? 0;

                _logger.LogInformation("Brute {b} → pelea completada. Remaining = {r}", bruteName, remaining);

                if (remaining <= 0)
                {
                    _logger.LogInformation("Brute {b} → sin peleas restantes. Finalizando.", bruteName);
                    break;
                }
            }
        }
        progress.Report(new(username, AccountProcessStateEnum.Completed, null, "Finalizó las tareas."));
        _logger.LogInformation("Finalizado daily Tasks para {user}", username);
    }
}
