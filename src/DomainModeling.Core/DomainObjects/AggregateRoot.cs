using System;

namespace DomainModeling.Core {
    /// <summary>
    /// An Aggregate Root is the Entity at the root of an Aggregate. An Aggregate is a grouping of Entities togther
    /// to all for invarients in business rules.
    /// </summary>
    public interface AggregateRoot : Entity { }
}
