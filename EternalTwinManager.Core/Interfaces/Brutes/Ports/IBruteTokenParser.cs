using EternalTwinManager.Core.Dtos.Brutes.Shared;
using System.Text.Json;

namespace EternalTwinManager.Core.Interfaces.Brutes.Ports;

public interface IBruteTokenParser
{
    AuthResultDto Parse(JsonElement json);
}
