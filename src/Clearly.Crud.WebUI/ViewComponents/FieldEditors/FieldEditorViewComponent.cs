// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.Models.EntityGraph;
using Microsoft.AspNetCore.Mvc;

namespace Clearly.Crud.WebUi.ViewComponents.FieldEditors;

public abstract class FieldEditorViewComponent : ViewComponent
{
    public abstract Task<IViewComponentResult> InvokeAsync(PropertyDefinitionNode fieldDefinition, object value);
}
