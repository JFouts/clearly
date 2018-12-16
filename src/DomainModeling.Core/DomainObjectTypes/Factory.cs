using System;

namespace DomainModeling.Core.DomainObjectTypes {
    public interface Factory<T> where T : AggregateRoot {
        Aggregate<T> Create(Guid id);
    }
}
