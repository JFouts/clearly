// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using DomainModeling.Crud.Services;

namespace DomainModeling.Crud.WebUi.ViewComponents.FieldEditors;

public interface IDataSourceReader<TModel>
{
    Task<IEnumerable<TModel>> ReadFrom(IDataSource dataSource);
}
