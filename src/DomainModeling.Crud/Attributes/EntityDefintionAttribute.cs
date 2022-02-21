// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

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
    protected internal abstract void ApplyToEntityDefinition(EntityDefinition entity);
}
