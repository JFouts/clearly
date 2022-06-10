using Clearly.Core;

namespace KingdomDeathTools.Api.Services;

public record FightingArt : AggregateRoot, INamedEntity
{
    public string Name { get; set; } = string.Empty;
}
