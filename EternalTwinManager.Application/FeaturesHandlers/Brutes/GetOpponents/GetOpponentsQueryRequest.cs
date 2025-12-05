using EternalTwinManager.Core.Brute.Dtos;
using EternalTwinManager.Core.Shared.Models;
using MediatR;

namespace EternalTwinManager.Application.FeaturesHandlers.Brutes.GetOpponents;

public record GetOpponentsQueryRequest(UserSession Session, string BruteName, int Level) : IRequest<IEnumerable<OpponentDto>>;
