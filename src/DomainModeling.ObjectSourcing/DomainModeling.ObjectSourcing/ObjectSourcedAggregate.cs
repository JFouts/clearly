using DomainModeling.Core.DomainObjectTypes;

namespace DomainModeling.ObjectSourcing
{
  public class ObjectSourcedAggregate<T> : Aggregate<T>
    where T : AggregateRoot {
    public long Version { get; private set; }

    public T AggregateRoot { get; }

    public ObjectSourcedAggregate(T aggregateRoot) {
      AggregateRoot = aggregateRoot;
      Version = 0;
    }
  }
}
