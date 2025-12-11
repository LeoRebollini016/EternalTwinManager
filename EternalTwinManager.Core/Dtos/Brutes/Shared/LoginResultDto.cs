using EternalTwinManager.Core.Dtos.Brutes.Auth;
using EternalTwinManager.Core.Models;

namespace EternalTwinManager.Core.Dtos.Brutes.Shared;

public record LoginResultDto(
    UserSession Session,
    AuthUserDto Account
);