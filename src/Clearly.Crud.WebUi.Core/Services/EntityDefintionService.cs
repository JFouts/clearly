using Clearly.Crud.EntityGraph;
using Clearly.Crud.Services;
using Newtonsoft.Json;

namespace Clearly.Crud.WebUi.Core.Services;

public class EntityDefinitionApiService : IEntityDefinitionApiService
{
    private readonly HttpClient http;
    private readonly HostedCrudApiConfiguration config;
    
    public EntityDefinitionApiService(HttpClient http, HostedCrudApiConfiguration config)
    {
        this.http = http;
        this.config = config;
    }

    public async Task<TypeDefinitionNodeFlattened> GetById(string entityNameKey)
    {
        var definitions = await Send<Dictionary<string, TypeDefinitionNodeFlattened>>(new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = Url($"/api/entity/{entityNameKey}"),
        });

        // TODO: Error handling
        return definitions[entityNameKey];
    }

    // TODO: Move into base class
    private Uri Url(string path)
    {
        return new Uri(new Uri(config.BaseUrl), path);
    }

    private async Task<T> Send<T>(HttpRequestMessage request)
    {
        var response = await http.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        if (json == null)
        {
            // TODO: Better Exception
            throw new Exception();
        }

        var entity = JsonConvert.DeserializeObject<T>(json);

        if (entity == null)
        {
            // TODO: Better Exception
            throw new Exception();
        }

        return entity;
    }
}
