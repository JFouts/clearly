// // Copyright (c) Justin Fouts All Rights Reserved.
// // Licensed under the MIT License. See LICENSE in the project root for license information.

// namespace Clearly.Crud;

// /// <summary>
// /// Defines all the features of an object type.
// /// </summary>
// public class ObjectTypeDefinition
// {
//     /// <summary>
//     /// Gets  or sets the feilds that are defined on this object.
//     /// </summary>
//     public IEnumerable<PropertyDefinitionNode> Fields { get; set; } = new List<PropertyDefinitionNode>();

//     /// <summary>
//     /// Gets or sets the reflected object type for the type this defines.
//     /// </summary>
//     public Type ObjectType { get; set; }

//     /// <summary>
//     /// Gets or sets the Key used to uniquely identy this object type.
//     /// </summary>
//     public string NameKey { get; set; } = string.Empty;

//     /// <summary>
//     /// Gets or sets the Display Name that is shown for this object type.
//     /// </summary>
//     public string DisplayName { get; set; } = string.Empty;

//     /// <summary>
//     /// Gets the list of features that are registered for this object.
//     /// </summary>
//     /// <remarks>
//     /// Typically you should prefer using <see cref="Using{TFeature}"/> instead.
//     /// </remarks>
//     public IEnumerable<IObjectFeature> RegisteredFeatures => Features;

//     /// <summary>
//     /// Gets the list of features that are defined for this object.
//     /// </summary>
//     protected List<IObjectFeature> Features { get; } = new List<IObjectFeature>();

//     /// <summary>
//     /// If the feature of type <see cref="TFeature" /> is not yet applied to this object type it will
//     /// initialize that feature and then apply it to this object type, then return that initialized
//     /// feature. Otherwise it returns the previously initialized feature for this object type.
//     /// </summary>
//     /// <typeparam name="TFeature">The feature this object type is using.</typeparam>
//     /// <returns>A feature that has been applied and initialized for this object type.</returns>
//     public TFeature Using<TFeature>()
//         where TFeature : class, IEntityFeature, new()
//     {
//         var feature = Features.OfType<TFeature>().SingleOrDefault();

//         if (feature == null)
//         {
//             feature = new TFeature(); // TODO: Can we use a factory to initialize a feature
//             Features.Add(feature);
//         }

//         return feature;
//     }
// }
