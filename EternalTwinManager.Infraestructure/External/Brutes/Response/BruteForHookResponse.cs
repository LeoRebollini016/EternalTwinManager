using System.Dynamic;

namespace EternalTwinManager.Infrastructure.External.Brute.Response;

public sealed record BruteForHookResponse(
    Guid Id,
    string Name,
    DateTime? DeletedAt,
    DateTime CreatedAt,
    DateTime? WillBeDeletedAt,
    string? DeletionReason,
    IReadOnlyList<string> DestinyPath,
    IReadOnlyList<string> PreviousDestinyPath,
    int Level,
    int Xp,
    int Hp,
    int EnduranceStat,
    double EnduranceModifier,
    int EnduranceValue,
    int StrengthStat,
    double StrengthModifier,
    int StrengthValue,
    int AgilityStat,
    double AgilityModifier,
    int AgilityValue,
    int SpeedStat,
    double SpeedModifier,
    int SpeedValue,
    int Ranking,
    string Gender,
    Guid UserId,
    string Body,
    string Colors,
    IReadOnlyList<string> Weapons,
    IReadOnlyList<string> Skills,
    IReadOnlyList<string> Pets,
    int Ascensions,
    IReadOnlyList<string>? AscendedWeapons,
    IReadOnlyList<string>? AscendedSkills,
    IReadOnlyList<string>? AscendedPets,
    Guid? MasterId,
    int PupilsCount,
    Guid? ClanId,
    bool RegisteredForTournament,
    DateTime? NextTournamentDate,
    DateTime? CurrentTournamentDate,
    int? CurrentTournamentStepWatched,
    DateTime? GlobalTournamentWatchedDate,
    int? GlobalTournamentRoundWatched,
    DateTime? EventTournamentWatchedDate,
    int? EventTournamentRoundWatched,
    DateTime LastFight,
    int FightsLeft,
    int Victories,
    int Losses,
    DateTime OpponentsGeneratedAt,
    DateTime? CanRankUpSince,
    bool Favorite,
    Guid? WantToJoinClanId,
    int TournamentWins,
    Guid? EventId,
    int Resets,
    MasterInfo? Master,
    ClanInfo Clan,
    UserInfo User,
    IReadOnlyList<TournamentInfo>? Tournaments,
    IReadOnlyList<object>? Inventory
);
public sealed record ClanInfo(Guid Id, string Name);

public sealed record UserInfo(Guid Id, string Name, DateTime LastSeen);

public sealed record MasterInfo(Guid Id, string Name);

public sealed record TournamentInfo(
    Guid Id,
    DateTime Date,
    string Type,
    int Rounds,
    Guid? EventId
);