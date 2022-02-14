using DomainModeling.Core;

namespace DomainModeling.Crud.Services;

public class StaticListDataSource : DataSource<KeyValuePair<string, string>>
{
    public readonly IEnumerable<KeyValuePair<string, string>> _data;

    public StaticListDataSource(IEnumerable<KeyValuePair<string, string>> data)
    {
        _data = data;
    }

    public override Task<IEnumerable<KeyValuePair<string, string>>> Load()
    {
        return Task.FromResult(_data);
    }
}
