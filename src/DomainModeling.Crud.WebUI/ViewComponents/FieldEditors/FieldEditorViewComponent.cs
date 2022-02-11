using DomainModeling.Crud.WebUi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DomainModeling.Crud.WebUi.ViewComponents.FieldEditors;

public abstract class FieldEditorViewComponent : ViewComponent
{
    public abstract Task<IViewComponentResult> InvokeAsync(EditorFormFieldDefinition fieldDefintion, object value);
}