// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.WebUi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Clearly.Crud.WebUi.ViewComponents.FieldEditors;

[ViewComponent]
public class DropDownListFieldEditor : FieldEditorViewComponent
{
    private readonly IDataSourceFactory dataSourceFactory;
    private readonly IDataSourceReader<KeyValuePair<string, string>> dataSourceReader;

    public DropDownListFieldEditor(IDataSourceFactory dataSourceFactory, IDataSourceReader<KeyValuePair<string, string>> dataSourceReader)
    {
        this.dataSourceFactory = dataSourceFactory;
        this.dataSourceReader = dataSourceReader;
    }

    public override async Task<IViewComponentResult> InvokeAsync(FieldDefinition fieldDefinition, object value)
    {
        var metadata = fieldDefinition.Using<CrudAdminFieldFeature>();
        var dropDownFeature = fieldDefinition.Using<CrudAdminDropDownFeature>();

        ArgumentNullException.ThrowIfNull(dropDownFeature.DataSource, nameof(dropDownFeature.DataSource));
        ArgumentNullException.ThrowIfNull(dropDownFeature.DataSourceType, nameof(dropDownFeature.DataSourceType));

        var dataSource = dataSourceFactory.Create(dropDownFeature.DataSourceType, dropDownFeature.DataSource);
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
