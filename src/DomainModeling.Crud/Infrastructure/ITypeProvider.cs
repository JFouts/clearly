namespace DomainModeling.Crud.Infrastructure;

internal interface ITypeProvider
{
    IEnumerable<Type> GetTypes();
}
