using System.Text.Json;

namespace EternalTwinManager.Core.Interfaces.Brutes.Apis;

public interface IBruteOAuthClient
{
    Task<string> GetCsrfTokenAsync(HttpClient client, CancellationToken ct = default);
    Task<string> GetOAuthCodeAsync(HttpClient client, string csrf, CancellationToken ct = default);
    Task<JsonElement> ExChangeCodeTokenAsync(HttpClient client, string code, CancellationToken ct = default);
}
