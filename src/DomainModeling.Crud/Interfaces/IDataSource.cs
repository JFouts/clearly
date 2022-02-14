using System.Collections;

namespace DomainModeling.Crud.Services;

public interface IDataSource<T> : IDataSource
{
    new Task<IEnumerable<T>> Load();
}

public interface IDataSource
{
    Task<IEnumerable> Load();
}