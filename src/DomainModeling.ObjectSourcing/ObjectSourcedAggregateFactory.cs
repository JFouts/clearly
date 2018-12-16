using System;
using DomainModeling.Core.DomainObjectTypes;

namespace DomainModeling.ObjectSourcing {
    public class ObjectSourcedAggregateFactory<TAggregateRoot> : Factory<TAggregateRoot> where TAggregateRoot : AggregateRoot, new() {
        public Aggregate<TAggregateRoot> Create(Guid id) {
            return new ObjectSourcedAggregate<TAggregateRoot>(id);
        }
    }
}
