using EternalTwinManager.Core.Dtos.Brutes.Shared;
using EternalTwinManager.Core.Interfaces.Brutes.Apis;
using EternalTwinManager.Core.Interfaces.Brutes.Ports;
using EternalTwinManager.Core.Interfaces.Shared;
using EternalTwinManager.Core.Interfaces.Shared.Ports;
using Microsoft.Extensions.Logging;

namespace EternalTwinManager.Infraestructure.Authentication.Brute;

public class BruteAuthenticator(IBruteLoginClient loginClient, IBruteOAuthClient oAuthClient, IBruteTokenParser tokenParser,
    IPasswordEncoder passwordEncoder, IHeaderConfigurator headerConfigurator, ILogger<BruteAuthenticator> logger) : IBruteAuthenticator
{
    private readonly IBruteLoginClient _loginClient = loginClient;
    private readonly IBruteOAuthClient _oAuthClient = oAuthClient;
    private readonly IBruteTokenParser _token_parser = tokenParser;
    private readonly IPasswordEncoder _passwordEncoder = passwordEncoder;
    private readonly IHeaderConfigurator _headerConfigurator = headerConfigurator;
    private readonly ILogger<BruteAuthenticator> _logger = logger;

    public async Task<AuthResultDto> AuthenticateAsync(HttpClient client, HttpClient noRedirectClient, string user, string password, CancellationToken ct = default)
    {
        _logger.LogInformation("Autenticando {user} (Brute)...", user);

        _headerConfigurator.SetDefaultHeaders(client);
        _headerConfigurator.SetDefaultHeaders(noRedirectClient);

        string code;
        {
            var csrf = await _oAuthClient.GetCsrfTokenAsync(noRedirectClient, ct);
            _headerConfigurator.SetCsrf(noRedirectClient, csrf);
            _headerConfigurator.SetCsrf(client, csrf);

            var encoded = _passwordEncoder.Encode(password);

            await _loginClient.LoginAsync(client, user, encoded, ct);

            code = await _oAuthClient.GetOAuthCodeAsync(noRedirectClient, csrf, ct);
        }

        if (code == string.Empty)
        {
            var csrfnew = await _oAuthClient.GetCsrfTokenAsync(noRedirectClient, ct);
            _headerConfigurator.SetCsrf(noRedirectClient, csrfnew);
            _headerConfigurator.SetCsrf(client, csrfnew);
            code = await _oAuthClient.GetOAuthCodeAsync(noRedirectClient, csrfnew, ct);
        }

        var crsf = await _oAuthClient.GetCsrfTokenAsync(client, ct);
        _headerConfigurator.SetCsrf(client, crsf);

        var tokenJson = await _oAuthClient.ExChangeCodeTokenAsync(client, code, ct);

        var auth = _token_parser.Parse(tokenJson);

        if (!auth.IsSuccess)
        {
            _logger.LogWarning("Auth parsed failed: {err}", auth.ErrorMessage);
            return auth;
        }

        _headerConfigurator.SetAuthHeaders(client, auth);

        _logger.LogInformation("Autenticación completada para {user}. UserId={id}", user, auth.UserId);

        return auth;
    }
}
