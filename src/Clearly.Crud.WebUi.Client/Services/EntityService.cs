using Clearly.Crud.Search;
using Clearly.Crud.Services;
using System.Net.Http.Json;
using System.Text.Json;

namespace Clearly.Crud.WebUi.Client.Services
{
    public interface IEntityApiService
    {
        Task<JsonElement> GetById(Guid id, string entityNameKey);
        Task Create(JsonElement value, string entityNameKey);
        Task Update(Guid id, JsonElement value, string entityNameKey);
        Task Delete(Guid id, string entityNameKey);
        Task<CrudSearchResult<JsonElement>> Search(CrudSearchOptions options, string entityNameKey);
    }

    public class EntityApiService : IEntityApiService
    {
        private readonly HttpClient http;
        private readonly HostedCrudApiConfiguration config;

        public EntityApiService(HttpClient http, HostedCrudApiConfiguration config)
        {
            this.http = http;
            this.config = config;
        }

        public async Task<JsonElement> GetById(Guid id, string entityNameKey)
        {
            return await Send<JsonElement>(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = Url($"/api/{entityNameKey}/{id}"),
            });
        }

        public async Task Create(JsonElement value, string entityNameKey)
        {
            await Send<JsonElement>(new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = Url($"/api/{entityNameKey}"),
                Content = JsonContent.Create(value),
            });
        }

        public async Task Update(Guid id, JsonElement value, string entityNameKey)
        {
            await Send<JsonElement>(new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = Url($"/api/{entityNameKey}/{id}"),
                Content = JsonContent.Create(value),
            });
        }

        public async Task Delete(Guid id, string entityNameKey)
        {
            await Send<JsonElement>(new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = Url($"/api/{entityNameKey}/{id}"),
            });
        }

        public async Task<CrudSearchResult<JsonElement>> Search(CrudSearchOptions options, string entityNameKey)
        {
            var queryParamaters = new Dictionary<string, object>();

            if (options.Skip > 0)
            {
                queryParamaters["skip"] = options.Skip;
            }

            if (options.Take > 0)
            {
                queryParamaters["take"] = options.Take;
            }

            if (!string.IsNullOrWhiteSpace(options.SearchQuery))
            {
                queryParamaters["searchQuery"] = options.SearchQuery;
            }

            return await Send<CrudSearchResult<JsonElement>>(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = Url($"/api/{entityNameKey}"),
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
}
