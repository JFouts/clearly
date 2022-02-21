// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;

namespace DomainModeling.Crud;

public class EntityFieldDefinition : BaseDefinition
{
    public PropertyInfo Property { get; set; }
    public string DisplayName { get; set; } = string.Empty;

    public EntityFieldDefinition(PropertyInfo property)
    {
        Property = property;
        DisplayName = property.Name.FormatForDisplay();
    }
}
