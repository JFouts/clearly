using DomainModeling.Crud.WebUi.Models;
using DomainModeling.Crud.WebUi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DomainModeling.Crud.WebUi.ViewComponents.FieldEditors;

[ViewComponent]
public class MultiSelectListFieldEditorViewComponent : FieldEditorViewComponent
{
    public override Task<IViewComponentResult> InvokeAsync(EditorFormFieldDefinition fieldDefintion)
    {
        return Task.FromResult<IViewComponentResult>(View(new InputFieldEditorViewModel {
            Id = fieldDefintion.FieldName,
            FieldName = fieldDefintion.FieldName,
            Label = fieldDefintion.DisplayName
        }));
    }
}
