using System;

namespace DomainModeling.Core.DomainObjectTypes {
    /// <summary>
    /// A Command is a special type of Value Object that represents an action a user
    /// wishes to take on the system.
    /// </summary>
    public interface Command {
        /// <summary>
        /// Dispate a Command being a Value Object and immutable we use an identity to
        /// uniquely identify Commands so that they are not processed multiple times
        /// </summary>
        Guid Id { get; }
    }
}
