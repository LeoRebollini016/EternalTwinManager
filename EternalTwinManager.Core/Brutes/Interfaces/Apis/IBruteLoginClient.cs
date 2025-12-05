namespace EternalTwinManager.Core.Brute.Interfaces.Apis;

public interface IBruteLoginClient
{
    Task LoginAsync(HttpClient client, string username, string encodedPassword, CancellationToken ct = default);
}
