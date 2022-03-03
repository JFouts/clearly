// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using Clearly.Core;
using Clearly.Crud.JsonLd;
using Clearly.Crud.Search;
using Microsoft.AspNetCore.Mvc;

namespace Clearly.Crud.RestApi.Controllers;

[GenericEntityController]
[Route("api/[type]")]
public class EntityCrudController<TEntity> : ControllerBase
    where TEntity : IEntity
{
    private readonly ICrudService<TEntity> service;

    public EntityCrudController(ICrudService<TEntity> service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Search()
    {
        return Ok(await service.Search(new CrudSearchOptions()));
    }
    
    [JsonLdFormatter]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var entity = await service.GetById(id);

        return Ok(entity);
    }

    [HttpPost]
    public async Task Post([FromBody] TEntity value)
    {
        await service.Insert(value);
    }

    [HttpPut("{id}")]
    public async Task Post(Guid id, [FromBody] TEntity value)
    {
        await service.Update(id, value);
    }

    [HttpDelete("{id}")]
    public async Task Post(Guid id)
    {
        await service.Delete(id);
    }
}
