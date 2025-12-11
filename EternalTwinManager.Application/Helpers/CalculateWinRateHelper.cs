using EternalTwinManager.Core.Dtos.Brutes.Battles;

namespace EternalTwinManager.Application.Helpers;

public static class CalculateWinRateHelper
{
    public static Dictionary<string, double> WinRateHelper(Dictionary<string, BruteBattleHistoryDto> history)
    {
        var winRates = new Dictionary<string, double>();

        foreach (var entry in history)
        {
            var winRate = entry.Value.Victories / (double)(entry.Value.Victories + entry.Value.Losses);
            winRates[entry.Key] = winRate;
        }
        return winRates;
    }
}
