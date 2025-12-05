using EternalTwinManager.Core.Brute.Entities;
using EternalTwinManager.Core.Shared.Models;

namespace EternalTwinManager.Core.Brute.Interfaces.Services;

public interface IBruteAuthenticator
{
    Task<AuthResult<BruteAccountData>> AuthenticateAsync(HttpClient client, string user, string password, bool randomMode = false, CancellationToken ct = default);
}
