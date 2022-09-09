// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections;
using Clearly.Crud.WebUi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Clearly.Crud.WebUi.ViewComponents.FieldEditors;

[ViewComponent]
public class ObjectListFieldEditor : FieldEditorViewComponent
{
    private readonly IEntityDefinitionFactory entityDefinitionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectListFieldEditor"/> class.
    /// </summary>
    /// <param name="entityDefinitionFactory">Factory used to create the defintion for the objects being rendered.</param>
    public ObjectListFieldEditor(IEntityDefinitionFactory entityDefinitionFactory)
    {
        this.entityDefinitionFactory = entityDefinitionFactory;
    }

    /// <inheritdoc/>
    public override Task<IViewComponentResult> InvokeAsync(FieldDefinition fieldDefinition, object value)
    {
        if (value is not IEnumerable listValue)
        {
            throw new ArgumentException($"{nameof(value)} must be of type {nameof(IEnumerable)}", nameof(value));
        }

        var childType = fieldDefinition.Property.PropertyType.GetEnumerableType();

        var definition = entityDefinitionFactory.CreateForType(childType);

        return Task.FromResult<IViewComponentResult>(
            View(new ObjectListEditorViewModel(definition, listValue)
            {
                Id = fieldDefinition.Property.Name,
                FieldName = fieldDefinition.Property.Name,
                Label = fieldDefinition.DisplayName,
            }));
    }
}
