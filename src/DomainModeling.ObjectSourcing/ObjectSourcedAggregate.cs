using System;
using DomainModeling.Core.DomainObjectTypes;

namespace DomainModeling.ObjectSourcing {
    public class ObjectSourcedAggregate<TAggregateRoot> : Aggregate<TAggregateRoot> where TAggregateRoot : AggregateRoot, new() {
        public Guid Id { get; }
        public long Version { get; }
        public TAggregateRoot AggregateRoot { get; }

        public ObjectSourcedAggregate(Guid id) {
            Id = id;
            Version = 0;
            AggregateRoot = new TAggregateRoot();
        }

        public ObjectSourcedAggregate(Guid id, long version, TAggregateRoot aggregateRoot) {
            Id = id;
            Version = version;
            AggregateRoot = aggregateRoot;
        }
    }
}