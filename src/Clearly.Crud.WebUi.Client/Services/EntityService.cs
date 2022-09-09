using Clearly.Crud.Search;
using Clearly.Crud.Services;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Clearly.Crud.WebUi.Client.Services
{
    public interface IEntityApiService
    {
        Task<JObject> GetById(Guid id, string entityNameKey);
        Task Create(JObject value, string entityNameKey);
        Task Update(Guid id, JObject value, string entityNameKey);
        Task Delete(Guid id, string entityNameKey);
        Task<CrudSearchResult<JObject>> Search(CrudSearchOptions options, string entityNameKey);
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

        public async Task<JObject> GetById(Guid id, string entityNameKey)
        {
            return await Send<JObject>(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = Url($"/api/{entityNameKey}/{id}"),
            });
        }

        public async Task Create(JObject value, string entityNameKey)
        {
            await Send<JObject>(new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = Url($"/api/{entityNameKey}"),
                Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json"),
            });
        }

        public async Task Update(Guid id, JObject value, string entityNameKey)
        {
            await Send<JObject>(new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = Url($"/api/{entityNameKey}/{id}"),
                Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json"),
            });
        }

        public async Task Delete(Guid id, string entityNameKey)
        {
            await Send<JObject>(new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = Url($"/api/{entityNameKey}/{id}"),
            });
        }

        public async Task<CrudSearchResult<JObject>> Search(CrudSearchOptions options, string entityNameKey)
        {
            var queryParamaters = new Dictionary<string, string>();

            if (options.Skip > 0)
            {
                queryParamaters["skip"] = options.Skip.ToString();
            }

            if (options.Take > 0)
            {
                queryParamaters["take"] = options.Take.ToString();
            }

            if (!string.IsNullOrWhiteSpace(options.SearchQuery))
            {
                queryParamaters["searchQuery"] = options.SearchQuery;
            }

            var url = Url($"/api/{entityNameKey}");
            url = new Uri(QueryHelpers.AddQueryString(url.ToString(), queryParamaters));

            return await Send<CrudSearchResult<JObject>>(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = url,
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
}
