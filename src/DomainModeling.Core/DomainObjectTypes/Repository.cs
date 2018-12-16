using System;

namespace DomainModeling.Core.DomainObjectTypes {
  /// <summary>
  /// A Repository is a Domain Object that has the responsiblity
  /// of persisting Aggregates and retriving persisted Aggregates
  /// </summary>
  /// <typeparam name="T">
  /// The Type of Aggreate you wish to retrieve
  /// </typeparam>
  public interface Repository<T> where T : AggregateRoot {

    /// <summary>
    /// Retrieve a perviously persisted Aggregate by it's identity
    /// </summary>
    /// <param name="id">
    /// The identity of the Aggregate you wish to retrieve
    /// </param>
    /// <returns>
    /// The Aggregate with a Identity of <paramref name="id"/>
    /// </returns>
    Aggregate<T> Get(Guid id);

    /// <summary>
    /// Persist an Aggregate
    /// </summary>
    /// <param name="aggregate">The Aggregate you wish to persist</param>
    void Save(Aggregate<T> aggregate);
  }
}
