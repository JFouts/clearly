namespace DomainModeling.Crud.WebUi.ViewModels;

public record ListViewModel : TableDataViewModel
{
    public string DisplayName { get; set; } = string.Empty;
}
