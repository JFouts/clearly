using Clearly.Core;
using Clearly.Crud;
using Clearly.Crud.WebUi;

namespace KingdomDeathTools.Api.Services;

public record Settlement : AggregateRoot, INamedEntity
{
    [FieldEditor(SystemViewComponents.Input)]
    public string Name { get; set; } = string.Empty;

    [FieldEditor(SystemViewComponents.DropDownList)]
    // TODO: Some way of defining this
    //[FieldEditorProperty("DataSource", "People of the Lantern;People of the Sun;People of the Stars")]
    public string Campaign { get; set; } = string.Empty;

    [FieldEditor(SystemViewComponents.MultiSelectList)]
    // TODO: Some way of defining this
    //[FieldEditorProperty("DataSource", "Gorm;Slenderman;Man Hunter;Flower Knight;Lonely Tree")]
    public IEnumerable<string> Expansions { get; set; } = Array.Empty<string>();
}
