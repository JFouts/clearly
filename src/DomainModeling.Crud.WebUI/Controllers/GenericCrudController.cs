using DomainModeling.Crud.WebUi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DomainModeling.Crud.WebUi.Controllers;

[GenericEntityRoute]
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

    private EntityFormDefinition CreateEntityFormDefinitionFromType(Type entity)
    {
        return new EntityFormDefinition
        {
            DisplayName = entity.Name,
            EntityName = entity.Name,
            Fields = entity
                .GetProperties()
                .Select(x => new EditorFormFieldDefinition
                {
                    DisplayName = x.Name,
                    FieldName = x.Name
                }).ToList()
        };
    }
}