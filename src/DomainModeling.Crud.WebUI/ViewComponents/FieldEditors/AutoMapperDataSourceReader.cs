using DomainModeling.Crud.Services;

using AutoMapper;
using System.Collections;

namespace DomainModeling.Crud.WebUi.ViewComponents.FieldEditors;

public class AutoMapperDataSourceReader<TModel> : IDataSourceReader<TModel>
{
    private readonly IMapper _mapper;

    public AutoMapperDataSourceReader(IMapper mapper)
    {
        _mapper = mapper;
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
            yield return _mapper.Map<TModel>(obj);
        }
    }
}
