using System;

namespace DomainModeling.Core.Exceptions {
  public class VersionMismatchException : ConcurrencyException {
    public Guid Id { get; }
    public long ExpectedVersion { get; }
    public long ActualVersion { get; }

    public VersionMismatchException(
      Guid id,
      long expectedVersion,
      long actualVersion)
        :base(
           $"Expected entity with id {id} to be at version " +
           $"{expectedVersion} but was actually at {actualVersion}. " +
           $"This usually means that another command made a change " +
           $"to this entity prior to the completion of the currently " +
           $"executing command. This command can be retried.") {
      Id = id;
      ExpectedVersion = expectedVersion;
      ActualVersion = actualVersion;
    }
  }
}
