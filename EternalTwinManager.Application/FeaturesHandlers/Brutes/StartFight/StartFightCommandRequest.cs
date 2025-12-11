using EternalTwinManager.Core.Dtos.Brutes.Battles;
using EternalTwinManager.Core.Models;
using MediatR;

namespace EternalTwinManager.Application.FeaturesHandlers.Brutes.StartFight;

public record StartFightCommandRequest(UserSession Session, string bruteName, string opponentName) : IRequest<FightDto>;
