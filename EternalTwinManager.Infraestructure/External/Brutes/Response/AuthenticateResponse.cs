namespace EternalTwinManager.Infrastructure.External.Brute.Response;

public record AuthenticateResponse(
    AuthenticateUserResponse User,
    Dictionary<string, object>? Modifiers,
    AuthenticateCurrentEventResponse? CurrentEvent,
    string Version
);
