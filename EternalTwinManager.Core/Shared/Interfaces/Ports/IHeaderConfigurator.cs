using EternalTwinManager.Core.Shared.Models;

namespace EternalTwinManager.Core.Shared.Interfaces.Services;

public interface IHeaderConfigurator
{
    void SetDefaultHeaders(HttpClient client);
    void SetAuthHeaders<T>(HttpClient client, AuthResult<T> authData);
}
