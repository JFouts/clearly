// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

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
    private readonly IEntityDefinitionFactory entityDefinitionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectFieldEditor"/> class.
    /// </summary>
    /// <param name="entityDefinitionFactory">Factory used to create the defintion for the objects being rendered.</param>
    public ObjectFieldEditor(IEntityDefinitionFactory entityDefinitionFactory)
    {
        this.entityDefinitionFactory = entityDefinitionFactory;
    }

    /// <inheritdoc/>
    public override Task<IViewComponentResult> InvokeAsync(FieldDefinition fieldDefinition, object value)
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

    private static IEnumerable<FieldEditorViewModel> BuildFieldsViewModel(ObjectTypeDefinition definition, object value)
    {
        foreach (var field in definition.Fields)
        {
            var feature = field.Using<CrudAdminFieldFeature>();

            yield return BuildFieldViewModel(field, value, feature);
        }
    }

    private static FieldEditorViewModel BuildFieldViewModel(FieldDefinition definition, object value, CrudAdminFieldFeature feature)
    {
        return new FieldEditorViewModel(definition)
        {
            FieldName = definition.Property.Name,
            FieldEditorName = feature.EditorViewComponentName,
            Hidden = !feature.DisplayInEditor,
            Value = definition.Property.GetValue(value),
        };
    }
}
