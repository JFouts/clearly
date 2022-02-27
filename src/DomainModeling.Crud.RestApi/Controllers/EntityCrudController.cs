// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DomainModeling.Core;
using DomainModeling.Crud.JsonLd;
using DomainModeling.Crud.Search;
using Microsoft.AspNetCore.Mvc;

namespace DomainModeling.Crud.RestApi.Controllers;

[Route("[controller]")]
public class SchemaController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var def = new HydraClass
        {
            Id = $"/schema",
            Description = "",
            // Title
        };

        return Ok(def);
    }
}

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
    public async Task<IActionResult> Index()
    {
        var baseUrl = "";
        var entity = entityDefinitionFactory.CreateForType(typeof(TEntity));
        
        var def = new HydraClass
        {
            Id = $"{baseUrl}/api/doc/{entity.NameKey}",
            Description = "",
            // Title
        };

        return Ok(def);
    }
}

public class HydraClass : HydraBase
{
    public override string Context => "http://www.w3.org/ns/hydra/context.jsonld";
    public override string Type => "Class";
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IEnumerable<HydraProperty> SupportedProperty = new List<HydraProperty>();
    public IEnumerable<HydraProperty> SupportedOperation = new List<HydraProperty>();
}

public abstract class JsonLdBase
{
    [JsonPropertyName("@context")]
    [JsonPropertyOrder(-3)]
    public abstract string Context { get; }

    [JsonPropertyOrder(-2)]
    [JsonPropertyName("@id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyOrder(-1)]
    [JsonPropertyName("@type")]
    public abstract string Type { get; }
}

public class HydraProperty : HydraBase
{
    public override string Type => "SupportedProperty";
    public string Property { get; set; } = string.Empty;
    public bool Required { get; set; }
    public bool Readable { get; set; }
    public bool Writable { get; set; }
}

public abstract class HydraBase : JsonLdBase
{
    public override string Context => "http://www.w3.org/ns/hydra/context.jsonld";
}

public class Opperation : HydraBase
{
    public override string Type => "Operation";
    public string Title { get; set; } = string.Empty;
    public string Method { get; set; } = string.Empty;
    public string Expects { get; set; } = string.Empty;
    public string Returns { get; set; } = string.Empty;
    public IEnumerable<string> PossibleStatus { get; set; } = new List<string>();
}


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
