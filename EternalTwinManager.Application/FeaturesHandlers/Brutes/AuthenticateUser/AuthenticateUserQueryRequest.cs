using EternalTwinManager.Core.Dtos.Brutes.Auth;
using MediatR;

namespace EternalTwinManager.Application.FeaturesHandlers.Brutes.AuthenticateUser;

public record AuthenticateUserQueryRequest(HttpClient Session, string AccountId, string AccessToken) : IRequest<AuthUserDto?>;
