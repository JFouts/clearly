using Microsoft.AspNetCore.Mvc;

namespace DomainModeling.Crud.AddControllers {
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
        public async Task<T> Get(string id)
        {
            return await _service.GetById(id);
        }
    
        [HttpPost]
        public async Task Post([FromBody] T value)
        {
            await _service.Insert(value);
        }

        [HttpPut("{id}")]
        public async Task Post(string id, [FromBody] T value)
        {
            await _service.Update(id, value);
        }

        [HttpDelete("{id}")]
        public async Task Post(string id)
        {
            await _service.Delete(id);
        }
    }
}
