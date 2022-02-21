using System.Linq.Expressions;
using System.Reflection;

namespace DomainModeling.Crud;

public static class ExpressionExtensions
{
    /// <summary>
    /// Selects a property on a class to get the PropertyInfo through reflection
    /// </summary>
    /// <typeparam name="TSource">The type the property is defined on</typeparam>
    /// <typeparam name="TProperty">The type of the property selected</typeparam>
    /// <param name="propertySelector">An expression selecting a property on an object</param>
    /// <returns>The PropertyInfo for the selected property thorugh reflection</returns>
    /// <exception cref="ArgumentException">Thrown when the selector expression is invalid</exception>
    /// <remarks>This must be a simple expression selecting a field. The expression will not be evaluated.</remarks>
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