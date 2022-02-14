using DomainModeling.Attributes.UI;
using DomainModeling.Core;
using DomainModeling.Crud;
using DomainModeling.Crud.Services;

namespace KingdomDeathTools.Api.Services;

public record Settlement : AggregateRoot, INamedEntity {
    [FieldEditor(SystemViewComponents.Input)]
    public string Name { get; set; } = string.Empty;

    [FieldEditor(SystemViewComponents.DropDownList)]
    [FieldEditorProperty("DataSource", "People of the Sun;People of the Moon")]
    public string Campaign { get; set; } = string.Empty;

    [FieldEditor(SystemViewComponents.MultiSelectList)]
    public IEnumerable<string> Expansions { get; set; } = new string[0];
}

public record Survivor : AggregateRoot, INamedEntity {
    [FieldEditor(SystemViewComponents.DropDownList)]
    [FieldEditorProperty("DataSource", typeof(EntityDataSource<Settlement>))]
    public Guid SettlementId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Insanity { get; set; }
    public int Evasion { get; set; }
    public int Strength { get; set; }
    public int Accuracy { get; set; }
    public int Luck { get; set; }
}

public class EntityDefinition
{

}