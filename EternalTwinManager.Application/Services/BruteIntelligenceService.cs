using EternalTwinManager.Application.Brutes.Interfaces.Services;
using EternalTwinManager.Application.Models;
using EternalTwinManager.Core.Brute.Interfaces.Apis;
using EternalTwinManager.Core.Brute.Interfaces.Services;
using EternalTwinManager.Core.Shared.Models;
using Microsoft.Extensions.Logging;

namespace EternalTwinManager.Application.Services;

public class BruteIntelligenceService(IBruteAuthenticator bruteAuthenticator, IBruteApiClient bruteApiClient, IHttpClientFactory httpFactory, ILogger<BruteIntelligenceService> logger) : IBruteIntelligenceService
{
    private readonly IBruteAuthenticator _bruteAuthenticator = bruteAuthenticator;
    private readonly IBruteApiClient _bruteApiClient = bruteApiClient;
    private readonly IHttpClientFactory _httpFactory = httpFactory;
    private readonly ILogger<BruteIntelligenceService> _logger = logger;

    public async Task<BruteLoginResult?> GetLoginAsync(string username, string password, CancellationToken ct = default)
    {
        var client = _httpFactory.CreateClient("BruteClient");

        try
        {
            var authResult = await _bruteAuthenticator.AuthenticateAsync(client, username, password, randomMode: false, ct);

            if(authResult is null || !authResult.IsSuccess)
            {
                _logger.LogWarning("Authentication failed for user {Username}", username);
                return null;
            }

            var accountId = authResult.AccountData?.UserId.ToString() ?? string.Empty;
            var token = authResult.Token ?? string.Empty;

            if (string.IsNullOrEmpty(accountId) || string.IsNullOrEmpty(token))
            {
                _logger.LogWarning("Authentication returned invalid data for user {Username}", username);
                return null;
            }

            var authUser = await _bruteApiClient.AuthenticateUserAsync(client, accountId, token, ct);

            if(authUser is null)
            {
                _logger.LogWarning("Failed to retrieve authenticated user data for user {Username}", username);
                return null;
            }
            var session = new UserSession(client);

            return new BruteLoginResult(session, authUser);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Login cancelado para {username}", username);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for user {Username}", username);
            return null;
        }
    }
}
