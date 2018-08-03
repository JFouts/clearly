using System;
using System.Linq;

namespace DomainModeling.Core.DomainObjectTypes {
    public interface Repository<T> where T : AggregateRoot {
        T Get(Guid id);
        void Save(T t);
    }
}
