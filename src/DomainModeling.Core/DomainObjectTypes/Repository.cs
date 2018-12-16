using System;
using System.Linq;

namespace DomainModeling.Core.DomainObjectTypes {
    public interface Repository<T> where T : AggregateRoot {
        Aggregate<T> Get(Guid id);
        void Save(Aggregate<T> t);
    }
}
