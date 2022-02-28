// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using DomainModeling.Core;
using Microsoft.AspNetCore.Mvc;

namespace DomainModeling.Crud.RestApi.Controllers;

[GenericEntityController]
[Route("[generic]/[type]")]
public class SchemaController<TEntity> : ControllerBase
    where TEntity : IEntity
{
    private readonly IEntityDefinitionFactory entityDefinitionFactory;

    public SchemaController(IEntityDefinitionFactory entityDefinitionFactory)
    {
        this.entityDefinitionFactory = entityDefinitionFactory;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var baseUrl = string.Empty;
        var entity = entityDefinitionFactory.CreateForType(typeof(TEntity));
        
        var def = new
        {
            @id = $"{baseUrl}/api/doc/{entity.NameKey}",
        };

        return Ok(def);
    }
}