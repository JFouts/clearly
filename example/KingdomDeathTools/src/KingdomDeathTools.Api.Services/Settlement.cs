using DomainModeling.Attributes.UI;
using DomainModeling.Core;
using DomainModeling.Crud;

namespace KingdomDeathTools.Api.Services;

public record Settlement : AggregateRoot {
    [FieldViewComponent(SystemViewComponents.Input)]
    public string Name { get; set; } = string.Empty;

    [FieldViewComponent(SystemViewComponents.DropDownList)]
    public string Campaign { get; set; } = string.Empty;

    [FieldViewComponent(SystemViewComponents.MultiSelectList)]
    public IEnumerable<string> Expansions { get; set; } = new string[0];
}


public record Survivor : AggregateRoot {
    public Guid SettlementId { get; set; }

    [FieldViewComponent(SystemViewComponents.Input)]
    public string Name { get; set; } = string.Empty;

    public int Insanity { get; set; }
    public int Evasion { get; set; }
    public int Strength { get; set; }
    public int Accuracy { get; set; }
    public int Luck { get; set; }
}