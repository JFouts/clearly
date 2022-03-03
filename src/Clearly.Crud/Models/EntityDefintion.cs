// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud;

public class EntityDefinition
{
    private readonly List<EntityFieldDefinition> field;
    public IEnumerable<EntityFieldDefinition> Fields => field;
    protected List<IEntityFeature> Features { get; } = new List<IEntityFeature>();

    public Type Entity { get; set; }

    public string NameKey { get; set; }
    public string DisplayName { get; set; }

    public EntityDefinition(Type entity)
    {
        Entity = entity;
        NameKey = entity.Name;
        DisplayName = entity.Name.FormatForDisplay();
        field = entity.GetProperties().Select(x => new EntityFieldDefinition(x)).ToList();
    }

    public TFeature Using<TFeature>()
        where TFeature : class, IEntityFeature, new()
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
