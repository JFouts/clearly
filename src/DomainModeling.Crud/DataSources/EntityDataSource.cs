using DomainModeling.Core;

namespace DomainModeling.Crud.Services;

/// <summary>
/// Uses search functionality of CRUD on an Entity to retrieve all
/// entities of that type as a data source.
/// </summary>
public class EntityDataSource<TEntity> : DataSource<TEntity> where TEntity : IEntity
{
    private readonly IEntityApiService<TEntity> _service;

    public EntityDataSource(IEntityApiService<TEntity> service)
    {
        _service = service;
    }

    public override async Task<IEnumerable<TEntity>> Load()
    {
        var response = await _service.Search();

        return await response.Results.ToListAsync();
    }
}
