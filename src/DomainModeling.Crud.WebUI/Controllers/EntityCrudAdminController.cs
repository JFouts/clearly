using System.Reflection;
using DomainModeling.Attributes.UI;
using DomainModeling.Core;
using DomainModeling.Crud.WebUi.Extensions;
using DomainModeling.Crud.WebUi.Factories;
using DomainModeling.Crud.WebUi.Models;
using DomainModeling.Crud.WebUi.Utilities;
using DomainModeling.Crud.WebUi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DomainModeling.Crud.WebUi.Controllers;

[GenericEntityController]
[Route("admin/[type]")]
public class EntityCrudAdminController<T> : Controller where T : IEntity, new()
{
    private readonly ICrudService<T> _service;

    private readonly IFieldDefinitionFactory _fieldDefintionFactory;
    private readonly IListViewModelFactory<T> _listViewModelFactory;

    public EntityCrudAdminController(ICrudService<T> service, IFieldDefinitionFactory fieldDefintionFactory, IListViewModelFactory<T> listViewModelFactory)
    {
        _service = service;
        _fieldDefintionFactory = fieldDefintionFactory;
        _listViewModelFactory = listViewModelFactory;
    }

    [HttpGet("edit/{id}")]
    public async Task<IActionResult> GetEditForm(Guid id)
    {
        var entity = await _service.GetById(id);

        return View("EditForm", new EntityEditViewModel {
            Definition = CreateEntityFormDefinitionFromType(entity),
            Record = entity.ToDictionary()
        });
    }

    [HttpGet("create")]
    public IActionResult GetCreateForm()
    {
        var entity = new T();

        return View("CreateForm", new EntityEditViewModel {
            Definition = CreateEntityFormDefinitionFromType(entity),
            Record = entity.ToDictionary()
        });
    }

    [HttpGet]
    public async Task<IActionResult> ListView([FromQuery] int page = 1, [FromQuery] int pageSize = 24)
    {
        var entity = typeof(T);

        var result = await _service.Search(new CrudSearchOptions {
            Skip = (page - 1) * pageSize,
            Take = pageSize
        });

        var viewModel = await _listViewModelFactory.Build(result, page, pageSize);

        return View(viewModel);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var entity = await _service.GetById(id);

        var viewModel = CreateEntityFormDefinitionFromType(entity);

        return View(viewModel);
    }

    private EntityFormDefinition CreateEntityFormDefinitionFromType(T entity)
    {
        var type = typeof(T);

        return new EntityFormDefinition
        {
            EntityType = type,
            DisplayName = type.Name.SplitCamelCase(),
            EntityName = type.Name.LowerCamelCase(),
            DataSourceUrl = $"/{type.Name.ToLower()}",
            Fields = type
                .GetProperties()
                .Select(CreateEditorFormFieldDefinition)
                .ToList()
        };
    }

    private EditorFormFieldDefinition CreateEditorFormFieldDefinition(PropertyInfo property)
    {
        var defintion = new EditorFormFieldDefinition
        {
            DisplayName = property.Name.SplitCamelCase(),
            FieldName = property.Name.LowerCamelCase(),
        };

        var editorAttribute = (FieldEditorAttribute?) Attribute.GetCustomAttribute(property, typeof(FieldEditorAttribute));
        var editorPropertiesAttributes = Attribute.GetCustomAttributes(property, typeof(FieldEditorPropertyAttribute)).OfType<FieldEditorPropertyAttribute>();

        if (editorAttribute != null)
        {
            defintion.FieldType = new EditorFormFieldType
            {
                FieldEditorName = editorAttribute.ViewComponentName,
                Properties = editorPropertiesAttributes.ToDictionary(x => x.Name, x => x.Value)
            };
        }

        return defintion;
    }
}