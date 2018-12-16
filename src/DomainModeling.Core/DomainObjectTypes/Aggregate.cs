namespace DomainModeling.Core.DomainObjectTypes {
  /// <summary>
  /// An Aggregate is a grouping of Entities togther
  /// to all for invarients in business rules.
  /// </summary>
  public interface Aggregate<TAggregateRoot> where TAggregateRoot : AggregateRoot {
    /// <summary>
    /// A version number for the aggreate that increases with every state change
    /// </summary>
    long Version { get; }

    /// <summary>
    /// An Aggregate Root is the Entity at the root of an Aggregate.
    /// </summary>
    TAggregateRoot AggregateRoot { get; }
  }
}
