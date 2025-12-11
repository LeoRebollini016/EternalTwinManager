using EternalTwinManager.Application.Brutes.Interfaces.Services;
using EternalTwinManager.Application.FeaturesHandlers.Brutes.Login;
using EternalTwinManager.Core.Dtos.Brutes.Shared;
using MediatR;

namespace EternalTwinManager.Application.FeaturesHandlers.Brutes.GetLogin;

public class LoginCommandHandler(IBruteIntelligenceService bruteIntelService) : IRequestHandler<LoginCommandRequest, LoginResultDto?>
{
    private readonly IBruteIntelligenceService _bruteIntelService = bruteIntelService;

    public async Task<LoginResultDto?> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        => await _bruteIntelService.GetLoginAsync(request.Username, request.Password, cancellationToken);
}
