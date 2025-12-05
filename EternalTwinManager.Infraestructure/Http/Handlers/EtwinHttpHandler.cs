using Microsoft.Extensions.Logging;

namespace EternalTwinManager.Infrastructure.Http.Handlers;

public class EtwinHttpHandler : DelegatingHandler
{
    private readonly ILogger<EtwinHttpHandler> _logger;
    private readonly bool _randomMode;
    private readonly TimeSpan _requestDelay;
    private readonly TimeSpan _errorDelay;

    public EtwinHttpHandler(ILogger<EtwinHttpHandler> logger, bool randomMode = false)
    {
        _logger = logger;
        _randomMode = randomMode;
        _requestDelay = randomMode ? TimeSpan.FromMilliseconds(500) : TimeSpan.FromSeconds(1);
        _errorDelay = randomMode ? TimeSpan.FromMilliseconds(1200) : TimeSpan.FromSeconds(3);
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        await Task.Delay(_requestDelay, cancellationToken);

        try
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode)
                return response;

            if ((int)response.StatusCode is 429 or >= 500)
            {
                _logger.LogWarning($"Retry por error de servidor {response.StatusCode}, aplicando delay...");
                await Task.Delay(_errorDelay, cancellationToken);

                return await base.SendAsync(request, cancellationToken);
            }

            return response;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error de red, reintentando...");
            await Task.Delay(_errorDelay, cancellationToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
