// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core;
using Clearly.Crud.JsonLd;
using Clearly.Crud.Search;
using Microsoft.AspNetCore.Mvc;

namespace Clearly.Crud.RestApi;

/// <summary>
/// Manages the CRUD opprations for entites.
/// </summary>
/// <typeparam name="TEntity">The type of the entity object.</typeparam>
[GenericEntityController]
[Route("api/[type]")]
public class EntityCrudController<TEntity> : ControllerBase
    where TEntity : IEntity
{
    private readonly ICrudService<TEntity> service;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityCrudController{TEntity}"/> class.
    /// </summary>
    /// <param name="service">The service for applying business logic to the CRUD opperations.</param>
    public EntityCrudController(ICrudService<TEntity> service)
    {
        this.service = service;
    }

    /// <summary>
    /// Runs a search query against the collection of objects of a single type of entity.
    /// </summary>
    /// <param name="query">Search query.</param>
    /// <param name="skip">Skip the first X objects.</param>
    /// <param name="take">Return the next X objects.</param>
    /// <returns>The results for your search query.</returns>
    [HttpGet]
    public async Task<IActionResult> Search(
        [FromQuery] string query = "",
        [FromQuery] int skip = 0,
        [FromQuery] int take = 24)
    {
        return Ok(await service.Search(new CrudSearchOptions
        {
            SearchQuery = query,
            Skip = skip,
            Take = take,
        }));
    }
    
    /// <summary>
    /// Retrievs a single entity object by it's id.
    /// </summary>
    /// <param name="id">The id of the entity object to retrieve.</param>
    /// <returns>The entity object for the passed id.</returns>
    [JsonLdFormatter]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var entity = await service.GetById(id);

        return Ok(entity);
    }

    /// <summary>
    /// Creates a new entity object.
    /// </summary>
    /// <param name="value">The values for the state of the entity to be created.</param>
    /// <returns>A refrence link for the entity object that was created.</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TEntity value)
    {
        if (value is null)
        {
            return BadRequest("value is required");
        }

        await service.Insert(value);

        // TODO: Return some reference to the inserted record
        return Ok();
    }

    /// <summary>
    /// Updates a single entity object by it's ID.
    /// </summary>
    /// <param name="id">The ID of the entity object to update.</param>
    /// <param name="value">The values for the updated state of the entity object.</param>
    /// <returns>A refrence link for the entity object that was updated.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] TEntity value)
    {
        await service.Update(id, value);

        // TODO: Return some reference to the inserted record
        return Ok();
    }

    /// <summary>
    /// Deletes a single entity object by it's ID.
    /// </summary>
    /// <param name="id">The ID of the entity object to delete.</param>
    /// <returns>An empty response body.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await service.Delete(id);

        return Ok();
    }
}
