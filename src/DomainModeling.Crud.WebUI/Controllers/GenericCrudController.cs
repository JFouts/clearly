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
    public IActionResult ListView([FromQuery] int page = 0, [FromQuery] int pageSize = 24)
    {
        var entity = typeof(T);

        var viewModel = CreateEntityFormDefinitionFromType(entity);

        return View(viewModel);
    }

    private EntityFormDefinition CreateEntityFormDefinitionFromType(Type entity)
    {
        return new EntityFormDefinition
        {
            DisplayName = SplitCamelCase(entity.Name),
            EntityName = LowerCamelCase(entity.Name),
            DataSourceUrl = $"/{entity.Name.ToLower()}",
            Fields = entity
                .GetProperties()
                .Select(CreateEditorFormFieldDefinition)
                .ToList()
        };
    }

    private EditorFormFieldDefinition CreateEditorFormFieldDefinition(PropertyInfo prop)
    {
        var defintion = new EditorFormFieldDefinition
        {
            DisplayName = SplitCamelCase(prop.Name),
            FieldName = LowerCamelCase(prop.Name),
        };

        var attribute = (FieldViewComponentAttribute?) Attribute.GetCustomAttribute(prop, typeof(FieldViewComponentAttribute));

        if (attribute != null)
        {
            defintion.FieldType = new EditorFormFieldType
            {
                FieldEditorName = attribute.ViewComponentName
            };
        }

        return defintion;
    }

    public static string SplitCamelCase(string str )
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

        return str;
    }
}