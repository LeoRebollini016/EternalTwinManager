using EternalTwinManager.Core.Brute.Interfaces.Apis;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace EternalTwinManager.Infraestructure.Authentication.Brute;

public class BruteLoginClient(IHttpClientFactory httpClientFactory, ILogger<BruteLoginClient> logger) : IBruteLoginClient
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly ILogger<BruteLoginClient> _logger = logger;
    
    public async Task LoginAsync(HttpClient client, string username, string encodedPassword, CancellationToken ct = default)
    {
        client ??= _httpClientFactory.CreateClient("BruteClient");

        var payload = new { login = username.ToLowerInvariant(), password = encodedPassword };

        using var res = await client.PutAsJsonAsync("https://eternaltwin.org/api/v1/auth/self?method=Etwin", payload, ct);
        res.EnsureSuccessStatusCode();
        _logger.LogInformation("Brute login successful for user {Username}", username);
    }
}
