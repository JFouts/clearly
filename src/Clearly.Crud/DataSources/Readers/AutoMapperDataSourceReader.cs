// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections;
using AutoMapper;
using Clearly.Crud.Services;
using Clearly.Crud.WebUi.Core;

namespace Clearly.Crud.WebUi;

// TODO: Can we do something like this without AutoMapper? Is it worth having this dependency?

public class AutoMapperDataSourceReader<TModel> : IDataSourceReader<TModel>
{
    private readonly IMapper mapper;

    public AutoMapperDataSourceReader(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public async Task<IEnumerable<TModel>> ReadFrom(IDataSource dataSource)
    {
        var results = await dataSource.Load();

        return Map(results).ToArray();
    }

    private IEnumerable<TModel> Map(IEnumerable source)
    {
        foreach (var obj in source)
        {
            yield return mapper.Map<TModel>(obj);
        }
    }
}
