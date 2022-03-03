// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;

namespace Clearly.Crud;

public class EntityFieldDefinition
{
    public PropertyInfo Property { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    protected List<IEntityFieldFeature> Features { get; } = new List<IEntityFieldFeature>();

    public EntityFieldDefinition(PropertyInfo property)
    {
        Property = property;
        DisplayName = property.Name.FormatForDisplay();
    }


    public TFeature Using<TFeature>()
        where TFeature : class, IEntityFieldFeature, new()
    {
        var feature = Features.OfType<TFeature>().SingleOrDefault();

        if (feature == null)
        {
            feature = new TFeature();
            Features.Add(feature);
        }

        return feature;
    }
}
