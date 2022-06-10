// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;

namespace Clearly.Crud;

/// <summary>
/// Defines features for a field on an object.
/// </summary>
public class FieldDefinition
{
    /// <summary>
    /// Gets or sets the reflected property object for the field this defines.
    /// </summary>
    public PropertyInfo Property { get; set; }
    
    /// <summary>
    /// Gets or sets the display name of the feild.
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// Gets the list of features that are registered for this field.
    /// </summary>
    /// <remarks>
    /// Typically you should prefer using <see cref="Using{TFeature}"/> instead.
    /// </remarks>
    public IEnumerable<IFieldFeature> RegisteredFeatures => Features;

    /// <summary>
    /// Gets the list of features that are defined for this property.
    /// </summary>
    protected List<IFieldFeature> Features { get; } = new List<IFieldFeature>();

    /// <summary>
    /// Initializes a new instance of the <see cref="FieldDefinition"/> class.
    /// Defines properties for a field on an object.
    /// </summary>
    /// <param name="property">The reflected property object for the field this defines.</param>
    public FieldDefinition(PropertyInfo property)
    {
        Property = property;
        DisplayName = property.Name.FormatForDisplay();
    }

    /// <summary>
    /// If the feature of type <see cref="TFeature" /> is not yet applied to this field it will
    /// initialize that feature and then apply it to this feild, then return that initialized
    /// feature. Otherwise it returns the previously initialized feature for this field.
    /// </summary>
    /// <typeparam name="TFeature">The feature this field is using.</typeparam>
    /// <returns>A feature that has been applied and initialized for this field.</returns>
    public TFeature Using<TFeature>()
        where TFeature : class, IFieldFeature, new()
    {
        var feature = Features.OfType<TFeature>().SingleOrDefault();

        if (feature == null)
        {
            feature = new TFeature(); // TODO: Can we use a factory to initialize a feature
            Features.Add(feature);
        }

        return feature;
    }
}
