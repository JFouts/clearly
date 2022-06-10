// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections;
using Clearly.Core;
using Clearly.Crud.Services;
using Clearly.Crud.WebUi.ViewComponents.FieldEditors;

namespace Clearly.Crud.WebUi;

public class CrudAdminEntityFieldModule : EntityFieldModule
{
    /// <inheritdoc/>
    public override void OnApplyingModule(EntityDefinition entity, FieldDefinition field)
    {
        var metadata = field.Using<CrudAdminFieldFeature>();

        if (field.Property.Name == nameof(IEntity.Id))
        {
            metadata.DisplayInEditor = false;
            metadata.DisplayOnSearch = false;
        }
    }

    /// <inheritdoc/>
    public override void OnApplyingFallbackDefaults(EntityDefinition entity, FieldDefinition field)
    {
        var metadata = field.Using<CrudAdminFieldFeature>();

        if (string.IsNullOrWhiteSpace(metadata.EditorViewComponentName))
        {
            ConfigureDefaultEditorFeature(metadata, field);
        }

        if (string.IsNullOrWhiteSpace(metadata.DisplayTemplate))
        {
            metadata.DisplayTemplate = GetDefaultDisplayTemplateForType(field.Property.PropertyType);
        }
    }

    private static void ConfigureDefaultEditorFeature(CrudAdminFieldFeature feature, FieldDefinition field)
    {
        var type = field.Property.PropertyType;

        if (type == typeof(string))
        {
            feature.EditorViewComponentName = nameof(InputFieldEditor);

            return;
        }

        if (type.IsEnum)
        {
            feature.EditorViewComponentName = nameof(DropDownListFieldEditor);
            feature.EditorProperties["DataSource"] = string.Join(";", type.GetEnumValues().OfType<object>().Select(x => $"{x},{type.GetEnumName(x)?.FormatForDisplay()}"));

            return;
        }

        if (type.IsAssignableTo(typeof(IEntity)))
        {
            feature.EditorViewComponentName = nameof(DropDownListFieldEditor);
            feature.EditorProperties["DataSource"] = typeof(EntityDataSource<>).MakeGenericType(type);

            return;
        }

        if (type.IsAssignableTo(typeof(IEnumerable)))
        {
            var enumerableType = type.GetEnumerableType();

            if (enumerableType.IsAssignableTo(typeof(IEntity)))
            {
                feature.EditorViewComponentName = nameof(MultiSelectListFieldEditor);
                feature.EditorProperties["DataSource"] = typeof(EntityDataSource<>).MakeGenericType(enumerableType);
            }
            else
            {
                feature.EditorViewComponentName = nameof(ObjectListFieldEditor);
            }

            return;
        }

        if (type.IsClass)
        {
            feature.EditorViewComponentName = nameof(ObjectFieldEditor);

            return;
        }

        feature.EditorViewComponentName = nameof(InputFieldEditor);
    }

    private static string GetDefaultDisplayTemplateForType(Type type)
    {
        return type.Name;
    }
}
