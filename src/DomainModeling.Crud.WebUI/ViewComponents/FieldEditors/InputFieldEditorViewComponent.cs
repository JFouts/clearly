// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using DomainModeling.Crud.WebUi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DomainModeling.Crud.WebUi.ViewComponents.FieldEditors;

[ViewComponent]
public class InputFieldEditor : FieldEditorViewComponent
{
    public override Task<IViewComponentResult> InvokeAsync(EntityFieldDefinition fieldDefinition, object value)
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
