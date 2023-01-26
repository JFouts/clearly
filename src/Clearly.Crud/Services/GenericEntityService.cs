// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Net.Http.Json;
using Clearly.Crud.Models.EntityGraph;

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

    public async Task<EntityTypeDefinitionNode> GetDefinition(string type)
    {
        return (EntityTypeDefinitionNode)await Send(
            new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = Url($"/api/entities/{type}"),
            }, 
            typeof(EntityTypeDefinitionNode));
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
