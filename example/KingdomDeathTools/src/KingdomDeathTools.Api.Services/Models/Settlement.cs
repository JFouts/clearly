using Clearly.Core;
using Clearly.Crud.WebUi;

namespace KingdomDeathTools.Api.Services;

public record Settlement : AggregateRoot, INamedEntity
{
    public string Name { get; set; } = string.Empty;

    [DropDownEditor("People of the Lantern;People of the Sun;People of the Stars")]
    public string Campaign { get; set; } = string.Empty;

    [MultiSelectEditor("Gorm;Slenderman;Man Hunter;Flower Knight;Lonely Tree")]
    public IEnumerable<string> Expansions { get; set; } = Array.Empty<string>();
}
