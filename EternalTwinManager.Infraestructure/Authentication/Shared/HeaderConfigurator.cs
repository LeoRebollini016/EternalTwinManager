using EternalTwinManager.Core.Shared.Interfaces.Services;
using EternalTwinManager.Core.Shared.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace EternalTwinManager.Infraestructure.Authentication.Shared;

public class HeaderConfigurator(ILogger<HeaderConfigurator> logger) : IHeaderConfigurator
{
    private readonly ILogger<HeaderConfigurator> _logger = logger;

    public void SetDefaultHeaders(HttpClient client)
    {
        client.DefaultRequestHeaders.UserAgent.Clear();
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.ParseAdd("application/json, text/plain, */*");

        client.DefaultRequestHeaders.Remove("Origin");
        client.DefaultRequestHeaders.TryAddWithoutValidation("Origin", "https://eternaltwin.org");

        client.DefaultRequestHeaders.Remove("Referer");
        client.DefaultRequestHeaders.TryAddWithoutValidation("Referer", "https://eternaltwin.org/login");
    }
    public void SetAuthHeaders<T>(HttpClient client, AuthResult<T> authResult)
    {
        string userId = string.Empty;

        if(authResult.AccountData is not null)
        {
            var prop = authResult.AccountData.GetType().GetProperty("UserId");
            userId = prop?.GetValue(authResult.AccountData)?.ToString() ?? string.Empty;
        }

        var token = authResult.Token ?? string.Empty;

        if(string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
        {
            _logger.LogWarning("SetAuthHeaders: UserId o Token están vacios.");
        }

        var raw = $"{userId}:{token}";
        var encoded = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(raw));

        var cookieValue = $"user={userId}; token={token}";

        client.DefaultRequestHeaders.Remove("Authorization");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encoded);

        client.DefaultRequestHeaders.Remove("Cookie");
        client.DefaultRequestHeaders.TryAddWithoutValidation("Cookie", cookieValue);
    }
}
