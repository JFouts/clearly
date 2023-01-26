// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.Services;

namespace Clearly.Crud.WebUi.Core;

public class DataSourceFactory : IDataSourceFactory
{
    //private readonly IServiceProvider serviceLocator;
    //private readonly IEntityDefinitionGraphFactory entityDefinitionFactory;
    //private readonly ITypeProvider entityTypeProvider;

    //public DataSourceFactory(IServiceProvider serviceLocator, IEntityDefinitionGraphFactory entityDefinitionFactory, ITypeProvider entityTypeProvider)
    //{
    //    this.serviceLocator = serviceLocator;
    //    this.entityDefinitionFactory = entityDefinitionFactory;
    //    this.entityTypeProvider = entityTypeProvider;
    //}

    public IDataSource Create(string dataSourceType, string dataSource)
    {
        switch (dataSourceType)
        {
            //case "EntityDataSource":
                // TODO: We should have a way to get the entity defintion from an entities NameKey
                //var entityType = entityTypeProvider.GetTypes().FirstOrDefault(x => string.Equals(x.Name, dataSource, StringComparison.InvariantCultureIgnoreCase)); 

                //if (entityType == null)
                //{
                //    throw new ArgumentException($"Entity of type {dataSource} could not be found.", nameof(dataSource));
                //}

                //var definition = entityDefinitionFactory.CreateForType(entityType);

                //var dataSourceObject = serviceLocator.GetRequiredService(typeof(EntityDataSource<>).MakeGenericType(definition.ObjectType)) as IDataSource;

                //if (dataSourceObject == null)
                //{
                //    throw new Exception();
                //}

                //return dataSourceObject;
            case "StaticList":
                const string recordSeperator = ";";
                const string valueSeperator = ",";

                var data = new List<KeyValuePair<string, string>>();
                var records = dataSource.Split(recordSeperator);

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
            default:
                throw new NotSupportedException($"Data Source type of {dataSourceType} is not supported by {nameof(DataSourceFactory)}");
        }
    }
}
