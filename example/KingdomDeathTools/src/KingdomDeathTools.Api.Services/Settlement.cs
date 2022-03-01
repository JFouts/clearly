using DomainModeling.Core;
using DomainModeling.Crud;
using DomainModeling.Crud.WebUi;
using DomainModeling.Crud.JsonLd;

namespace KingdomDeathTools.Api.Services;

public record Settlement : AggregateRoot, INamedEntity
{
    [TermSchema("https://schema.org/Property")]
    [FieldEditor(SystemViewComponents.Input)]
    public string Name { get; set; } = string.Empty;

    [FieldEditor(SystemViewComponents.DropDownList)]
    [FieldEditorProperty("DataSource", "People of the Lantern;People of the Sun;People of the Stars")]
    public string Campaign { get; set; } = string.Empty;

    [FieldEditor(SystemViewComponents.MultiSelectList)]
    [FieldEditorProperty("DataSource", "People of the Lantern;People of the Sun;People of the Stars")]
    public IEnumerable<string> Expansions { get; set; } = new string[0];
}

[TypeSchema("https://schema.org/Recipe", TermsVocab = "https://schema.org/")]
public record Recipe : IEntity
{
    [NonLinkedData]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public AggregateRating AggregateRating { get; set; }
}

public record AggregateRating
{
    public decimal RatingValue { get; set; }
    public decimal RatingCount { get; set; }
}