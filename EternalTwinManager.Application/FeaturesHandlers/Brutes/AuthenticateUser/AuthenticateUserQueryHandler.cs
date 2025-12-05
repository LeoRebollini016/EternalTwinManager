using EternalTwinManager.Application.Interfaces.Services;
using EternalTwinManager.Core.Brute.Dtos.Auth;
using MediatR;

namespace EternalTwinManager.Application.FeaturesHandlers.Brutes.AuthenticateUser;

public class AuthenticateUserQueryHandler(IBruteAutomationService bruteAutoService) : IRequestHandler<AuthenticateUserQueryRequest, AuthUserDto?>
{
    private readonly IBruteAutomationService _bruteAutoService = bruteAutoService;
    public async Task<AuthUserDto?> Handle(AuthenticateUserQueryRequest request, CancellationToken cancellationToken)
        => await _bruteAutoService.AuthenticateUserAsync(request.Session, request.AccountId, request.AccessToken, cancellationToken);
}
