// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core;
using Clearly.Crud.WebUi.ViewModels;

namespace Clearly.Crud.WebUi.Factories;

public class EntityEditorViewModelFactory<TEntity> : IEntityEditorViewModelFactory<TEntity>
    where TEntity : IEntity
{
    private readonly IEntityDefinitionFactory entityDefinitionFactory;

    public EntityEditorViewModelFactory(IEntityDefinitionFactory entityDefinitionFactory)
    {
        this.entityDefinitionFactory = entityDefinitionFactory;
    }

    public EntityEditorViewModel Build(TEntity value)
    {
        var definition = entityDefinitionFactory.CreateFor<TEntity>();

        var metadata = definition.Using<CrudAdminEntityFeature>();

        return new EntityEditorViewModel(definition)
        {
            DataSourceUrl = metadata.DataSourceUrl,
            DisplayName = definition.DisplayName,
            Fields = BuildFieldsViewModel(definition, value),
        };
    }

    private IEnumerable<EntityFieldEditorViewModel> BuildFieldsViewModel(EntityDefinition definition, TEntity value)
    {
        return definition.Fields.Select(x => BuildFieldViewModel(x, value));
    }

    private EntityFieldEditorViewModel BuildFieldViewModel(EntityFieldDefinition definition, TEntity value)
    {
        var metadata = definition.Using<CrudAdminEntityFieldFeature>();

        return new EntityFieldEditorViewModel(definition)
        {
            FieldName = definition.Property.Name,
            FieldEditorName = metadata.EditorViewComponentName,
            Value = definition.Property.GetValue(value),
        };
    }
}
