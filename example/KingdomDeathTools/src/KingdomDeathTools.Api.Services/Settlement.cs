using DomainModeling.Core;
using DomainModeling.Crud;
using DomainModeling.Crud.WebUi;
using DomainModeling.Crud.JsonLd;

namespace KingdomDeathTools.Api.Services;

public record Settlement : AggregateRoot, INamedEntity
{
    [JsonLdSchema(Iri = "https://schema.org/Property")]
    [FieldEditor(SystemViewComponents.Input)]
    public string Name { get; set; } = string.Empty;

    [FieldEditor(SystemViewComponents.DropDownList)]
    [FieldEditorProperty("DataSource", "People of the Lantern;People of the Sun;People of the Stars")]
    public string Campaign { get; set; } = string.Empty;

    [FieldEditor(SystemViewComponents.MultiSelectList)]
    [FieldEditorProperty("DataSource", "People of the Lantern;People of the Sun;People of the Stars")]
    public IEnumerable<string> Expansions { get; set; } = new string[0];
}
