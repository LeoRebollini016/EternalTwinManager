using System.Text.Json;

namespace EternalTwinManager.Core.Brute.Interfaces.Apis;

public interface IBruteOAuthClient
{
    Task<string> GetCsrfTokenAsync(HttpClient client, CancellationToken ct = default);
    Task<string> GetOAuthCodeAsync(HttpClient client, CancellationToken ct = default);
    Task<JsonElement> ExChangeCodeTokenAsync(HttpClient client, string code, CancellationToken ct = default);
}
