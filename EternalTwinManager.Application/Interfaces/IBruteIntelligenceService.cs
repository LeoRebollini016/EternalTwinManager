using EternalTwinManager.Core.Dtos.Brutes.Shared;

namespace EternalTwinManager.Application.Brutes.Interfaces.Services;

public interface IBruteIntelligenceService
{
    Task<LoginResultDto?> GetLoginAsync(string username, string password, CancellationToken ct = default);
}