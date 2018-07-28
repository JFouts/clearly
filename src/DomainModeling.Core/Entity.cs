using System;

namespace DomainModeling.Core {
    /// <summary>
    /// An Entity is a Domain Object that maintains an idenity over time. The attributes associated with
    /// the Entity can change over time, but the identity remains constant.
    /// </summary>
    public abstract class Entity {
        /// <summary>
        /// The unique identity of the entity that remains constant over time.
        /// </summary>
        public Guid Id { get; }

        public Entity(Guid id) {
            Id = Id;
        }
    }
}
