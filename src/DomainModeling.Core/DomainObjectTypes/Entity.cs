using System;

namespace DomainModeling.Core.DomainObjectTypes {
    /// <summary>
    /// An Entity is a Domain Object that maintains an idenity over time. The attributes associated with
    /// the Entity can change over time, but the identity remains constant.
    /// </summary>
    public interface Entity {
        /// <summary>
        /// The unique identity of the entity that remains constant over time.
        /// </summary>
        Guid Id { get; }
    }
}
