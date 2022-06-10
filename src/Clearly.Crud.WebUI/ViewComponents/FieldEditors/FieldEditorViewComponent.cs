// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Mvc;

namespace Clearly.Crud.WebUi.ViewComponents.FieldEditors;

public abstract class FieldEditorViewComponent : ViewComponent
{
    public abstract Task<IViewComponentResult> InvokeAsync(FieldDefinition fieldDefinition, object value);
}
