using EternalTwinManager.Core.Brute.Interfaces.Apis;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;

namespace EternalTwinManager.Infraestructure.Authentication.Brute;

public class BruteOAuthClient(IHttpClientFactory httpFactory, ILogger<BruteOAuthClient> logger) : IBruteOAuthClient
{
    private readonly IHttpClientFactory _httpFactory = httpFactory;
    private readonly ILogger<BruteOAuthClient> _logger = logger;

    public async Task<string> GetCsrfTokenAsync(HttpClient client, CancellationToken ct = default)
    {
        client ??= _httpFactory.CreateClient("BruteClient");

        var response = await client.GetFromJsonAsync<JsonElement>("https://brute.eternaltwin.org/api/csrf", ct);
        if(response.TryGetProperty("csrfToken", out var tokenProp))
        {
            var csrf = tokenProp.GetString() ?? string.Empty;
            client.DefaultRequestHeaders.Remove("X-CSRF-Token");
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-CSRF-Token", csrf);
            return csrf;
        }
        throw new InvalidOperationException("csrfToken no encontrado en respuesta.");
    }
    public async Task<string> GetOAuthCodeAsync(HttpClient loggedClient, CancellationToken ct = default)
    {
        var noRedirectClient = _httpFactory.CreateClient("BruteNoRedirect");

        foreach (var header in loggedClient.DefaultRequestHeaders)
        {
            if (!noRedirectClient.DefaultRequestHeaders.Contains(header.Key))
                noRedirectClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
        }

        var url = "https://eternaltwin.org/oauth/authorize?access_type=offline&response_type=code" +
                  "&redirect_uri=https://brute.eternaltwin.org/oauth/callback" +
                  "&client_id=brute_production@clients&scope=base&state=";

        using var req = new HttpRequestMessage(HttpMethod.Get, url);
        using var response = await noRedirectClient.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, ct);

        var status = (int)response.StatusCode;

        if (status >= 300 && status <= 399)
        {
            var loc = response.Headers.Location;
            if (loc == null)
                throw new InvalidOperationException("OAuth: 302 sin Location.");

            var q = HttpUtility.ParseQueryString(loc.Query);
            var code = q["code"];

            return code ?? throw new InvalidOperationException("OAuth: code ausente.");
        }

        if (status == 200)
        {
            var html = await response.Content.ReadAsStringAsync(ct);

            var match = Regex.Match(html, @"callback\?code=([^""']+)");
            if (match.Success)
                return match.Groups[1].Value;
        }

        throw new InvalidOperationException($"OAuth: status inesperado {status}");
    }
    public async Task<JsonElement> ExChangeCodeTokenAsync(HttpClient client, string code, CancellationToken ct = default)
    {
        client ??= _httpFactory.CreateClient("BruteClient");

        var endpoint = $"https://brute.eternaltwin.org/api/oauth/token?code={Uri.EscapeDataString(code)}";
        var json = await client.GetFromJsonAsync<JsonElement>(endpoint, ct);
        return json;
    }
}
