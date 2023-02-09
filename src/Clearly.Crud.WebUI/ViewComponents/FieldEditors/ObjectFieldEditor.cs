// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.EntityGraph;
using Clearly.Crud.WebUi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Clearly.Crud.WebUi.ViewComponents.FieldEditors;

/// <summary>
/// Editor for a generic object.
/// The object will be recursively rendered using the <see cref="ObjectTypeDefinition"/> defined for it.
/// </summary>
[ViewComponent]
public class ObjectFieldEditor : FieldEditorViewComponent
{
    private readonly IEntityDefinitionGraphFactory entityDefinitionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectFieldEditor"/> class.
    /// </summary>
    /// <param name="entityDefinitionFactory">Factory used to create the defintion for the objects being rendered.</param>
    public ObjectFieldEditor(IEntityDefinitionGraphFactory entityDefinitionFactory)
    {
        this.entityDefinitionFactory = entityDefinitionFactory;
    }

    /// <inheritdoc/>
    public override Task<IViewComponentResult> InvokeAsync(PropertyDefinitionNode fieldDefinition, object value)
    {
        var definition = entityDefinitionFactory.CreateForType(fieldDefinition.Property.PropertyType);

        return Task.FromResult<IViewComponentResult>(View(new ObjectFieldEditorViewModel(definition)
        {
            Id = fieldDefinition.Property.Name,
            FieldName = fieldDefinition.Property.Name,
            Label = fieldDefinition.DisplayName,
            Fields = BuildFieldsViewModel(definition, value).ToList(),
        }));
    }

    private static IEnumerable<FieldEditorViewModel> BuildFieldsViewModel(ObjectTypeDefinitionNode definition, object value)
    {
        foreach (var property in definition.Properties)
        {
            var feature = property.Using<CrudAdminPropertyFeature>();

            yield return BuildFieldViewModel(property, value, feature);
        }
    }

    private static FieldEditorViewModel BuildFieldViewModel(PropertyDefinitionNode definition, object value, CrudAdminPropertyFeature feature)
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
