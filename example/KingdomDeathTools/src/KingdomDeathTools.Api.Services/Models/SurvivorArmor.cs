using Clearly.Core;

namespace KingdomDeathTools.Api.Services;

public record SurvivorArmor : IValueObject
{
    public int Head { get; set; }
    public int Arms { get; set; }
    public int Body { get; set; }
    public int Waist { get; set; }
    public int Legs { get; set; }
}
