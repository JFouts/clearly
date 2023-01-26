// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.Models.EntityGraph;
using Clearly.Crud.WebUi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Clearly.Crud.WebUi.ViewComponents.FieldEditors;

/// <summary>
/// Editor for a simple text based input box.
/// </summary>
[ViewComponent]
public class InputFieldEditor : FieldEditorViewComponent
{
    /// <inheritdoc/>
    public override Task<IViewComponentResult> InvokeAsync(PropertyDefinitionNode fieldDefinition, object value)
    {
        return Task.FromResult<IViewComponentResult>(View(new InputFieldEditorViewModel
        {
            Id = fieldDefinition.Property.Name,
            FieldName = fieldDefinition.Property.Name,
            Label = fieldDefinition.DisplayName,
            Value = value,
        }));
    }
}
