using EternalTwinManager.Core.Shared.Interfaces.Services;
using System.Text;

namespace EternalTwinManager.Infraestructure.Authentication.Shared;

public class PasswordEncoder: IPasswordEncoder
{
    public string Encode(string password)
    {
        if (password is null) return string.Empty;

        var bytes = Encoding.UTF8.GetBytes(password);
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }
}
