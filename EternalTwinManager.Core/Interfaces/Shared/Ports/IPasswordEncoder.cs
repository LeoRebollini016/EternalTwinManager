namespace EternalTwinManager.Core.Interfaces.Shared.Ports;

public interface IPasswordEncoder
{
    string Encode(string password);
}
