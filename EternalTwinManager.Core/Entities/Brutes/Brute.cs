namespace EternalTwinManager.Core.Entities.Brutes;

public class Brute
{
    public string Name { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public string Body { get; set; } = default!;
    public string Colors { get; set; } = default!;

    public int EnduranceValue { get; set; }
    public int StrengthValue { get; set; }
    public int AgilityValue { get; set; }
    public int SpeedValue { get; set; }

    public IReadOnlyList<string> DestinyPath { get; set; } = Array.Empty<string>();
    public IReadOnlyList<string> PreviousDestinyPath { get; set; } = Array.Empty<string>();
    public IReadOnlyList<string> Weapons { get; set; } = Array.Empty<string>();
    public IReadOnlyList<string> Skills { get; set; } = Array.Empty<string>();
    public IReadOnlyList<string> Pets { get; set; } = Array.Empty<string>();

    public int Victories { get; set; }
    public int Losses { get; set; }

    public int TotalFights => Victories + Losses;

    public double WinRate =>
        TotalFights == 0 ? 0 : (double)Victories / TotalFights;

    public DateTime? LastFight { get; set; }
    public int FightsLeft { get; set; }

    public int TournamentWins { get; set; }
    public int Resets { get; set; }
}
