using Microsoft.AspNetCore.Mvc;

namespace DomainModeling.Crud.WebUi.ViewComponents.FieldEditors;

public abstract class FieldEditorViewComponent : ViewComponent
{
    public abstract Task<IViewComponentResult> InvokeAsync(EntityFieldDefinition fieldDefinition, object value);
}