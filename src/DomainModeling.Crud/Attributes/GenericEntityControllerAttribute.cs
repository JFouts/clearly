namespace DomainModeling.Crud;

/// <summary>
/// Creates route entries for all Entities within the application to the decorated generic controller. 
/// Use the [type] route template paramater to replace with the class name for each entity. By default
/// the route will be "[controller]/[type]"
/// </summary>
[System.AttributeUsage(System.AttributeTargets.Class)]  
public class GenericEntityControllerAttribute : Attribute
{
    
}