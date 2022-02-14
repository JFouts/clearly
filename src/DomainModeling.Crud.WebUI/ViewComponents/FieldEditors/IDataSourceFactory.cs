using DomainModeling.Crud.Services;

namespace DomainModeling.Crud.WebUi.ViewComponents.FieldEditors;

public interface IDataSourceFactory
{
    IDataSource Create(object sourceDefinition);
}
