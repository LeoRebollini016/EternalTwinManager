using EternalTwinManager.Application.Interfaces.Services;
using MediatR;

namespace EternalTwinManager.Application.FeaturesHandlers.Brutes.SetDailyWatched;

public class SetDailyWatchedCommandHandler(IBruteAutomationService bruteAutoService) : IRequestHandler<SetDailyWatchedCommandRequest, bool>
{
    private readonly IBruteAutomationService _bruteAutoService = bruteAutoService;

    public async Task<bool> Handle(SetDailyWatchedCommandRequest request, CancellationToken cancellationToken)
        => await _bruteAutoService.SetDailyWatchedAsync(request.Session, cancellationToken);
}