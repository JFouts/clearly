using System;

namespace DomainModeling.Core.DomainObjectTypes {
    public interface Aggregate<TAggregateRoot> where TAggregateRoot : AggregateRoot {
        Guid Id { get; }
        long Version { get; }
        /// <summary>
        /// An Aggregate Root is the Entity at the root of an Aggregate. An Aggregate is a grouping of Entities togther
        /// to all for invarients in business rules.
        /// </summary>
        TAggregateRoot AggregateRoot { get; }
    }
}