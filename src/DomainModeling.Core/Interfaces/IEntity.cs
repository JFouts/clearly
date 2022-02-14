namespace DomainModeling.Core;

public interface IEntity
{
    Guid Id { get; set; }
}

public interface INamedEntity : IEntity
{
    string Name { get; set; }
}