using System;
using DomainModeling.Core.DomainObjectTypes;

namespace DomainModeling.ObjectSourcing.Sample
{
  public class SampleAggregateFactory : Factory<SampleAggregate> {
    public Aggregate<SampleAggregate> Create(Guid id) {
      return new ObjectSourcedAggregate<SampleAggregate>(
        new SampleAggregate(id));
    }
  }

  public class SampleAggregate : AggregateRoot {
    public Guid Id { get; }

    public SampleAggregate(Guid id) {
      Id = id;
    }
  }
}
