using EternalTwinManager.Application.Interfaces.Services;
using EternalTwinManager.Core.Dtos.Brutes.Auth;
using EternalTwinManager.Core.Dtos.Brutes.Battles;
using EternalTwinManager.Core.Interfaces.Brutes.Apis;
using EternalTwinManager.Core.Models;

namespace EternalTwinManager.Application.Services;

public class BruteAutomationService(IBruteApiClient bruteApi) : IBruteAutomationService
{
    private readonly IBruteApiClient _bruteApi = bruteApi;

    public Task<AuthUserDto?> AuthenticateUserAsync(HttpClient session, string accountId, string accessToken, CancellationToken ct)
        => _bruteApi.AuthenticateUserAsync(session, accountId, accessToken, ct);
    public Task<Dictionary<string, BruteBattleHistoryDto>> GetBattleHistoryAsync(UserSession session, IEnumerable<string> bruteNames, CancellationToken ct)
        => _bruteApi.GetBattleHistoryAsync(session, bruteNames, ct);

    public Task<IEnumerable<OpponentDto>> GetOpponentsAsync(UserSession session, string bruteName, int level, CancellationToken ct)
        => _bruteApi.GetOpponentsAsync(session, bruteName, level, ct);

    public Task<bool> SetDailyWatchedAsync(UserSession session, CancellationToken ct)
        => _bruteApi.SetDailyWatchedAsync(session, ct);

    public Task<FightDto> StartFightAsync(UserSession session, string bruteName, string opponentName, CancellationToken ct)
        => _bruteApi.StartFightAsync(session, bruteName, opponentName, ct);
}