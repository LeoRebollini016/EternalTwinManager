namespace EternalTwinManager.Core.Dtos.Brutes.Auth;

public record AuthBruteDto(
    string Id,
    string Name,
    int Level,
    int Ranking,
    int FightsLeft,
    int Victories,
    int Losses,
    bool Favorite,
    string Gender,
    string Body,
    string Colors
);