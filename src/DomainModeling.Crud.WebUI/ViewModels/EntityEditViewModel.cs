using DomainModeling.Crud.WebUi.Models;

namespace DomainModeling.Crud.WebUi.ViewModels;

public record EntityEditViewModel {
    public EntityFormDefinition Definition { get; set; } = new EntityFormDefinition();
    public Dictionary<string, object> Record { get; set; } = new Dictionary<string, object>();
}