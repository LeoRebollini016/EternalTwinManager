using EternalTwinManager.Core.Brute.Entities;
using EternalTwinManager.Core.Shared.Models;
using System.Text.Json;

namespace EternalTwinManager.Core.Brute.Interfaces.Services;

public interface IBruteTokenParser
{
    AuthResult<BruteAccountData> Parse(JsonElement json);
}
