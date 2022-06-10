// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core;
using Clearly.Crud.Search;
using Clearly.Crud.WebUi.ViewModels;

namespace Clearly.Crud.WebUi.Factories;

/// <summary>
/// Factory to create an <see cref="ListViewModel"/> from a <see cref="IEntity"/>.
/// </summary>
/// <typeparam name="TEntity">The entity type of your model.</typeparam>
public interface ISearchListViewModelFactory<TEntity>
    where TEntity : IEntity
{
    /// <summary>
    /// Creates an <see cref="ListViewModel"/> from a <see cref="IEntity"/>.
    /// </summary>
    /// <param name="result">The search result model.</param>
    /// <param name="page">The search result page number.</param>
    /// <param name="pageSize">The size of each search result page.</param>
    /// <returns>The view model for the entity.</returns>
    Task<ListViewModel> Build(CrudSearchResult<TEntity> result, int page, int pageSize);
}
