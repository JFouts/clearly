using System.Collections;

namespace DomainModeling.Crud.Services;

public abstract class DataSource<T> : IDataSource<T>
{
    public abstract Task<IEnumerable<T>> Load();

    async Task<IEnumerable> IDataSource.Load()
    {
        return await Load();
    }
}
