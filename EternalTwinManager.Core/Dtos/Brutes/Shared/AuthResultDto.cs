namespace EternalTwinManager.Core.Dtos.Brutes.Shared;

public record AuthResultDto(
    bool IsSuccess,
    string Token,
    string UserId,
    string? ErrorMessage
);