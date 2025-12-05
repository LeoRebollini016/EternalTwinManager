namespace EternalTwinManager.Core.Shared.Models;

public class AuthResult<TAccountData>
{
    public bool IsSuccess { get; set; } = false;
    public string Token { get; set; } = "";
    public string ErrorMessage { get; set; } = "";
    public TAccountData? AccountData { get; set; }
}