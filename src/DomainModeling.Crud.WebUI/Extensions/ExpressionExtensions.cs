using System.Linq.Expressions;
using System.Reflection;

namespace DomainModeling.Crud.WebUi;

public static class ExpressionExtensions
{
    public static PropertyInfo GetPropertyInfo<TSource, TProperty>(this Expression<Func<TSource, TProperty>> propertySelector)
    {
        var type = typeof(TSource);

        var member = propertySelector.Body as MemberExpression
            ?? throw new ArgumentException($"Expression '{propertySelector}' refers to a method, not a property.");

        var propInfo = member.Member as PropertyInfo
            ?? throw new ArgumentException($"Expression '{propertySelector}' refers to a field, not a property.");

        if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType!))
        {
            throw new ArgumentException($"Expression '{propertySelector}' refers to a property that is not from type {type}.");
        }

        return propInfo;
    }
}