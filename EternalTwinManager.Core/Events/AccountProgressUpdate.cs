using EternalTwinManager.Core.Enums;

namespace EternalTwinManager.Core.Events;

public record AccountProgressUpdate
(
    string AccountName,
    AccountProcessStateEnum State,
    string? CreatureName,
    string? Message = null
);