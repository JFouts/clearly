// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.EntityGraph;

/// <summary>
/// Represents a node in a type's definition graph.
/// </summary>
public abstract class DefinitionNode
{
    /// <summary>
    /// Gets or sets the display name of the field.
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;
        
    /// <summary>
    /// Gets or sets a unique identifier representing this node.
    /// </summary>
    public string NodeKey { get; set; } = string.Empty;

    /// <summary>
    /// Gets the list of features that are registered for this definition node.
    /// </summary>
    /// <remarks>
    /// Typically you should prefer using <see cref="Using{TFeature}"/> instead.
    /// </remarks>
    public IEnumerable<IDefinitionFeature> RegisteredFeatures => Features;

    /// <summary>
    /// Gets the list of features that are defined for this graph node.
    /// </summary>
    protected List<IDefinitionFeature> Features { get; } = new List<IDefinitionFeature>();

    /// <summary>
    /// If the feature of type <see cref="TFeature" /> is not yet applied to this graph node it 
    /// will initialize that feature and then apply it to this field, then return that initialized
    /// feature. Otherwise it returns the previously initialized feature for this graph node.
    /// </summary>
    /// <typeparam name="TFeature">The feature this field is using.</typeparam>
    /// <returns>A feature that has been applied and initialized for this field.</returns>
    public TFeature Using<TFeature>()
        where TFeature : class, IDefinitionFeature, new()
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
