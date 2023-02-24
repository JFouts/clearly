using Microsoft.AspNetCore.Components;
using Clearly.Crud.EntityGraph;
using Clearly.Crud.WebUi.Core.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Clearly.Crud.WebUi.Client.Pages;

public abstract class EntityEditorBase : ComponentBase
{
    [Inject]
    private NavigationManager NavigationManager { get; set; }
    
    [Inject]
    private IEntityDefinitionApiService EntityDefinitionService { get; set; }

    [Parameter]
    public string EntityName { get; set; } = string.Empty;

    [Parameter]
    public string Id { get; set; } = Guid.Empty.ToString();

    protected Dictionary<string, TypeDefinitionNodeFlattened> TypeDefinitionGraph { get; set; } = new Dictionary<string, TypeDefinitionNodeFlattened>();

    protected TypeDefinitionNodeFlattened EntityDefinition { get; set; } = new TypeDefinitionNodeFlattened();

    public JObject Entity { get; set; } = new JObject();

    public Dictionary<string, object> GetParameters(PropertyDefinitionNodeFlattened fieldDefinition)
    {
        var key = fieldDefinition.NodeKey.ToCamelCase();
        var property = Entity.Property(key);

        if (property == null)
        {
            property = new JProperty(key, JValue.CreateNull());
            Entity.Add(property);
        }

        return new Dictionary<string, object>
        {
            { "Input", property },
            { "PropertyDefinitionNode", fieldDefinition },
            { "TypeDefinitionGraph", TypeDefinitionGraph },
        };
    }

    protected Type GetComponentType(string type)
    {
        var componentType = this.GetType().Assembly.GetTypes().FirstOrDefault(x => x.Name == type);

        if (componentType == null)
        {
            throw new InvalidOperationException($"Cannot create component {type} because it does not exist.");
        }

        return componentType;
    }

    protected override async Task OnInitializedAsync()
    {
        var response = await EntityDefinitionService.GetById(EntityName);

        TypeDefinitionGraph = response;
        EntityDefinition = response[EntityName];
    }

    protected abstract Task<HttpResponseMessage> SaveJsonContent(StringContent json);

    protected async void OnSubmit()
    {
        // TODO: Processing Indicator

        var json = JsonConvert.SerializeObject(Entity);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await SaveJsonContent(content);

        if (response.IsSuccessStatusCode)
        {
            NavigationManager.NavigateTo($"/admin/entity/{EntityName}");
        }
        else
        {
            // TODO: Display Error
        }
    }
}
