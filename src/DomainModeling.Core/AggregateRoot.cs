namespace DomainModeling.Core
{
    public abstract record AggregateRoot : IEntity
    {
        public Guid Id { get; set; }
    }
}
