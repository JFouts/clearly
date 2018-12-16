using System;

namespace DomainModeling.Core.DomainObjectTypes {
  /// <summary>
  /// A Factory is a Domain Object that is responsible for the creation
  /// of new Aggregates
  /// </summary>
  /// <typeparam name="T">
  /// The type Aggregate you wish to create
  /// </typeparam>
  public interface Factory<T> where T : AggregateRoot {
    Aggregate<T> Create(Guid id);
  }
}
