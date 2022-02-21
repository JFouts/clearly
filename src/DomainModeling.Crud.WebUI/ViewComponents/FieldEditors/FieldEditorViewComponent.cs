// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Mvc;

namespace DomainModeling.Crud.WebUi.ViewComponents.FieldEditors;

public abstract class FieldEditorViewComponent : ViewComponent
{
    public abstract Task<IViewComponentResult> InvokeAsync(EntityFieldDefinition fieldDefinition, object value);
}
