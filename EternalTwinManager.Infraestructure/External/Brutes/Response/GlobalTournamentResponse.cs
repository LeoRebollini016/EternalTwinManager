namespace EternalTwinManager.Infrastructure.External.Brute.Response;

public sealed record GlobalTournamentResponse(
    TournamentData Tournament,
    IReadOnlyList<object> LastRounds,
    bool Done,
    object? NextOpponent
);

public sealed record TournamentData(
    Guid Id,
    int Rounds,
    string Type,
    IReadOnlyList<object> Fights
);