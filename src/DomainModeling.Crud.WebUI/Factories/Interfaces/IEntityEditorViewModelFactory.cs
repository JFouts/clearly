using DomainModeling.Core;
using DomainModeling.Crud.WebUi.ViewModels;

namespace DomainModeling.Crud.WebUi.Factories;

public interface IEntityEditorViewModelFactory<TEntity> where TEntity : IEntity
{
    EntityEditorViewModel Build(TEntity value);
}
