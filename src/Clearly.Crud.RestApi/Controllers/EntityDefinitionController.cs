// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.Models.EntityGraph;
using Microsoft.AspNetCore.Mvc;

namespace Clearly.Crud.RestApi;

[Route("api/entity")]
public class EntityDefinitionController : ControllerBase
{
    private readonly ITypeProvider entityTypeProvider;
    private readonly IEntityDefinitionGraphFactory entityDefinitionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityDefinitionController"/> class.
    /// </summary>
    /// <param name="entityTypeProvider"></param>
    /// <param name="entityDefinitionFactory"></param>
    public EntityDefinitionController(ITypeProvider entityTypeProvider, IEntityDefinitionGraphFactory entityDefinitionFactory)
    {
        this.entityTypeProvider = entityTypeProvider;
        this.entityDefinitionFactory = entityDefinitionFactory;
    }

    [HttpGet]
    public IActionResult GetAllEntities()
    {
        var supportedTypes = entityTypeProvider.GetTypes()
            .Select(x => entityDefinitionFactory.CreateForType(x))
            .Select(x => new { x.NodeKey, x.DisplayName })
            .ToList();

        return Ok(supportedTypes);
    }

    [HttpGet("{nameKey}")]
    public IActionResult GetByNameKey([FromRoute] string nameKey)
    {
        // TODO: This lookup is garbage, it should be persisted somewhere
        var supportedType = entityTypeProvider.GetTypes()
            .Select(x => entityDefinitionFactory.CreateForType(x))
            .OfType<EntityTypeDefinitionNode>()
            .FirstOrDefault(x => string.Equals(x.NodeKey, nameKey, StringComparison.InvariantCultureIgnoreCase));

        if (supportedType == null)
        {
            return NotFound();
        }

        return Ok(entityDefinitionFactory.Flatten(supportedType));
    }
}
