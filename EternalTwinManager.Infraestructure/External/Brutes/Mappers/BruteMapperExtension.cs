using EternalTwinManager.Infrastructure.External.Brute.Response;

namespace EternalTwinManager.Infrastructure.External.Brute.Mappers;

public static class BruteMapperExtension
{
    public static Core.Entities.Brutes.Brute ForHookToBrute(this BruteForHookResponse src)
    {

        IReadOnlyList<string> Safe(IReadOnlyList<string>? l)
            => l ?? Array.Empty<string>();

        return new Core.Entities.Brutes.Brute
        {
            Name = src.Name ?? string.Empty,
            Gender = src.Gender ?? string.Empty,
            Body = src.Body ?? string.Empty,
            Colors = src.Colors ?? string.Empty,

            EnduranceValue = src.EnduranceValue,
            StrengthValue = src.StrengthValue,
            AgilityValue = src.AgilityValue,
            SpeedValue = src.SpeedValue,

            DestinyPath = Safe(src.DestinyPath),
            PreviousDestinyPath = Safe(src.PreviousDestinyPath),
            Weapons = Safe(src.Weapons),
            Skills = Safe(src.Skills),
            Pets = Safe(src.Pets),

            Victories = src.Victories,
            Losses = src.Losses,

            LastFight = src.LastFight == default ? null : src.LastFight,
            FightsLeft = src.FightsLeft,

            TournamentWins = src.TournamentWins,
            Resets = src.Resets
        };
    }
}
