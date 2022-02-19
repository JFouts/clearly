namespace DomainModeling.Crud.WebUi;

public static class EntityDefinitionnExtensions
{
    public static EntityDefinition CreateFor<TEntity>(this IEntityDefinitionFactory factory)
    {
        return factory.CreateForType(typeof(TEntity));
    }
}
