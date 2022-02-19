namespace DomainModeling.Crud.WebUi.ViewModels;

public record EntityEditorViewModel
{
    public EntityDefinition Definition { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string DataSourceUrl { get; set; } = string.Empty;
    public IEnumerable<EntityFieldEditorViewModel> Fields { get; set; } = new List<EntityFieldEditorViewModel>();
    
    public EntityEditorViewModel(EntityDefinition definition)
    {
        Definition = definition;
    }
}
