// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Clearly.Crud.WebUi.ViewComponents.FieldEditors;

public class DataSourceFactory : IDataSourceFactory
{
    private readonly IServiceProvider serviceLocator;

    public DataSourceFactory(IServiceProvider serviceLocator)
    {
        this.serviceLocator = serviceLocator;
    }

    public IDataSource Create(object sourceDefinition)
    {
        switch (sourceDefinition)
        {
            case Type dataSourceType:
                var dataSource = serviceLocator.GetRequiredService(dataSourceType) as IDataSource;

                if (dataSource == null)
                {
                    throw new Exception();
                }

                return dataSource;
            case string dataListText:
                const string recordSeperator = ";";
                const string valueSeperator = ",";

                var data = new List<KeyValuePair<string, string>>();
                var records = dataListText.Split(recordSeperator);

                foreach (var record in records)
                {
                    var values = record.Split(valueSeperator);

                    if (values.Any())
                    {
                        var key = values.First();
                        var value = values.ElementAtOrDefault(1) ?? values.First();

                        data.Add(new KeyValuePair<string, string>(key, value));
                    }
                }

                return new StaticListDataSource(data);
        }

        throw new Exception();
    }
}
