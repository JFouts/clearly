// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud;

public class EntityDefinition : BaseDefinition
{
    private readonly List<EntityFieldDefinition> field;
    public IEnumerable<EntityFieldDefinition> Fields => field;

    private readonly List<IEntityFeature> features = new List<IEntityFeature>();
    public IEnumerable<IEntityFeature> Features => features;

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

    public void AddFeature(IEntityFeature feature)
    {
        features.Add(feature);
    }
}

public interface IEntityFeature
{

}