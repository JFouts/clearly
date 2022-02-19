namespace DomainModeling.Crud.WebUi;

/// <summary>
/// Applies changes to the Entity Field Definition when the Definition is being built
/// </summary>
[AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true, Inherited = true)]  
public abstract class EntityFieldDefinitionAttribute : Attribute
{
    /// <summary>
    /// Applies any changes to the entity field Definition caused by this attribute
    /// </summary>
    /// <param name="entity">The entity definition the field is on</param>
    /// <param name="field">The entity field Definition being built</param>
    internal protected abstract void ApplyToEntityFieldDefinition(EntityDefinition entity, EntityFieldDefinition field);
}
