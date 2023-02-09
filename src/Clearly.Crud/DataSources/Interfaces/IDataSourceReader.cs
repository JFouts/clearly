// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.Services;

namespace Clearly.Crud.WebUi.Core;

public interface IDataSourceReader<TModel>
{
    Task<IEnumerable<TModel>> ReadFrom(IDataSource dataSource);
}

//public class DataSourceReader<TModel> : IDataSourceReader<TModel>
//{
//    public async Task<IEnumerable<TModel>> ReadFrom(IDataSource dataSource)
//    {
//        var data = await dataSource.Load();

//        return data.OfType<TModel>();
//    }
//}