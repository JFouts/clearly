using DomainModeling.Crud.WebUi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DomainModeling.Crud.WebUi.ViewComponents.FieldEditors;

[ViewComponent]
public class DropDownListFieldEditorViewComponent : FieldEditorViewComponent
{
    private readonly IDataSourceFactory _dataSourceFactory;
    private readonly IDataSourceReader<KeyValuePair<string, string>> _dataSourceReader;

    public DropDownListFieldEditorViewComponent(IDataSourceFactory dataSourceFactory, IDataSourceReader<KeyValuePair<string, string>> dataSourceReader)
    {
        _dataSourceFactory = dataSourceFactory;
        _dataSourceReader = dataSourceReader;
    }

    public override async Task<IViewComponentResult> InvokeAsync(EntityFieldDefinition fieldDefinition, object value)
    {
        var metadata = fieldDefinition.UsingMetadata<CrudAdminEntityFieldMetadata>();

        if (!metadata.EditorProperties.TryGetValue("DataSource", out var dataSourceDefinition))
        {
            throw new ArgumentNullException("DataSource");
        }

        var dataSource = _dataSourceFactory.Create(dataSourceDefinition);
        var data = await _dataSourceReader.ReadFrom(dataSource);

        return View(new DropDownFieldEditorViewModel 
        {
            Id = fieldDefinition.Property.Name,
            FieldName = fieldDefinition.Property.Name,
            Label = fieldDefinition.DisplayName,
            Value = value?.ToString() ?? string.Empty,
            Options = data.Select(x => new DropDownOptionViewModel {
                Value = x.Key,
                Label = x.Value
            })
        });
    }
}
