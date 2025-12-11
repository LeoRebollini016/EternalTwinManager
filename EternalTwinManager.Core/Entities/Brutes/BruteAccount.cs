namespace EternalTwinManager.Core.Entities.Brutes;

public class BruteAccount
{
    public string AccountId { get; }
    public string Username { get; }
    public int Gold { get; private set; }
    public int BruteLimit { get; }
    public string? EventId { get; private set; }

    public IReadOnlyList<BruteInfo> Brutes { get; private set; }

    public BruteAccount(
        string accountId,
        string username,
        int gold,
        int bruteLimit,
        string? eventId,
        IReadOnlyList<BruteInfo> brutes)
    {
        AccountId = accountId;
        Username = username;
        Gold = gold;
        BruteLimit = bruteLimit;
        EventId = eventId;
        Brutes = brutes?.ToList() ?? new List<BruteInfo>();
    }
    public void SetBrutes(IEnumerable<BruteInfo> brutes) => Brutes = brutes.ToList();
    public void SetGold(int gold) => Gold = gold;
}
