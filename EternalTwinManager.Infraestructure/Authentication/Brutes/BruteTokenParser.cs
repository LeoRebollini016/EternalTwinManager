using EternalTwinManager.Core.Brute.Entities;
using EternalTwinManager.Core.Brute.Interfaces.Services;
using EternalTwinManager.Core.Shared.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace EternalTwinManager.Infraestructure.Authentication.Brute;

public class BruteTokenParser(ILogger<BruteTokenParser> logger) : IBruteTokenParser
{
    private readonly ILogger<BruteTokenParser> _logger = logger;

    public AuthResult<BruteAccountData> Parse(JsonElement json)
    {
        try
        {
            string token = string.Empty;
            string userId = string.Empty;
            int gold = 0;

            if (json.ValueKind == JsonValueKind.Object && json.TryGetProperty("user", out var userObj))
            {
                if (userObj.TryGetProperty("connexionToken", out var t)) token = t.GetString() ?? string.Empty;
                if (userObj.TryGetProperty("id", out var id)) userId = id.GetString() ?? string.Empty;
                if (userObj.TryGetProperty("gold", out var g) && g.ValueKind == JsonValueKind.Number) gold = g.GetInt32();
            }
            else
            {
                if (json.TryGetProperty("connexionToken", out var t)) token = t.GetString() ?? string.Empty;
                if (json.TryGetProperty("id", out var id)) userId = id.GetString() ?? string.Empty;
                if (json.TryGetProperty("gold", out var g) && g.ValueKind == JsonValueKind.Number) gold = g.GetInt32();
            }
            var accountData = new BruteAccountData
            {
                UserId = userId,
                Gold = gold
            };

            return new AuthResult<BruteAccountData>
            {
                IsSuccess = true,
                Token = token,
                AccountData = accountData
            };
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Error parseando token JSON");

            return new AuthResult<BruteAccountData>
            {
                IsSuccess = false,
                ErrorMessage = ex.Message
            };
        }
    }
}
