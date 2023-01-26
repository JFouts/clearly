// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using Clearly.Core;

namespace Clearly.Crud.Models.EntityGraph;

/// <summary>
/// Represents a node in an type's definition graph for a type.
/// </summary>
public abstract class TypeDefinitionNode : DefinitionNode
{
    /// <summary>
    /// Gets the Type this node represents.
    /// </summary>
    public Type Type { get; }

    public TypeDefinitionNode(Type type)
    {
        Type = type;
    }
}

/// <summary>
/// Represents a node in an type's definition graph for a data type that is an <see cref="IEntity"/>.
/// </summary>
public class EntityTypeDefinitionNode : ObjectTypeDefinitionNode
{
    public EntityTypeDefinitionNode(Type type)
        : base(type)
    {
    }
}

/// <summary>
/// Represents a node in an type's definition graph for a data type that is a value type.
/// </summary>
public class ValueTypeDefinitionNode : TypeDefinitionNode
{
    public ValueTypeDefinitionNode(Type type)
        : base(type)
    {
    }
}

/// <summary>
/// Represents a node in an type's definition graph for a data type that is an <see cref="object"/>.
/// </summary>
public class ObjectTypeDefinitionNode : TypeDefinitionNode
{
    public IEnumerable<PropertyDefinitionNode> Properties { get; set; } = new List<PropertyDefinitionNode>();

    public ObjectTypeDefinitionNode(Type type)
        : base(type)
    {
    }
}

/// <summary>
/// Represents a node in a definition graph for a property of an object.
/// </summary>
public class PropertyDefinitionNode : DefinitionNode
{
    /// <summary>
    /// Gets the <see cref="PropertyInfo" /> this node represents.
    /// </summary>
    public PropertyInfo Property { get; }

    /// <summary>
    /// Gets the <see cref="ObjectTypeDefinitionNode"/> representing the type of the property this node represents.
    /// </summary>
    public ObjectTypeDefinitionNode Type { get; }

    public PropertyDefinitionNode(PropertyInfo property, ObjectTypeDefinitionNode type)
    {
        Property = property;
        Type = type;
    }
}

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

public interface IDefinitionFeature
{
}

/// <summary>
/// Represents a node in an type's definition graph for a type.
/// </summary>
public abstract class TypeDefinitionNodeFlattened : DefinitionNodeFlattened
{
}

/// <summary>
/// Represents a node in an type's definition graph for a data type that is an <see cref="IEntity"/>.
/// </summary>
public class EntityTypeDefinitionNodeFlattened : ObjectTypeDefinitionNodeFlattened
{
}

/// <summary>
/// Represents a node in an type's definition graph for a data type that is a value type.
/// </summary>
public class ValueTypeDefinitionNodeFlattened : TypeDefinitionNodeFlattened
{
}

/// <summary>
/// Represents a node in an type's definition graph for a data type that is an <see cref="object"/>.
/// </summary>
public class ObjectTypeDefinitionNodeFlattened : TypeDefinitionNodeFlattened
{
    public IEnumerable<PropertyDefinitionNodeFlattened> Properties { get; set; } = new List<PropertyDefinitionNodeFlattened>();
}

/// <summary>
/// Represents a node in a definition graph for a property of an object.
/// </summary>
public class PropertyDefinitionNodeFlattened : DefinitionNodeFlattened
{
    /// <summary>
    /// Gets or sets the NodeKey for the <see cref="TypeDefinitionNodeFlattened"/> the type of the property this node represents.
    /// </summary>
    public string TypeNodeKey { get; set; } = string.Empty;
}

/// <summary>
/// Represents a node in a type's definition graph.
/// </summary>
public abstract class DefinitionNodeFlattened
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
    /// Gets or sets the list of features that are defined for this graph node.
    /// </summary>
    public IEnumerable<IDefinitionFeature> Features { get; set; } = new List<IDefinitionFeature>();
}
