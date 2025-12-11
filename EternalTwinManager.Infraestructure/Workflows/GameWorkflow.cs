using System.Net;
using EternalTwinManager.Core.Dtos.Brutes.Shared;
using EternalTwinManager.Core.Interfaces.Brutes.Apis;
using EternalTwinManager.Core.Interfaces.Brutes.Ports;
using EternalTwinManager.Core.Interfaces.Shared.Ports;
using EternalTwinManager.Core.Models;
using Microsoft.Extensions.Logging;

namespace EternalTwinManager.Infrastructure.Workflows;

public class GameWorkflow(IEternalHttpClientFactory clients, IBruteApiClient api, IBruteAuthenticator authenticator, ILogger<GameWorkflow> logger) : IGameWorkflow
{
    private readonly IBruteAuthenticator _authenticator = authenticator;
    private readonly IBruteApiClient _api = api;
    private readonly ILogger<GameWorkflow> _logger = logger;
    private readonly IEternalHttpClientFactory _clients = clients;

    public async Task<LoginResultDto?> ExecuteAsync(string username, string pass, CancellationToken ct)
    {
        var cookieContainer = new CookieContainer();

        var client = _clients.CreateClient(allowAutoRedirect: true, cookieContainer);

        using var noRedirectClient = _clients.CreateClient(allowAutoRedirect: false, cookieContainer);

        try
        {
            var preSession = await _authenticator.AuthenticateAsync(client, noRedirectClient, username, pass, ct);

            if (preSession is null || !preSession.IsSuccess)
            {
                _logger.LogWarning("Authentication failed for user {username}", username);
                client.Dispose();
                return null;
            }

            var accountId = preSession.UserId.ToString() ?? string.Empty;
            var token = preSession.Token ?? string.Empty;

            if (string.IsNullOrEmpty(accountId) || string.IsNullOrEmpty(token))
            {
                _logger.LogWarning("Authentication returned invalid data for user {username}", username);
                client.Dispose();
                return null;
            }

            var authUser = await _api.AuthenticateUserAsync(client, accountId, token, ct);
            if (authUser is null)
            {
                _logger.LogWarning("Failed to retrieve authenticated user data for user {username}", username);
                client.Dispose();
                return null;
            }

            var session = new UserSession(client);

            return new LoginResultDto(session, authUser);
        }
         catch (OperationCanceledException)
        {
            _logger.LogInformation("Login cancelado para {username}", username);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for user {username}", username);
            return null;
        }
    }
}