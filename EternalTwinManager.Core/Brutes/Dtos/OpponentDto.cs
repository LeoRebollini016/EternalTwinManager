namespace EternalTwinManager.Core.Brute.Dtos;

public record OpponentDto(
    string Name,
    int Level,
    int Hp,
    int EnduranceValue,
    int StrengthValue,
    int SpeedValue,
    int AgilityValue,
    IReadOnlyList<string> Skills,
    IReadOnlyList<string> Weapons,
    IReadOnlyList<string> Pets
);
