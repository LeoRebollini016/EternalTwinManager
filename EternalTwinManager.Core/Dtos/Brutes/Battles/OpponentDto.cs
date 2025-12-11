namespace EternalTwinManager.Core.Dtos.Brutes.Battles;

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
