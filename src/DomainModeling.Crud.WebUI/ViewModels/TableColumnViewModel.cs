namespace DomainModeling.Crud.WebUi.ViewModels;

public record TableColumnViewModel {
    public string Key { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string DisplayTemplate { get; set; } = string.Empty;
    public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
}
