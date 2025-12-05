using EternalTwinManager.Core.Brute.Dtos;
using EternalTwinManager.Core.Brute.Dtos.Auth;
using EternalTwinManager.Core.Brute.Interfaces.Apis;
using EternalTwinManager.Core.Brutes.Dtos;
using EternalTwinManager.Core.Shared.Models;
using EternalTwinManager.Infrastructure.External.Brute.Mappers;
using EternalTwinManager.Infrastructure.External.Brute.Request;
using EternalTwinManager.Infrastructure.External.Brute.Response;
using EternalTwinManager.Infrastructure.External.Brutes.Mappers;
using EternalTwinManager.Infrastructure.External.Brutes.Request;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace EternalTwinManager.Infraestructure.ApiClients;

public class BruteApiClient(ILogger<BruteApiClient> logger) : IBruteApiClient
{
    private readonly ILogger<BruteApiClient> _logger = logger;
    public async Task<AuthUserDto?> AuthenticateUserAsync(HttpClient httpClient, string accountId, string accessToken, CancellationToken ct)
    {
        var _httpClient = httpClient;
        var authenticateRequest = new AuthenticateRequest(accountId, accessToken);

        _logger.LogInformation("Authenticating user with: {AccountId}", accountId);

        var response = await _httpClient.PostAsJsonAsync("user/authenticate?", authenticateRequest, ct);

        if(!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("Failed to authenticate user with accountId: {AccountId}. Status Code: {StatusCode}", accountId, response.StatusCode);
            return null;
        }

        var authResponse = await response.Content.ReadFromJsonAsync<AuthenticateResponse>(cancellationToken: ct);

        return authResponse!.ToDto();
    }

    public async Task<IEnumerable<OpponentDto>> GetOpponentsAsync(UserSession session, string bruteName, int level, CancellationToken ct)
    {
        var httpClient = session.HttpClient;
        var response = await httpClient.GetAsync($"brute/{bruteName}/get-opponents/{level}?", ct);
        response.EnsureSuccessStatusCode();

        var opponents = await response.Content.ReadFromJsonAsync<List<OpponentResponse>>(cancellationToken: ct);

        return opponents?.Select(o => o.ToDto()).ToList() ?? new List<OpponentDto>();
    }

    public async Task<Dictionary<string, BruteBattleHistoryDto>> GetBattleHistoryAsync(UserSession session, IEnumerable<string> bruteNames, CancellationToken ct)
    {
        var httpClient = session.HttpClient;

        const int maxParallel = 3;
        using var semaphore = new SemaphoreSlim(maxParallel, maxParallel);

        var results = new Dictionary<string, BruteBattleHistoryDto>();

        var tasks = bruteNames.Select(async bruteName =>
        {
            await semaphore.WaitAsync(ct);
            try
            {
                var url = $"brute/{Uri.EscapeDataString(bruteName)}/for-hook";
                HttpResponseMessage resp;

                try
                {
                    resp = await httpClient.GetAsync(url, ct).ConfigureAwait(false);
                }
                catch (OperationCanceledException) { throw; }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Error fetching win rate for brute: {BruteName}", bruteName);
                    results[bruteName] = new BruteBattleHistoryDto(Victories: 0, Losses: 0);
                    return;
                }
                if (!resp.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed to fetch win rate for brute: {BruteName}. Status Code: {StatusCode}", bruteName, resp.StatusCode);
                    results[bruteName] = new BruteBattleHistoryDto(Victories: 0, Losses: 0);
                    return;
                }

                var wr = await resp.Content.ReadFromJsonAsync<BruteForHookResponse>(cancellationToken: ct);

                if(wr is null)
                {
                    _logger.LogWarning("Received null response for brute: {BruteName}", bruteName);
                    results[bruteName] = new BruteBattleHistoryDto(Victories: 0, Losses: 0);
                    return;
                }

                results[bruteName] = new BruteBattleHistoryDto(wr.Victories, wr.Losses);
            }
            finally
            {
                semaphore.Release();
            }
        });

        await Task.WhenAll(tasks).ConfigureAwait(false);

        return results.ToDictionary(k => k.Key, v => v.Value);
    }
    public async Task<bool> SetDailyWatchedAsync(UserSession session, CancellationToken ct)
    {
        var httpClient = session.HttpClient;
        var response = await httpClient.PatchAsync("fight?watched=true", null, ct);

        return response.IsSuccessStatusCode;
    }
    
    public async Task<FightDto> StartFightAsync(UserSession session, string bruteName, string opponentName, CancellationToken ct)
    {
        var httpClient = session.HttpClient;

        var payload = new FightRequest(bruteName, opponentName);

        var response = await httpClient.PatchAsJsonAsync("fight", payload, ct);
        response.EnsureSuccessStatusCode();

        var fightResponse = await response.Content.ReadFromJsonAsync<FightResponse>(cancellationToken: ct);

        return fightResponse!.ToDto();
    }
}
