namespace DomainModeling.Crud;

/// <summary>
/// Applies changes to the Entity Definition when the Definition is being built
/// </summary>
[AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true, Inherited = true)]  
public abstract class EntityDefinitionAttribute : Attribute
{
    /// <summary>
    /// Applies any changes to the entity Definition caused by this attribute
    /// </summary>
    /// <param name="entity">The entity being built</param>
    internal protected abstract void ApplyToEntityDefinition(EntityDefinition entity);
}
