namespace EternalTwinManager.Core.Entities.Brutes;

public class OpponentInfo
{
    public string Id { get; }
    public string Name { get; }
    public int Level { get; }
    public int Hp { get; }
    public int Endurance { get; }
    public int Strenght { get; }
    public int Agility { get; }
    public int Speed { get; }
    public IReadOnlyList<string> Weapons { get; }
    public IReadOnlyList<string> Skills { get; }
    public IReadOnlyList<string> Pets { get; }
    public OpponentInfo(
        string id,
        string name,
        int level,
        int hp,
        int endurance,
        int strenght,
        int agility,
        int speed,
        IEnumerable<string>? weapons,
        IEnumerable<string>? skills,
        IEnumerable<string>? pets)
    {
        Id = id;
        Name = name;
        Level = level;
        Hp = hp;
        Endurance = endurance;
        Strenght = strenght;
        Agility = agility;
        Speed = speed;
        Weapons = (weapons ?? Enumerable.Empty<string>()).ToList();
        Skills = (skills ?? Enumerable.Empty<string>()).ToList();
        Pets = (pets ?? Enumerable.Empty<string>()).ToList();
    }
}