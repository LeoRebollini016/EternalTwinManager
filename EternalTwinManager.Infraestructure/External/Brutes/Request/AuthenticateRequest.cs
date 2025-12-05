namespace EternalTwinManager.Infrastructure.External.Brutes.Request;

public record AuthenticateRequest(
    string login,
    string token
);
