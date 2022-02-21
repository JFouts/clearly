// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using DomainModeling.Crud.Search;
using Microsoft.AspNetCore.Mvc;

namespace DomainModeling.Crud.RestApi.Controllers;

[GenericEntityController]
[Route("api/[type]")]
public class EntityCrudController<T> : ControllerBase
{
    private readonly ICrudService<T> service;

    public EntityCrudController(ICrudService<T> service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Search()
    {
        return Ok(await service.Search(new CrudSearchOptions()));
    }

    [HttpGet("{id}")]
    public async Task<T> Get(Guid id)
    {
        return await service.GetById(id);
    }

    [HttpPost]
    public async Task Post([FromBody] T value)
    {
        await service.Insert(value);
    }

    [HttpPut("{id}")]
    public async Task Post(Guid id, [FromBody] T value)
    {
        await service.Update(id, value);
    }

    [HttpDelete("{id}")]
    public async Task Post(Guid id)
    {
        await service.Delete(id);
    }
}
