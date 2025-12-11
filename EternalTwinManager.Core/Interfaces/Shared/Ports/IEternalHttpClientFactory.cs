using System.Net;

namespace EternalTwinManager.Core.Interfaces.Shared.Ports;

public interface IEternalHttpClientFactory
{
    HttpClient CreateClient(bool allowAutoRedirect, CookieContainer cookieContainer);
}