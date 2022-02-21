namespace DomainModeling.Crud;

public class EntityDefinition : BaseDefinition
{
    private readonly List<EntityFieldDefinition> _field;
    public IEnumerable<EntityFieldDefinition> Fields => _field;

    public Type Entity { get; set; }

    public string NameKey { get; set; }
    public string DisplayName { get; set; }

    public EntityDefinition(Type entity)
    {
        Entity = entity;
        NameKey = entity.Name;
        DisplayName = entity.Name.SplitCamelCase();
        _field = entity.GetProperties().Select(x => new EntityFieldDefinition(x)).ToList();
    }
}
