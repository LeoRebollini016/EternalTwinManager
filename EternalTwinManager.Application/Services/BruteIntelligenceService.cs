using EternalTwinManager.Application.Brutes.Interfaces.Services;
using EternalTwinManager.Core.Dtos.Brutes.Shared;
using EternalTwinManager.Core.Interfaces.Brutes.Ports;

namespace EternalTwinManager.Application.Services;

public class BruteIntelligenceService(IGameWorkflow bruteLogin) : IBruteIntelligenceService
{
    private readonly IGameWorkflow _bruteLogin = bruteLogin;

    public async Task<LoginResultDto?> GetLoginAsync(string username, string password, CancellationToken ct = default)
        => await _bruteLogin.ExecuteAsync(username, password, ct);
}
