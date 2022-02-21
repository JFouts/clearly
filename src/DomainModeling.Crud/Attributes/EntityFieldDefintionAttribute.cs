// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud;

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
    protected internal abstract void ApplyToEntityFieldDefinition(EntityDefinition entity, EntityFieldDefinition field);
}
