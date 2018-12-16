using System;
using System.Collections.Generic;
using DomainModeling.Core.DomainObjectTypes;
using DomainModeling.Core.Exceptions;
using DomainModeling.Core.Utilities.Interfaces;

namespace DomainModeling.ObjectSourcing {
  public class InMemoryObjectSourcedAggregateRepository<TAggregate> : Repository<TAggregate> where TAggregate : AggregateRoot, new() {
    private readonly IJsonConverter jsonConverter;
    private Dictionary<Guid, DatabaseObject<TAggregate>> database = new Dictionary<Guid, DatabaseObject<TAggregate>>();

    public InMemoryObjectSourcedAggregateRepository(IJsonConverter jsonConverter) {
      this.jsonConverter = jsonConverter;
    }

    public Aggregate<TAggregate> Get(Guid id) {
      if (database.TryGetValue(id, out var obj))
        return new ObjectSourcedAggregate<TAggregate>(id, obj.Version, jsonConverter.Deserialize<TAggregate>(obj.Data));

      throw new NotFoundException(id);
    }

    public void Save(Aggregate<TAggregate> t) {
      var obj = new DatabaseObject<TAggregate> {
        Version = t.Version + 1,
        Data = jsonConverter.Serialize(t.AggregateRoot)
      };

      if (database.TryGetValue(t.Id, out var oldObj) && oldObj.Version != t.Version)
        throw new VersionMismatchException(t.Id, t.Version, oldObj.Version);

      database[t.Id] = obj;
    }

    private class DatabaseObject<T> {
      public long Version { get; set; }
      public string Data { get; set; }
    }
  }
}
