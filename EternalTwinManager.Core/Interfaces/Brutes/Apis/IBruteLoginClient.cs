namespace EternalTwinManager.Core.Interfaces.Brutes.Apis;

public interface IBruteLoginClient
{
    Task LoginAsync(HttpClient client, string username, string encodedPassword, CancellationToken ct = default);
}
