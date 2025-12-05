using EternalTwinManager.Application.Interfaces.Services;
using EternalTwinManager.Core.Brutes.Dtos;
using MediatR;

namespace EternalTwinManager.Application.FeaturesHandlers.Brutes.GetWinRates;

public class GetBattleHistoryQueryHandler(IBruteAutomationService bruteAutoService) : IRequestHandler<GetBattleHistoryQueryRequest, Dictionary<string, BruteBattleHistoryDto>>
{
    private readonly IBruteAutomationService _bruteAutoService = bruteAutoService;

    public async Task<Dictionary<string, BruteBattleHistoryDto>> Handle(GetBattleHistoryQueryRequest request, CancellationToken cancellationToken)
        => await _bruteAutoService.GetBattleHistoryAsync(request.Session, request.BruteNames, cancellationToken);
}
