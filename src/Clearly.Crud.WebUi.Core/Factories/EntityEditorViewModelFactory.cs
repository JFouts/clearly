// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core;
using Clearly.Crud.WebUi.ViewModels;

namespace Clearly.Crud.WebUi.Factories;

/// <inheritdoc/>
public class EntityEditorViewModelFactory<TEntity> : IEntityEditorViewModelFactory<TEntity>
    where TEntity : IEntity
{
    private readonly IEntityDefinitionFactory entityDefinitionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityEditorViewModelFactory{TEntity}"/> class.
    /// </summary>
    /// <param name="entityDefinitionFactory">The factory used to create entity definitons.</param>
    public EntityEditorViewModelFactory(IEntityDefinitionFactory entityDefinitionFactory)
    {
        this.entityDefinitionFactory = entityDefinitionFactory;
    }

    /// <inheritdoc/>
    public EntityEditorViewModel Build(TEntity value)
    {
        var definition = entityDefinitionFactory.CreateForEntity<TEntity>();

        var metadata = definition.Using<CrudAdminEntityFeature>();

        return new EntityEditorViewModel(definition)
        {
            DataSourceUrl = metadata.DataSourceUrl,
            DisplayName = definition.DisplayName,
            Fields = BuildFieldsViewModel(definition, value).ToList(),
        };
    }

    private static IEnumerable<FieldEditorViewModel> BuildFieldsViewModel(EntityDefinition definition, TEntity value)
    {
        foreach (var field in definition.Fields)
        {
            var feature = field.Using<CrudAdminFieldFeature>();

            yield return BuildFieldViewModel(field, value, feature);
        }
    }

    private static FieldEditorViewModel BuildFieldViewModel(FieldDefinition definition, TEntity value, CrudAdminFieldFeature feature)
    {
        return new FieldEditorViewModel(definition)
        {
            FieldName = definition.Property.Name,
            FieldEditorName = feature.EditorComponentName,
            Hidden = !feature.DisplayInEditor,
            Value = definition.Property.GetValue(value),
        };
    }
}
