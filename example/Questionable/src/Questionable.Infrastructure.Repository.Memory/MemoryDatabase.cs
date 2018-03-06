using System.Collections.Generic;

namespace Repository.Memory
{
    public class MemoryDatabase<T>
    {
        public List<T> Data { get; } = new List<T>();
    }
}