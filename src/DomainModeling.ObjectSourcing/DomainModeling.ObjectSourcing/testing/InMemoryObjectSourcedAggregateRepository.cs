using System;
using System.Collections.Generic;
using DomainModeling.Core.DomainObjectTypes;
using DomainModeling.Core.Exceptions;

namespace DomainModeling.ObjectSourcing.Testing
{
  /// <summary>
  /// This is only intended for testing or debugging puposes. It does not
  /// support multiple asyncronous calls and should not be used in
  /// production code.
  /// </summary>
  public class InMemoryObjectSourcedAggregateRepository<T> : Repository<T>
      where T : AggregateRoot {

    private Dictionary<Guid, Aggregate<T>> _data =
      new Dictionary<Guid, Aggregate<T>>();

    public Aggregate<T> Get(Guid id) {
      AssertAggregateExists(id);

      return _data[id];
    }

    public void Save(Aggregate<T> aggregate) {
      var id = aggregate.AggregateRoot.Id;

      AssertVersionMatches(id, aggregate.Version);

      _data[id] = aggregate;
    }

    private long GetExpectedVersion(Guid id) {
      return _data.ContainsKey(id) ? _data[id].Version : 0L;
    }

    private void AssertAggregateExists(Guid id) {
      if (!_data.ContainsKey(id))
        throw new NotFoundException(id);
    }

    private void AssertVersionMatches(Guid id, long version) {
      var expectedVersion = GetExpectedVersion(id);
      if (expectedVersion != version)
        throw new VersionMismatchException(id, expectedVersion, version);
    }
  }
}
