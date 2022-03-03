using Clearly.Core;
using Clearly.Crud;
using Clearly.Crud.WebUi;
using Clearly.Crud.JsonLd;

namespace KingdomDeathTools.Api.Services;

public record Settlement : AggregateRoot, INamedEntity
{
    [FieldEditor(SystemViewComponents.Input)]
    public string Name { get; set; } = string.Empty;

    [FieldEditor(SystemViewComponents.DropDownList)]
    [FieldEditorProperty("DataSource", "People of the Lantern;People of the Sun;People of the Stars")]
    public string Campaign { get; set; } = string.Empty;

    [FieldEditor(SystemViewComponents.MultiSelectList)]
    [FieldEditorProperty("DataSource", "Gorm;Slenderman;Man Hunter;Flower Knight;Lonely Tree")]
    public IEnumerable<string> Expansions { get; set; } = new string[0];
}
