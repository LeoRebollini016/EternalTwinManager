using EternalTwinManager.Application.Models;

namespace EternalTwinManager.Application.Brutes.Interfaces.Services;

public interface IBruteIntelligenceService
{
    Task<BruteLoginResult?> GetLoginAsync(string username, string password, CancellationToken ct = default);
}