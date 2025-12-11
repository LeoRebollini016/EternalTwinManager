using EternalTwinManager.Core.Models;
using MediatR;

namespace EternalTwinManager.Application.FeaturesHandlers.Brutes.SetDailyWatched;

public record SetDailyWatchedCommandRequest(UserSession Session) : IRequest<bool>;
