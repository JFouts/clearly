// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud;

/// <summary>
/// Applies changes to the <see cref="FieldDefinition"/> when the it is built.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public abstract class FieldDefinitionAttribute : Attribute
{
    /// <summary>
    /// Called when the <see cref="FieldDefinition"/> is being built to apply any changes caused by this attribute.
    /// </summary>
    /// <param name="objectType">The object type definition the field is on.</param>
    /// <param name="field">The field Definition being built.</param>
    protected internal abstract void ApplyToFieldDefinition(ObjectTypeDefinition objectType, FieldDefinition field);
}
