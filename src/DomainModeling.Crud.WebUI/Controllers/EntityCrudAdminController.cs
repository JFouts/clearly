using DomainModeling.Core;
using DomainModeling.Crud.Services;
using DomainModeling.Crud.WebUi.Factories;
using Microsoft.AspNetCore.Mvc;

namespace DomainModeling.Crud.WebUi.Controllers;

[GenericEntityController]
[Route("admin/[type]")]
public class EntityCrudAdminController<T> : Controller where T : IEntity, new()
{
    private readonly IEntityApiService<T> _service;

    private readonly IEntityEditorViewModelFactory<T> _editorViewModelFactory;
    private readonly ISearchListViewModelFactory<T> _listViewModelFactory;

    public EntityCrudAdminController(IEntityApiService<T> service, ISearchListViewModelFactory<T> listViewModelFactory, IEntityEditorViewModelFactory<T> editorViewModelFactory)
    {
        _service = service;
        _listViewModelFactory = listViewModelFactory;
        _editorViewModelFactory = editorViewModelFactory;
    }

    [HttpGet("edit/{id}")]
    public async Task<IActionResult> GetEditForm(Guid id)
    {
        var entity = await _service.GetById(id);

        var viewModel = _editorViewModelFactory.Build(entity);

        return View("EditForm", viewModel);
    }

    [HttpGet("create")]
    public IActionResult GetCreateForm()
    {
        var entity = new T();

        var viewModel = _editorViewModelFactory.Build(entity);

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

        var viewModel = await _listViewModelFactory.Build(result, page, pageSize);

        return View(viewModel);
    }
}