using EternalTwinManager.Core.Dtos.Brutes.Shared;
using EternalTwinManager.Core.Interfaces.Shared;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Text;

namespace EternalTwinManager.Infraestructure.Authentication.Shared;

public class HeaderConfigurator(ILogger<HeaderConfigurator> logger) : IHeaderConfigurator
{
    private readonly ILogger<HeaderConfigurator> _logger = logger;

    public void SetDefaultHeaders(HttpClient client)
    {
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");
        client.DefaultRequestHeaders.Accept.ParseAdd("application/json, text/plain, */*");

        client.DefaultRequestHeaders.TryAddWithoutValidation("Origin", "https://eternaltwin.org");
        client.DefaultRequestHeaders.TryAddWithoutValidation("Referer", "https://eternaltwin.org/login");
    }
    public void SetAuthHeaders(HttpClient client, AuthResultDto authResult)
    {
        string userId = authResult.UserId ?? string.Empty;
        var token = authResult.Token ?? string.Empty;

        if(string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            _logger.LogWarning("SetAuthHeaders: UserId o Token están vacios.");

        var raw = $"{userId}:{token}";
        var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(raw));
        var cookieValue = $"user={userId}; token={token}";

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encoded);
        client.DefaultRequestHeaders.TryAddWithoutValidation("Cookie", cookieValue);
    }
    public void SetCsrf(HttpClient client, string csrfToken)
    {
        client.DefaultRequestHeaders.Remove("X-Csrf-Token");
        client.DefaultRequestHeaders.TryAddWithoutValidation("X-Csrf-Token", csrfToken);
    }
}
