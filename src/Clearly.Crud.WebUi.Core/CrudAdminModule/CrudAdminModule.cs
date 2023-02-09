// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections;
using Clearly.Core;
using Clearly.Crud.EntityGraph;

namespace Clearly.Crud.WebUi;

/// <summary>
/// A Clearly module for applying the CRUD admin on top of the CRUD API.
/// </summary>
public class CrudAdminModule : IDefinitionNodeModule
{
    public void OnApplyingModule(DefinitionNode node)
    {
        if (node is PropertyDefinitionNode propertyNode)
        {
            OnApplyingModule(propertyNode);
        }
    }

    public void OnApplyingFallbackDefaults(DefinitionNode node)
    {
        if (node is EntityTypeDefinitionNode entityNode)
        {
            OnApplyingFallbackDefaults(entityNode);
        }
        else if (node is PropertyDefinitionNode propertyNode)
        {
            OnApplyingFallbackDefaults(propertyNode);
        }
    }
    
    /// <inheritdoc/>
    public void OnApplyingFallbackDefaults(EntityTypeDefinitionNode entity)
    {
        var metadata = entity.Using<CrudAdminEntityFeature>();

        if (string.IsNullOrWhiteSpace(metadata.DataSourceUrl))
        {
            metadata.DataSourceUrl = $"/api/{entity.NodeKey.ToLower()}";
        }
    }

    /// <inheritdoc/>
    public void OnApplyingModule(PropertyDefinitionNode property)
    {
        var metadata = property.Using<CrudAdminPropertyFeature>();

        if (property.Property.Name == nameof(IEntity.Id))
        {
            metadata.DisplayInEditor = false;
            metadata.DisplayOnSearch = false;
        }
    }

    /// <inheritdoc/>
    public void OnApplyingFallbackDefaults(PropertyDefinitionNode property)
    {
        var metadata = property.Using<CrudAdminPropertyFeature>();

        if (string.IsNullOrWhiteSpace(metadata.EditorComponentName))
        {
            ConfigureDefaultEditorFeature(metadata, property);
        }

        if (string.IsNullOrWhiteSpace(metadata.DisplayComponentName))
        {
            metadata.DisplayComponentName = GetDefaultDisplayComponentForType(property.Property.PropertyType);
        }
    }

    private static void ConfigureDefaultEditorFeature(CrudAdminPropertyFeature feature, PropertyDefinitionNode property)
    {
        var type = property.Property.PropertyType;

        if (type == typeof(string))
        {
            feature.EditorComponentName = "InputEditorComponent";

            return;
        }

        if (type.IsEnum)
        {
            feature.EditorComponentName = "DropDownEditorComponent";
            property.Using<CrudAdminDropDownFeature>().DataSource = string.Join(";", type.GetEnumValues().OfType<object>().Select(x => $"{x},{type.GetEnumName(x)?.FormatForDisplay()}"));
            property.Using<CrudAdminDropDownFeature>().DataSourceType = "StaticList";

            return;
        }

        if (type.IsAssignableTo(typeof(IEntity)))
        {
            feature.EditorComponentName = "DropDownEditorComponent";
            property.Using<CrudAdminDropDownFeature>().DataSource = type.Name; // TODO: Get the name key
            property.Using<CrudAdminDropDownFeature>().DataSourceType = "EntityDataSource";

            return;
        }

        if (type.IsAssignableTo(typeof(IEnumerable)))
        {
            var enumerableType = type.GetEnumerableType();

            if (enumerableType.IsAssignableTo(typeof(IEntity)))
            {
                feature.EditorComponentName = "MultiSelectEditorComponent";
                property.Using<CrudAdminDropDownFeature>().DataSource = enumerableType.Name; // TODO: Get the name key
                property.Using<CrudAdminDropDownFeature>().DataSourceType = "EntityDataSource";
            }
            else
            {
                feature.EditorComponentName = "ObjectEditorComponent";
            }

            return;
        }

        if (type.IsClass)
        {
            feature.EditorComponentName = "ObjectEditorComponent";

            return;
        }

        feature.EditorComponentName = "InputEditorComponent";
    }

    private static string GetDefaultDisplayComponentForType(Type type)
    {
        switch (Type.GetTypeCode(type))
        {
            case TypeCode.Byte:
            case TypeCode.SByte:
            case TypeCode.UInt16:
            case TypeCode.UInt32:
            case TypeCode.UInt64:
            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
            case TypeCode.Decimal:
            case TypeCode.Double:
            case TypeCode.Single:
                return "NumberDisplayComponent";
            case TypeCode.Object:
                if (type == typeof(Guid))
                {
                    return "TextDisplayComponent";
                }

                if (type.IsAssignableTo(typeof(IEnumerable<string>)))
                {
                    return "TextArrayDisplayComponent";
                }

                if (type.IsAssignableTo(typeof(IEnumerable)))
                {
                    return "ObjectArrayDisplayComponent";
                }

                return "ObjectDisplayComponent";
            case TypeCode.Boolean:
                return "CheckBoxDisplayComponent";
            case TypeCode.DateTime:
                return "DateTimeDisplayComponent";
            case TypeCode.String:
            case TypeCode.Empty:
            case TypeCode.Char:
            case TypeCode.DBNull:
            default:
                return "TextDisplayComponent";
        }
    }

}
