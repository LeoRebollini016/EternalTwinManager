using EternalTwinManager.Core.Interfaces.Brutes.Apis;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;

namespace EternalTwinManager.Infraestructure.Authentication.Brute;

public class BruteOAuthClient() : IBruteOAuthClient
{
    public async Task<string> GetCsrfTokenAsync(HttpClient client, CancellationToken ct = default)
    {
        var response = await client.GetFromJsonAsync<JsonElement>("https://brute.eternaltwin.org/api/csrf", ct);
        if(response.TryGetProperty("csrfToken", out var tokenProp))
            return tokenProp.GetString() ?? string.Empty;

        throw new InvalidOperationException("csrfToken no encontrado en respuesta.");
    }
    public async Task<string> GetOAuthCodeAsync(HttpClient client, string csrf, CancellationToken ct = default)
    {

        var url = "https://eternaltwin.org/oauth/authorize?access_type=offline&response_type=code" +
                  "&redirect_uri=https://brute.eternaltwin.org/oauth/callback" +
                  "&client_id=brute_production@clients&scope=base&state=";

        var req = new HttpRequestMessage(HttpMethod.Get, url);
        using var response = await client.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, ct);

        var status = (int)response.StatusCode;

        if (status >= 300 && status <= 399)
        {
            var loc = response.Headers.Location;
            if (loc == null)
                throw new InvalidOperationException("OAuth: 302 sin Location.");

            Uri final = loc.IsAbsoluteUri
               ? loc
               : new Uri(new Uri("https://eternaltwin.org"), loc);

            var q = HttpUtility.ParseQueryString(final.Query);
            var code = q["code"];

            return code ?? "";
        }

        if (status == 200)
        {
            var requestUri = response.RequestMessage?.RequestUri;
            if (requestUri != null)
            {
                var q = HttpUtility.ParseQueryString(requestUri.Query);
                var codeFromUri = q["code"];
                if (!string.IsNullOrEmpty(codeFromUri))
                    return codeFromUri;
            }

            // 2) Fallback: buscar en el HTML (por compatibilidad)
            var html = await response.Content.ReadAsStringAsync(ct);
            var match = Regex.Match(html, @"callback\?code=([^""'&]+)");
            if (match.Success)
                return match.Groups[1].Value;
        }

        throw new InvalidOperationException($"OAuth: status inesperado {status}");
    }
    public async Task<JsonElement> ExChangeCodeTokenAsync(HttpClient client, string code, CancellationToken ct = default)
    {
        var endpoint = $"https://brute.eternaltwin.org/api/oauth/token?code={Uri.EscapeDataString(code)}";
        return await client.GetFromJsonAsync<JsonElement>(endpoint, ct);
    }
}
