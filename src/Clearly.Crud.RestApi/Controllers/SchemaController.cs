// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Mvc;

namespace Clearly.Crud.RestApi.Controllers;

[Route("[controller]")]
public class SchemaController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        var def = new
        {
            @id = $"/schema",
        };

        return Ok(def);
    }
}
