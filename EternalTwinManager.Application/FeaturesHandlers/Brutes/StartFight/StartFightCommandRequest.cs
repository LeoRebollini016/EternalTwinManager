using EternalTwinManager.Core.Brutes.Dtos;
using EternalTwinManager.Core.Shared.Models;
using MediatR;

namespace EternalTwinManager.Application.FeaturesHandlers.Brutes.StartFight;

public record StartFightCommandRequest(UserSession Session, string bruteName, string opponentName) : IRequest<FightDto>;
