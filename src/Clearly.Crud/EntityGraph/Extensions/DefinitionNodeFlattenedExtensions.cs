// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json.Linq;

namespace Clearly.Crud.EntityGraph;

public static class DefinitionNodeFlattenedExtensions
{
    public static T GetFeature<T>(this DefinitionNodeFlattened node) where T : IDefinitionFeature, new()
    {
        var key = typeof(T).Name.ToLowerInvariant();

        if (!node.Features.ContainsKey(key))
        {
            var feature = new T();
            node.Features[key] = JToken.FromObject(feature);
            
            return feature;
        }

        // TODO: Better exceptions
        return node.Features[key].ToObject<T>() ?? throw new Exception($"Failed to convert feature {key} into it's implementation type.");
    }
}