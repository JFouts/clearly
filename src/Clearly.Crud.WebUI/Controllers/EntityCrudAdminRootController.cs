// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Mvc;

namespace Clearly.Crud.WebUi.Controllers;

/// <summary>
/// Controller for rendering the Blazor Web UI.
/// </summary>
public class EntityCrudAdminRootController : Controller
{
    /// <summary>
    /// Renders the shell markup for the Blazor Web UI.
    /// </summary>
    /// <returns>The html shell.</returns>
    public IActionResult Index()
    {
        return View();
    }
}
