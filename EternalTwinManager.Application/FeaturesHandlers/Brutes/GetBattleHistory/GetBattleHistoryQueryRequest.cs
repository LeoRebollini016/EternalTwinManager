using EternalTwinManager.Core.Brutes.Dtos;
using EternalTwinManager.Core.Shared.Models;
using MediatR;

namespace EternalTwinManager.Application.FeaturesHandlers.Brutes.GetWinRates;

public record GetBattleHistoryQueryRequest(UserSession Session, IEnumerable<string> BruteNames) : IRequest<Dictionary<string, BruteBattleHistoryDto>>;
