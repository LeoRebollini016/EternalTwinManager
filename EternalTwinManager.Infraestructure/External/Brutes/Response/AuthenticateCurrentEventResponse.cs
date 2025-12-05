namespace EternalTwinManager.Infrastructure.External.Brute.Response;

public record AuthenticateCurrentEventResponse(
    string Id,
    DateTime Date,
    string Type,
    int MaxLevel,
    int MaxRound,
    string Status,
    string? WinnerId,
    DateTime? FinishedAt,
    List<string>? SortedBrutes
);
