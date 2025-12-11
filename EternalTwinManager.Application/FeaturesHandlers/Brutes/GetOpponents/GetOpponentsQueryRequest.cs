using EternalTwinManager.Core.Dtos.Brutes.Battles;
using EternalTwinManager.Core.Models;
using MediatR;

namespace EternalTwinManager.Application.FeaturesHandlers.Brutes.GetOpponents;

public record GetOpponentsQueryRequest(UserSession Session, string BruteName, int Level) : IRequest<IEnumerable<OpponentDto>>;
