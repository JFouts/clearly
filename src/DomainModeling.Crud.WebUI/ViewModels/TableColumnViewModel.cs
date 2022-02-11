using DomainModeling.Crud.WebUi.Models;

namespace DomainModeling.Crud.WebUi.ViewModels;

public record TableColumnViewModel {
    public string Key { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public FieldDefintion FieldDefintion { get; set; } = new FieldDefintion();
}
