using Clearly.Core;

namespace KingdomDeathTools.Api.Services;

public record Disorder : AggregateRoot, INamedEntity
{
    public string Name { get; set; } = string.Empty;
}
