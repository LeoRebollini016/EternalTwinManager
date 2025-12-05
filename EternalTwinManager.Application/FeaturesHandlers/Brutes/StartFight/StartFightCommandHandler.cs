using EternalTwinManager.Application.Interfaces.Services;
using EternalTwinManager.Core.Brutes.Dtos;
using MediatR;

namespace EternalTwinManager.Application.FeaturesHandlers.Brutes.StartFight;

public class StartFightCommandHandler(IBruteAutomationService bruteAutoService) : IRequestHandler<StartFightCommandRequest, FightDto>
{
    private readonly IBruteAutomationService _bruteAutoService = bruteAutoService;

    public async Task<FightDto> Handle(StartFightCommandRequest request, CancellationToken cancellationToken)
        => await _bruteAutoService.StartFightAsync(request.Session, request.bruteName, request.opponentName, cancellationToken);
}
