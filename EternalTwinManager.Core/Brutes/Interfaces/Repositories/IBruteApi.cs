using EternalTwinManager.Core.Brute.Entities;
using EternalTwinManager.Core.Shared.Models;

namespace EternalTwinManager.Core.Brute.Interfaces.Repositories;

public interface IBruteApi
{
    Task<AuthResult<BruteAccountData>> AuthenticateAsync(string username, string password);
    Task<int> GetGoldAsync(string token);
}
