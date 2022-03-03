// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core;
using Clearly.Crud.Search;
using Clearly.Crud.Services;
using Clearly.Crud.WebUi.Factories;
using Microsoft.AspNetCore.Mvc;

namespace Clearly.Crud.WebUi.Controllers;

[GenericEntityController]
[Route("admin/[type]")]
public class EntityCrudAdminController<T> : Controller
    where T : IEntity, new()
{
    private readonly IEntityApiService<T> service;

    private readonly IEntityEditorViewModelFactory<T> editorViewModelFactory;
    private readonly ISearchListViewModelFactory<T> listViewModelFactory;

    public EntityCrudAdminController(IEntityApiService<T> service, ISearchListViewModelFactory<T> listViewModelFactory, IEntityEditorViewModelFactory<T> editorViewModelFactory)
    {
        this.service = service;
        this.listViewModelFactory = listViewModelFactory;
        this.editorViewModelFactory = editorViewModelFactory;
    }

    [HttpGet("edit/{id}")]
    public async Task<IActionResult> GetEditForm(Guid id)
    {
        var entity = await service.GetById(id);

        var viewModel = editorViewModelFactory.Build(entity);

        return View("EditForm", viewModel);
    }

    [HttpGet("create")]
    public IActionResult GetCreateForm()
    {
        var entity = new T();

        var viewModel = editorViewModelFactory.Build(entity);

        return View("CreateForm", viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> ListView([FromQuery] int page = 1, [FromQuery] int pageSize = 24)
    {
        var entity = typeof(T);

        var result = await service.Search(new CrudSearchOptions
        {
            Skip = (page - 1) * pageSize,
            Take = pageSize,
        });

        var viewModel = await listViewModelFactory.Build(result, page, pageSize);

        return View(viewModel);
    }
}
