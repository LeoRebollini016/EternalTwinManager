using EternalTwinManager.Core.Brute.Dtos.Auth;
using EternalTwinManager.Core.Shared.Models;

namespace EternalTwinManager.Application.Models;

public record BruteLoginResult(
    UserSession Session,
    AuthUserDto AuthUser
);
