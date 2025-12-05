namespace EternalTwinManager.Infrastructure.External.Brute.Response;
public record FightResponse(
    Guid Id,
    int XpWon,
    int FightsLeft,
    int Victories,
    int Losses
);