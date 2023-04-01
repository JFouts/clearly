// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core;
using Clearly.Crud.Search;

namespace Clearly.Crud;

/// <summary>
/// Service to handle business logic for CRUD opperations on an entity.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
public interface ICrudService<TEntity> : IService
    where TEntity : IEntity
{
    /// <summary>
    /// Queries the collection of entities of type <see cref="TEntity"/> to get a page
    /// of search results.
    /// </summary>
    /// <param name="options">Options for the search query.</param>
    /// <returns>Paged search results.</returns>
    Task<CrudSearchResult<TEntity>> Search(CrudSearchOptions options);

    /// <summary>
    /// Queries the collection of entities of type <typeparamref name="TEntity"/> to get a sinlge
    /// entity by it's ID.
    /// </summary>
    /// <param name="id">The ID of the Entity being retrieved.</param>
    /// <returns>The Entity with ID of <paramref name="id"/>.</returns>
    Task<TEntity> GetById(Guid id);

    /// <summary>
    /// Commits an update for a single entity of type <typeparamref name="TEntity"/> by it's ID.
    /// </summary>
    /// <param name="id">The ID of the Entity being updated.</param>
    /// <param name="obj">The state of the new Entity.</param>
    Task Update(Guid id, TEntity obj);

    /// <summary>
    /// Commits a new single entity of type <typeparamref name="TEntity"/>.
    /// </summary>
    /// <param name="obj">The state of the new Entity.</param>
    /// <remarks>The service will generate a new ID for the Entity if not provided.</remarks>
    Task Insert(TEntity obj);

    /// <summary>
    /// Commits a delete for a single entity of type <typeparamref name="TEntity"/> by it's ID.
    /// </summary>
    /// <param name="id">The ID of the Entity being deleted.</param>
    Task Delete(Guid id);
}
