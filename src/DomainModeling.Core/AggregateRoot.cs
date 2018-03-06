using System;

namespace DomainModeling.Core
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; set; }
    }
}
