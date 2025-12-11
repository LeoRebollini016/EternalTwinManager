namespace EternalTwinManager.Core.Entities.Brutes;

public class BruteInfo
{
    public string Id { get; }
    public string Name { get; }
    public int Level { get; }
    public int Hp { get; }
    public int Xp { get; }
    public int Endurance { get; }
    public int Strenght { get; }
    public int Agility { get; }
    public int Speed { get; }
    public int FightLeft { get; }
    public bool RegisteredForTournament { get; }
    public IReadOnlyList<string> Weapons { get; }
    public IReadOnlyList<string> Skills { get; }
    public IReadOnlyList<string> Pets { get; }
    public IReadOnlyList<string> DestinyPath { get; }
    public IReadOnlyList<string> PreviousDestinyPath { get; }

    public BruteInfo(
        string id,
        string name,
        int level,
        int hp,
        int xp,
        int endurance,
        int strenght,
        int agility,
        int speed,
        int fightLeft,
        bool registeredForTournament,
        IEnumerable<string>? weapons,
        IEnumerable<string>? skills,
        IEnumerable<string>? pets,
        IEnumerable<string>? destinyPath,
        IEnumerable<string>? previousDestinyPath)
    {
        Id = id;
        Name = name;
        Level = level;
        Hp = hp;
        Xp = xp;
        Endurance = endurance;
        Strenght = strenght;
        Agility = agility;
        Speed = speed;
        FightLeft = fightLeft;
        RegisteredForTournament = registeredForTournament;
        Weapons = (weapons ?? Enumerable.Empty<string>()).ToList();
        Skills = (skills?? Enumerable.Empty<string>()).ToList();
        Pets = (pets ?? Enumerable.Empty<string>()).ToList();
        DestinyPath = (destinyPath ?? Enumerable.Empty<string>()).ToList();
        PreviousDestinyPath = (previousDestinyPath ?? Enumerable.Empty<string>()).ToList();
    }
}
