// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Net.Http.Json;
using Clearly.Core;
using Clearly.Crud.Search;

namespace Clearly.Crud.Services;

public class GenericEntityService
{
    private readonly HttpClient http;
    private readonly HostedCrudApiConfiguration config;

    public GenericEntityService(HttpClient http, HostedCrudApiConfiguration config)
    {
        this.http = http;
        this.config = config;
    }

    public async Task<EntityDefinition> GetDefinition(string type)
    {
        return (EntityDefinition) await Send(
            new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = Url($"/api/entites/{type}"),
            }, 
            typeof(EntityDefinition));
    }

    public async Task<object> GetById(Guid id, string typeKey)
    {
        return await Send(
            new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = Url($"/api/{typeKey}/{id}"),
            },
            null);
    }

    private Uri Url(string path)
    {
        return new Uri(new Uri(config.BaseUrl), path);
    }

    private async Task<object> Send(HttpRequestMessage request, Type type)
    {
        var response = await http.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var entity = await response.Content.ReadFromJsonAsync(type);

        if (entity == null)
        {
            // TODO: Better Exception
            throw new Exception();
        }

        return entity;
    }
}

// TODO: Pull URLs for entity from Entity Configuration
// TODO: Well defined exceptions that come from this class
// TODO: Security and customization of HTTP headers (Can a named HttpClient solve this?)
public class HostedEntityApiService<TEntity> : IEntityApiService<TEntity>
    where TEntity : IEntity
{
    private readonly HttpClient http;
    private readonly HostedCrudApiConfiguration config;

    public HostedEntityApiService(HttpClient http, HostedCrudApiConfiguration config)
    {
        this.http = http;
        this.config = config;
    }

    public async Task<TEntity> GetById(Guid id)
    {
        return await Send<TEntity>(new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = Url($"/api/{typeof(TEntity).Name}/{id}"),
        });
    }

    public async Task Create(TEntity value)
    {
        await Send<TEntity>(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = Url($"/api/{typeof(TEntity).Name}"),
            Content = JsonContent.Create(value),
        });
    }

    public async Task Update(Guid id, TEntity value)
    {
        await Send<TEntity>(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = Url($"/api/{typeof(TEntity).Name}/{id}"),
            Content = JsonContent.Create(value),
        });
    }

    public async Task Delete(Guid id)
    {
        await Send<TEntity>(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = Url($"/api/{typeof(TEntity).Name}/{id}"),
        });
    }

    public async Task<CrudSearchResult<TEntity>> Search(CrudSearchOptions searchOptions)
    {
        // TODO: Convert search options into query paramaters
        return await Send<CrudSearchResult<TEntity>>(new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = Url($"/api/{typeof(TEntity).Name}"),
        });
    }

    private Uri Url(string path)
    {
        return new Uri(new Uri(config.BaseUrl), path);
    }

    private async Task<T> Send<T>(HttpRequestMessage request)
    {
        var response = await http.SendAsync(request);

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
