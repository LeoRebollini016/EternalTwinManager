using EternalTwinManager.Application.Brutes.Interfaces.Services;
using EternalTwinManager.Application.FeaturesHandlers.Brutes.Login;
using EternalTwinManager.Application.Models;
using MediatR;

namespace EternalTwinManager.Application.FeaturesHandlers.Brutes.GetLogin;

public class LoginCommandHandler(IBruteIntelligenceService bruteIntelService) : IRequestHandler<LoginCommandRequest, BruteLoginResult?>
{
    private readonly IBruteIntelligenceService _bruteIntelService = bruteIntelService;

    public async Task<BruteLoginResult?> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        => await _bruteIntelService.GetLoginAsync(request.Username, request.Password, cancellationToken);
}
