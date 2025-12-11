using EternalTwinManager.Core.Dtos.Brutes.Shared;

namespace EternalTwinManager.Core.Interfaces.Shared;

public interface IHeaderConfigurator
{
    void SetDefaultHeaders(HttpClient client);
    void SetAuthHeaders(HttpClient client, AuthResultDto authData);
    void SetCsrf(HttpClient client, string csrfToken);
}
