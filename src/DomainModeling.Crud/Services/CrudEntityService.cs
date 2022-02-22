// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Runtime.CompilerServices;
using DomainModeling.Core;
using DomainModeling.Crud.Search;

[assembly: InternalsVisibleTo("DomainModeling.Crud.Test.Unit")]

namespace DomainModeling.Crud.Services;

// TODO: Well defined exceptions that come from this class
internal class CrudEntityService<T> : ICrudService<T>
    where T : IEntity
{
    private readonly IEntityRepository<T> repository;

    public CrudEntityService(IEntityRepository<T> repository)
    {
        this.repository = repository;
    }

    public async Task Delete(Guid id)
    {
        await repository.Delete(id);
    }

    public async Task<T> GetById(Guid id)
    {
        return await repository.GetById(id);
    }

    public async Task Insert(T obj)
    {
        if (obj.Id == Guid.Empty)
        {
            obj.Id = Guid.NewGuid();
        }

        await repository.Insert(obj);
    }

    public async Task<CrudSearchResult<T>> Search(CrudSearchOptions options)
    {
        return await repository.Search(options);
    }

    public async Task Update(Guid id, T obj)
    {
        await repository.Update(id, obj);
    }
}
