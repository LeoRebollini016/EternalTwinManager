using EternalTwinManager.Application.FeaturesHandlers.Brutes.GetOpponents;
using EternalTwinManager.Application.FeaturesHandlers.Brutes.GetWinRates;
using EternalTwinManager.Application.FeaturesHandlers.Brutes.StartFight;
using EternalTwinManager.Application.Helpers;
using EternalTwinManager.Core.Dtos.Brutes.Battles;
using EternalTwinManager.Core.Enums;
using EternalTwinManager.Core.Events;
using EternalTwinManager.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EternalTwinManager.Application.Orchestrators.Pipelines;

public class AutoFightPipeline(IMediator mediator, ILogger<AutoFightPipeline> logger)
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<AutoFightPipeline> _logger = logger;

    public async Task<FightDto> ExecuteAsync(UserSession session, string userName, string bruteName, int level, IProgress<AccountProgressUpdate> progress, CancellationToken ct)
    {
        progress.Report(new(userName, AccountProcessStateEnum.SearchingOpponents, bruteName, "Buscando oponentes."));
        _logger.LogInformation("AutoFightPipeline iniciado para {brute}", bruteName);

        var opponentRequest = new GetOpponentsQueryRequest(session, bruteName, level);
        var opponents = await _mediator.Send(opponentRequest, ct);

        if (opponents == null || !opponents.Any())
        {
            progress.Report(new(userName, AccountProcessStateEnum.Error, bruteName, "No se encontró oponentes."));
            _logger.LogWarning("No hay oponentes para {brute}", bruteName);

            return new FightDto(false, null);
        }
        progress.Report(new(userName, AccountProcessStateEnum.SelectingWorstOpponent, bruteName, "Seleccionando oponente más débil."));
        var opponentsList = opponents.Select(o => o.Name).ToList();

        var battleHistoryRequest = new GetBattleHistoryQueryRequest(session, opponentsList);
        Dictionary<string, BruteBattleHistoryDto> battleHistoryOpponents = await _mediator.Send(battleHistoryRequest, ct);

        var winRateOpponents = CalculateWinRateHelper.WinRateHelper(battleHistoryOpponents);
        var weakest = winRateOpponents.MinBy(kvp => kvp.Value);

        _logger.LogInformation("Oponente elegido: {name} win rate: {wr}", weakest.Key, weakest.Value);

        progress.Report(new(userName, AccountProcessStateEnum.Fighting, bruteName, $"Combatiendo contra {weakest.Key}."));
        var fightRequest = new StartFightCommandRequest(session, bruteName, weakest.Key);
        return await _mediator.Send(fightRequest, ct);
    }
}
