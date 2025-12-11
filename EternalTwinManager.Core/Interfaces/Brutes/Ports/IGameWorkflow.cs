using EternalTwinManager.Core.Dtos.Brutes.Shared;

namespace EternalTwinManager.Core.Interfaces.Brutes.Ports;

public interface IGameWorkflow
{
    Task<LoginResultDto?> ExecuteAsync(string username, string pass, CancellationToken ct);
}
