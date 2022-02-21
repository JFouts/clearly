// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using DomainModeling.Core;
using DomainModeling.Crud.Search;

namespace DomainModeling.Crud.Services;

/// <summary>
/// Uses search functionality of CRUD on an Entity to retrieve all
/// entities of that type as a data source.
/// </summary>
public class EntityDataSource<TEntity> : DataSource<TEntity>
    where TEntity : IEntity
{
    private readonly IEntityApiService<TEntity> service;

    public EntityDataSource(IEntityApiService<TEntity> service)
    {
        this.service = service;
    }

    public override async Task<IEnumerable<TEntity>> Load()
    {
        var response = await service.Search(new CrudSearchOptions());

        return await response.Results.ToListAsync();
    }
}
