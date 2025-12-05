using EternalTwinManager.Core.Brute.Entities;
using EternalTwinManager.Core.Brute.Interfaces.Apis;
using EternalTwinManager.Core.Brute.Interfaces.Services;
using EternalTwinManager.Core.Shared.Interfaces.Services;
using EternalTwinManager.Core.Shared.Models;
using Microsoft.Extensions.Logging;

namespace EternalTwinManager.Infraestructure.Authentication.Brute;

public class BruteAuthenticator(IBruteLoginClient loginClient, IBruteOAuthClient oAuthClient, IBruteTokenParser tokenParser,
    IPasswordEncoder passwordEncoder, IHeaderConfigurator headerConfigurator, IHttpClientFactory httpFactory, ILogger<BruteAuthenticator> logger) : IBruteAuthenticator
{
    private readonly IBruteLoginClient _loginClient = loginClient;
    private readonly IBruteOAuthClient _oAuthClient = oAuthClient;
    private readonly IBruteTokenParser _tokenParser = tokenParser;
    private readonly IPasswordEncoder _passwordEncoder = passwordEncoder;
    private readonly IHeaderConfigurator _headerConfigurator = headerConfigurator;
    private readonly IHttpClientFactory _httpFactory = httpFactory;
    private readonly ILogger<BruteAuthenticator> _logger = logger;

    public async Task<AuthResult<BruteAccountData>> AuthenticateAsync(HttpClient client, string user, string password, bool randomMode = false, CancellationToken ct = default)
    {
        client ??= _httpFactory.CreateClient("BruteClient");

        _logger.LogInformation("Autenticando {user} (Brute)...", user);

        _headerConfigurator.SetDefaultHeaders(client);

        await _oAuthClient.GetCsrfTokenAsync(client, ct);

        var encoded = _passwordEncoder.Encode(password);

        await _loginClient.LoginAsync(client, user, encoded, ct);

        string code = await _oAuthClient.GetOAuthCodeAsync(client, ct);

        await _oAuthClient.GetCsrfTokenAsync(client, ct);

        var tokenJson = await _oAuthClient.ExChangeCodeTokenAsync(client, code, ct);

        var auth = _tokenParser.Parse(tokenJson);

        if (!auth.IsSuccess)
        {
            _logger.LogWarning("Auth parsed failed: {err}", auth.ErrorMessage);
            return auth;
        }
        _headerConfigurator.SetAuthHeaders(client, auth);

        _logger.LogInformation("Autenticación completada para {user}. UserId={id}", user, auth.AccountData?.UserId);

        return auth;
    }
}
