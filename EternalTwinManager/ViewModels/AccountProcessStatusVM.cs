using EternalTwinManager.Core.Enums;

namespace EternalTwinManager.WinForms.ViewModels;

public record AccountProcessStatusVM
(
    int Id,
    string AccountName,
    string CreatureName,
    AccountProcessStateEnum State,
    string ErrorMessage,
    DateTime LastUpdate
);