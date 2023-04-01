// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.Services;

namespace Clearly.Crud.WebUi;

public interface IDataSourceFactory
{
    Task<IDataSource> Create(string dataSourceType, string dataSource);
}
