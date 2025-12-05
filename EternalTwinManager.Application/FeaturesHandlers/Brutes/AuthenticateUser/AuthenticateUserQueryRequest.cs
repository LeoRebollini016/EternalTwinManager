using EternalTwinManager.Core.Brute.Dtos.Auth;
using EternalTwinManager.Core.Shared.Models;
using MediatR;

namespace EternalTwinManager.Application.FeaturesHandlers.Brutes.AuthenticateUser;

public record AuthenticateUserQueryRequest(HttpClient Session, string AccountId, string AccessToken) : IRequest<AuthUserDto?>;
