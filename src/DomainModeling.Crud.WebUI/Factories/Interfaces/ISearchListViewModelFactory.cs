// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using DomainModeling.Crud.Search;
using DomainModeling.Crud.WebUi.ViewModels;

namespace DomainModeling.Crud.WebUi.Factories;

public interface ISearchListViewModelFactory<TEntity>
{
    Task<ListViewModel> Build(CrudSearchResult<TEntity> result, int page, int pageSize);
}
