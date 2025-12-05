namespace EternalTwinManager.Core.Shared.Models;
public sealed class UserSession
{
    public HttpClient HttpClient { get; }

    public UserSession(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }
}
