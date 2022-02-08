using Microsoft.AspNetCore.Mvc;

namespace DomainModeling.Crud.AddControllers;

[GenericEntityRoute]
public class EntityCrudController<T> : ControllerBase {
    private readonly ICrudService<T> _service;

    public EntityCrudController(ICrudService<T> service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Search()
    {
        return Ok(_service.Search(new CrudSearchOptions()));
    }

    [HttpGet("{id}")]
    public async Task<T> Get(Guid id)
    {
        return await _service.GetById(id);
    }

    [HttpPost]
    public async Task Post([FromBody] T value)
    {
        await _service.Insert(value);
    }

    [HttpPut("{id}")]
    public async Task Post(Guid id, [FromBody] T value)
    {
        await _service.Update(id, value);
    }

    [HttpDelete("{id}")]
    public async Task Post(Guid id)
    {
        await _service.Delete(id);
    }
}