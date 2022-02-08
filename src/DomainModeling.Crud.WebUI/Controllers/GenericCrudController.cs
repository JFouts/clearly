using System.Reflection;
using System.Text.RegularExpressions;
using DomainModeling.Attributes.UI;
using DomainModeling.Crud.WebUi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DomainModeling.Crud.WebUi.Controllers;

[GenericEntityRoute]
[Route("admin")]
public class GenericCrudController<T> : Controller
{
    private readonly ICrudService<T> _service;

    public GenericCrudController(ICrudService<T> service)
    {
        _service = service;
    }

    [HttpGet("edit/{id}")]
    public IActionResult GetEditForm(string id)
    {
        var entity = typeof(T);

        var viewModel = CreateEntityFormDefinitionFromType(entity);

        return View("EditForm", viewModel);
    }

    [HttpGet("create")]
    public IActionResult GetCreateForm()
    {
        var entity = typeof(T);

        var viewModel = CreateEntityFormDefinitionFromType(entity);

        return View("CreateForm", viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> ListView([FromQuery] int page = 1, [FromQuery] int pageSize = 24)
    {
        var entity = typeof(T);

        var result = await _service.Search(new CrudSearchOptions {
            Skip = (page - 1) * pageSize,
            Take = pageSize
        });

        var results = await result.Results.ToListAsync();

        var viewModel = new EntitySearchModel {
            PageCount  = ((result.Count - 1) / pageSize) + 1,
            CurrentPage = page,
            FormDefinition = CreateEntityFormDefinitionFromType(entity),
            Results = results.OfType<object>().Select(ObjectToDictionary).ToList()
        };

        return View(viewModel);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var entity = typeof(T);

        var viewModel = CreateEntityFormDefinitionFromType(entity);

        return View(viewModel);
    }

    private Dictionary<string, object> ObjectToDictionary(object obj)
    {
        var data = new Dictionary<string, object>();

        foreach (var property in obj.GetType().GetProperties())
        {
            var value =  property.GetValue(obj);

            if (value != null)
            {
                data[LowerCamelCase(property.Name)] = value;
            }
        }

        return data;
    }

    private EntityFormDefinition CreateEntityFormDefinitionFromType(Type entity)
    {
        return new EntityFormDefinition
        {
            EntityType = entity,
            DisplayName = SplitCamelCase(entity.Name),
            EntityName = LowerCamelCase(entity.Name),
            DataSourceUrl = $"/{entity.Name.ToLower()}",
            Fields = entity
                .GetProperties()
                .Select(CreateEditorFormFieldDefinition)
                .ToList()
        };
    }

    private EditorFormFieldDefinition CreateEditorFormFieldDefinition(PropertyInfo property)
    {
        var defintion = new EditorFormFieldDefinition
        {
            DisplayName = SplitCamelCase(property.Name),
            FieldName = LowerCamelCase(property.Name),
        };

        var attribute = (FieldViewComponentAttribute?) Attribute.GetCustomAttribute(property, typeof(FieldViewComponentAttribute));

        if (attribute != null)
        {
            defintion.FieldType = new EditorFormFieldType
            {
                FieldEditorName = attribute.ViewComponentName
            };
        }

        return defintion;
    }

    public static string SplitCamelCase(string str)
    {
        return Regex.Replace( 
            Regex.Replace( 
                str, 
                @"(\P{Ll})(\P{Ll}\p{Ll})", 
                "$1 $2" 
            ), 
            @"(\p{Ll})(\P{Ll})", 
            "$1 $2" 
        );
    }    
    
    public static string LowerCamelCase(string str)
    {
        if (str?.Length > 0)
        {
            return Char.ToLower(str[0]) + str.Substring(1);
        }

        return str ?? string.Empty;
    }
}