// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using DomainModeling.Crud.WebUi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DomainModeling.Crud.WebUi.ViewComponents.FieldEditors;

[ViewComponent]
public class DropDownListFieldEditorViewComponent : FieldEditorViewComponent
{
    private readonly IDataSourceFactory dataSourceFactory;
    private readonly IDataSourceReader<KeyValuePair<string, string>> dataSourceReader;

    public DropDownListFieldEditorViewComponent(IDataSourceFactory dataSourceFactory, IDataSourceReader<KeyValuePair<string, string>> dataSourceReader)
    {
        this.dataSourceFactory = dataSourceFactory;
        this.dataSourceReader = dataSourceReader;
    }

    public override async Task<IViewComponentResult> InvokeAsync(EntityFieldDefinition fieldDefinition, object value)
    {
        var metadata = fieldDefinition.UsingMetadata<CrudAdminEntityFieldMetadata>();

        if (!metadata.EditorProperties.TryGetValue("DataSource", out var dataSourceDefinition))
        {
            throw new ArgumentNullException("DataSource");
        }

        var dataSource = dataSourceFactory.Create(dataSourceDefinition);
        var data = await dataSourceReader.ReadFrom(dataSource);

        return View(new DropDownFieldEditorViewModel
        {
            Id = fieldDefinition.Property.Name,
            FieldName = fieldDefinition.Property.Name,
            Label = fieldDefinition.DisplayName,
            Value = value?.ToString() ?? string.Empty,
            Options = data.Select(x => new DropDownOptionViewModel
            {
                Value = x.Key,
                Label = x.Value,
            }),
        });
    }
}
