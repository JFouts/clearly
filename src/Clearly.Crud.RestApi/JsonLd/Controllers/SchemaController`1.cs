// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core;
using Clearly.Crud.EntityGraph;
using Microsoft.AspNetCore.Mvc;

namespace Clearly.Crud.RestApi.JsonLd;

[GenericEntityController]
[Route("[generic]/[type]")]
public class SchemaController<TEntity> : ControllerBase
    where TEntity : IEntity
{
    private readonly IEntityDefinitionGraphFactory entityDefinitionFactory;

    public SchemaController(IEntityDefinitionGraphFactory entityDefinitionFactory)
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
            @id = $"{baseUrl}/schema/{entity.NodeKey.ToLowerInvariant()}",
        };

        return Ok(def);
    }
}
