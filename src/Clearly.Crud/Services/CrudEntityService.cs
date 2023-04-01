// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Runtime.CompilerServices;
using Clearly.Core;
using Clearly.Crud.Search;

// TODO: Move this elsewhere
[assembly: InternalsVisibleTo("Clearly.Crud.Test.Unit")]

namespace Clearly.Crud.Services;

/// <inheritdoc cref="ICrudService{TEntity}"/>
// TODO: Well defined exceptions that come from this class
public class CrudEntityService<T> : ICrudService<T>
    where T : IEntity
{
    private readonly IEntityRepository<T> repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CrudEntityService{T}"/> class.
    /// </summary>
    /// <param name="repository">The read/write repository for the Entity.</param>
    public CrudEntityService(IEntityRepository<T> repository)
    {
        this.repository = repository;
    }

    /// <inheritdoc/>
    public async Task<CrudSearchResult<T>> Search(CrudSearchOptions options)
    {
        return await repository.Search(options);
    }

    /// <inheritdoc/>
    public async Task<T> GetById(Guid id)
    {
        return await repository.GetById(id);
    }

    /// <inheritdoc/>
    public async Task Insert(T obj)
    {
        if (obj.Id == Guid.Empty)
        {
            obj.Id = Guid.NewGuid();
        }

        await repository.Insert(obj);
    }

    /// <inheritdoc/>
    public async Task Update(Guid id, T obj)
    {
        await repository.Update(id, obj);
    }

    /// <inheritdoc/>
    public async Task Delete(Guid id)
    {
        await repository.Delete(id);
    }
}
