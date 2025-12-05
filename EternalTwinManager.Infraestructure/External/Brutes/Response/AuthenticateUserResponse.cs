namespace EternalTwinManager.Infrastructure.External.Brute.Response;

public record AuthenticateUserResponse(
    string Id,
    string Lang,
    string Name,
    bool Admin,
    bool Moderator,
    string ConnexionToken,
    int BruteLimit,
    int Gold,
    int FightSpeed,
    bool BackgroundMusic,
    DateTime? DinorpgDone,
    List<string>? Ips,
    DateTime? BannedAt,
    string? BanReason,
    bool DisplayVersusPage,
    bool DisplayOpponentDetails,
    DateTime? LastSeen,
    List<AuthenticateBruteResponse>? Brutes,
    List<object>? Following,
    List<object>? Notifications
);
