using EternalTwinManager.Application.Interfaces.Services;
using EternalTwinManager.Core.Dtos.Brutes.Battles;
using MediatR;

namespace EternalTwinManager.Application.FeaturesHandlers.Brutes.GetOpponents;

public class GetOpponentsQueryHandler(IBruteAutomationService bruteAutoService) : IRequestHandler<GetOpponentsQueryRequest, IEnumerable<OpponentDto>>
{
    private readonly IBruteAutomationService _bruteAutoService = bruteAutoService;

    public async Task<IEnumerable<OpponentDto>> Handle(GetOpponentsQueryRequest request, CancellationToken cancellationToken)
        => await _bruteAutoService.GetOpponentsAsync(request.Session, request.BruteName, request.Level, cancellationToken);
}