using Clearly.Core;

namespace KingdomDeathTools.Api.Services;

public record Survivor : AggregateRoot, INamedEntity
{
    public Settlement Settlement { get; set; }
    public string Name { get; set; } = string.Empty;
    public SurvivorSex Sex { get; set; }
    public int Movement { get; set; }
    public int Insanity { get; set; }
    public int Evasion { get; set; }
    public int Strength { get; set; }
    public int Accuracy { get; set; }
    public int Luck { get; set; }
    public SurvivorArmor Armor { get; set; } = new SurvivorArmor();
    public int HuntXp { get; set; }
    public WeaponProficiencyType WeaponProficiencyType { get; set; } = WeaponProficiencyType.None;
    public int WeaponProficiency { get; set; }
    public IEnumerable<FightingArt> FightingArts { get; set; } = new List<FightingArt>();
    public IEnumerable<Disorder> Disorders { get; set; } = new List<Disorder>();
    public IEnumerable<Ability> Abilities { get; set; } = new List<Ability>();
}
