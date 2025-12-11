using EternalTwinManager.Core.Dtos.Brutes.Shared;

namespace EternalTwinManager.Core.Interfaces.Brutes.Ports;

public interface IBruteAuthenticator
{
    Task<AuthResultDto> AuthenticateAsync(HttpClient client, HttpClient noRedirectClient, string user, string password, CancellationToken ct = default);
}
