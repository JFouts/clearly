using System;

namespace DomainModeling.Core {
    public interface Repository<T> where T : Entity {
        T Get(Guid id);
        void Save(T t);
    }
}
