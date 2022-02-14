namespace DomainModeling.Crud;

internal interface ITypeProvider
{
    IEnumerable<Type> GetTypes();
}
