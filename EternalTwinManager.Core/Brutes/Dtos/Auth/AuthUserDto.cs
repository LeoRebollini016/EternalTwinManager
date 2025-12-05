namespace EternalTwinManager.Core.Brute.Dtos.Auth;

public record AuthUserDto(
    string UserId,
    string UserName,
    string ConnexionToken,
    int Gold,
    int BruteLimit,
    DateTime? LastSeen,
    IReadOnlyList<AuthBruteDto> Brutes
);
