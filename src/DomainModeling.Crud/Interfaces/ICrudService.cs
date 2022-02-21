// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using DomainModeling.Crud.Search;

namespace DomainModeling.Crud;

public interface ICrudService<T> : IService
{
    Task<CrudSearchResult<T>> Search(CrudSearchOptions options);
    Task<T> GetById(Guid id);
    Task Update(Guid id, T obj);
    Task Insert(T obj);
    Task Delete(Guid id);
}
