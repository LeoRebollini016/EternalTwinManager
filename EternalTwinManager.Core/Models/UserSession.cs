namespace EternalTwinManager.Core.Models;
public sealed class UserSession
{
    public HttpClient HttpClient { get; }

    public UserSession(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }
}
