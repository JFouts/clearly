namespace DomainModeling.Crud.WebUi.ViewModels;

public interface IPageableViewModel
{
    int PageCount { get; }
    int CurrentPage { get; }
}