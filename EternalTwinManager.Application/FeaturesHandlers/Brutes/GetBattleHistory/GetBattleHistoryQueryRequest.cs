using EternalTwinManager.Core.Dtos.Brutes.Battles;
using EternalTwinManager.Core.Models;
using MediatR;

namespace EternalTwinManager.Application.FeaturesHandlers.Brutes.GetWinRates;

public record GetBattleHistoryQueryRequest(UserSession Session, IEnumerable<string> BruteNames) : IRequest<Dictionary<string, BruteBattleHistoryDto>>;
