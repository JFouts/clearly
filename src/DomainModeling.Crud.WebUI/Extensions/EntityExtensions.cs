using DomainModeling.Core;

namespace DomainModeling.Crud.WebUi.Extensions;

public static class EntityExtensions
{
    public static Dictionary<string, object> ToDictionary(this IEntity entity)
    {
        var data = new Dictionary<string, object>();

        if (entity != null)
        {
            foreach (var property in entity.GetType().GetProperties())
            {
                var value =  property.GetValue(entity);

                if (value != null)
                {
                    data[property.Name] = value;
                }
            }
        }
        
        return data;
    }
}