using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Options;
using EternalTwinManager.Core.Options;
using EternalTwinManager.Core.Interfaces.Shared.Ports;

namespace EternalTwinManager.Infraestructure.Http;

public class EternalHttpClientFactory : IEternalHttpClientFactory
{
    private readonly ApiSettingsOptions _apiOptions;

    public EternalHttpClientFactory(IOptions<ApiSettingsOptions> options)
    {
        _apiOptions = options.Value;
    }

    public HttpClient CreateClient(bool allowAutoRedirect, CookieContainer cookieContainer)
    {
        var handler = new HttpClientHandler
        {
            CookieContainer = cookieContainer ?? new CookieContainer(),
            UseCookies = true,
            AllowAutoRedirect = allowAutoRedirect,
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };

        var client = new HttpClient(handler)
        {
            BaseAddress = new Uri(_apiOptions.BaseUrl),
            Timeout = TimeSpan.FromSeconds(30)
        };

        return client;
    }
}