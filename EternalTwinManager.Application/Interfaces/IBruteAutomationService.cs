using EternalTwinManager.Core.Dtos.Brutes.Auth;
using EternalTwinManager.Core.Dtos.Brutes.Battles;
using EternalTwinManager.Core.Models;

namespace EternalTwinManager.Application.Interfaces.Services;

public interface IBruteAutomationService
{
    Task<AuthUserDto?> AuthenticateUserAsync(HttpClient session, string accountId, string accessToken, CancellationToken ct);
    Task<IEnumerable<OpponentDto>> GetOpponentsAsync(UserSession session,string bruteName, int level, CancellationToken ct);
    Task<FightDto> StartFightAsync(UserSession session, string bruteName, string opponentName, CancellationToken ct);
    Task<bool> SetDailyWatchedAsync(UserSession session, CancellationToken ct);
    Task<Dictionary<string, BruteBattleHistoryDto>> GetBattleHistoryAsync(UserSession session, IEnumerable<string> bruteNames, CancellationToken ct);
}