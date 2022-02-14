using System.Net.Http.Json;
using DomainModeling.Core;

namespace DomainModeling.Crud.Services;

// TODO: Pull URLs for entity from Entity Configuration

// TODO: Well defined exceptions that come from this class

// TODO: Security and customization of HTTP headers (Can a named HttpClient solve this?)

public class HostedEntityApiService<TEntity> : IEntityApiService<TEntity> where TEntity : IEntity
{
    public readonly HttpClient _http;
    public readonly HostedCrudApiConfiguration _config;

    public HostedEntityApiService(HttpClient http, HostedCrudApiConfiguration config)
    {
        _http = http;
        _config = config;
    }

    public async Task<TEntity> GetById(Guid id)
    {
        return await Send<TEntity>(new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri =  Url($"/api/{typeof(TEntity).Name}/{id}")
        });
    }

    public async Task Create(TEntity value)
    {
        await Send<TEntity>(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri =  Url($"/api/{typeof(TEntity).Name}"),
            Content = JsonContent.Create(value)
        });
    }

    public async Task Update(Guid id, TEntity value)
    {
        await Send<TEntity>(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri =  Url($"/api/{typeof(TEntity).Name}/{id}"),
            Content = JsonContent.Create(value)
        });
    }

    public async Task Delete(Guid id)
    {
        await Send<TEntity>(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri =  Url($"/api/{typeof(TEntity).Name}/{id}")
        });
    }

    public async Task<CrudSearchResult<TEntity>> Search()
    {
        return await Send<CrudSearchResult<TEntity>>(new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = Url($"/api/{typeof(TEntity).Name}")
        });
    }

    private Uri Url(string path)
    {
        return new Uri(new Uri(_config.BaseUrl), path);
    }

    private async Task<T> Send<T>(HttpRequestMessage request)
    {
        var response = await _http.SendAsync(request);

        response.EnsureSuccessStatusCode();
        
        var entity = await response.Content.ReadFromJsonAsync<T>();

        if (entity == null)
        {
            // TODO: Better Exception
            throw new Exception();    
        }

        return entity;
    }
}
