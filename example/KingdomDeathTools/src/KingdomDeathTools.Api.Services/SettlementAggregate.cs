using DomainModeling.Attributes.UI;
using DomainModeling.Core;
using DomainModeling.Crud;

namespace KingdomDeathTools.Api.Services;

public record Settlement : AggregateRoot {
    [FieldViewComponent(SystemViewComponents.Input)]
    public string SettlementName { get; set; } = string.Empty;

    [FieldViewComponent(SystemViewComponents.DropDownList)]
    public string Campaign { get; set; } = string.Empty;

    [FieldViewComponent(SystemViewComponents.MultiSelectList)]
    public IEnumerable<string> Expansions { get; set; } = new string[0];
}