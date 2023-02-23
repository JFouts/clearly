// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.Services;
using Clearly.Crud.WebUi.Core.Services;

namespace Clearly.Crud.WebUi.Core;

public class DataSourceFactory : IDataSourceFactory
{
    private readonly IEntityDefinitionApiService _entityDefinitionApiService;
    private readonly IEntityApiService _entityApiService;

    public DataSourceFactory(IEntityDefinitionApiService entityDefinitionApiService, IEntityApiService entityApiService)
    {
        _entityDefinitionApiService = entityDefinitionApiService;
        _entityApiService = entityApiService;
    }

    public async Task<IDataSource> Create(string dataSourceType, string dataSource)
    {
        switch (dataSourceType)
        {
            case "EntityDataSource":
                // TODO: We should have a way to get the entity definition from an entities NameKey
                var definition =await  _entityDefinitionApiService.GetById(dataSource.ToLowerInvariant()); 

                if (definition == null)
                {
                   throw new ArgumentException($"Entity definition of type {dataSource} could not be found.", nameof(dataSource));
                }

                return new FuncDataSource<KeyValuePair<string,string>>(async () =>
                {
                    var results = await _entityApiService.Search(new Search.CrudSearchOptions(), definition.NodeKey);
                    
                    return results.Results.Select(x => new KeyValuePair<string, string>(x["id"]?.ToString() ?? string.Empty, x["name"]?.ToString() ?? string.Empty));
                });

            case "StaticList":
                const string recordSeparator = ";";
                const string valueSeparator = ",";

                var data = new List<KeyValuePair<string, string>>();
                var records = dataSource.Split(recordSeparator);

                foreach (var record in records)
                {
                    var values = record.Split(valueSeparator);

                    if (values.Any())
                    {
                        var key = values.First();
                        var value = values.ElementAtOrDefault(1) ?? values.First();

                        data.Add(new KeyValuePair<string, string>(key, value));
                    }
                }

                return new StaticListDataSource(data);
            default:
                throw new NotSupportedException($"Data Source type of {dataSourceType} is not supported by {nameof(DataSourceFactory)}");
        }
    }
}
