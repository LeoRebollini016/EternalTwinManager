namespace EternalTwinManager.Infrastructure.External.Brute.Response;

public record OpponentResponse(
    Guid Id,
    string Name,
    int Ranking,
    int Level,
    string Gender,
    int Hp,
    int EnduranceStat,
    double EnduranceModifier,
    int EnduranceValue,
    int StrengthStat,
    double StrengthModifier,
    int StrengthValue,
    int SpeedStat,
    double SpeedModifier,
    int SpeedValue,
    int AgilityStat,
    double AgilityModifier,
    int AgilityValue,
    string? DeletedAt,
    string Body,
    string Colors,
    IReadOnlyList<string> Skills,
    IReadOnlyList<string> Weapons,
    IReadOnlyList<string> Pets,
    string? EventId
);
