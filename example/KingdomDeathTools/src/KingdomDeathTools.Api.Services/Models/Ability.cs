using Clearly.Core;

namespace KingdomDeathTools.Api.Services;

public record Ability : AggregateRoot, INamedEntity
{
    public string Name { get; set; } = string.Empty;
}
