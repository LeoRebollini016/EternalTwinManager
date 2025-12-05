namespace EternalTwinManager.Core.Shared.Interfaces.Services;

public interface IPasswordEncoder
{
    string Encode(string password);
}
