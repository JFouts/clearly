// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using DomainModeling.Core;
using DomainModeling.Crud.Search;

namespace DomainModeling.Crud.Services;

public interface IEntityApiService<TEntity>
    where TEntity : IEntity
{
    Task<CrudSearchResult<TEntity>> Search(CrudSearchOptions searchOptions);
    Task<TEntity> GetById(Guid id);
    Task Create(TEntity value);
    Task Update(Guid id, TEntity value);
    Task Delete(Guid id);
}
