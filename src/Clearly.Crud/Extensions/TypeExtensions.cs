// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections;

namespace Clearly.Crud;

/// <summary>
/// Collection of extension methods for <see cref="Type"/>.
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// If the type is enumerable, this will get the generic type for the collection.
    /// </summary>
    /// <param name="type">A collection type.</param>
    /// <returns>The enumerable type of the collection.</returns>
    public static Type GetEnumerableType(this Type type)
    {
        var ienum = FindIEnumerable(type);

        if (ienum == null || !type.IsAssignableTo(typeof(IEnumerable)))
        {
            throw new ArgumentException($"{nameof(type)} must be of type {nameof(IEnumerable)}", nameof(type));
        }

        return ienum.GetGenericArguments()[0];
    }

    private static Type? FindIEnumerable(Type seqType)
    {
        if (seqType == null || seqType == typeof(string))
        {
            return null;
        }

        if (seqType.IsArray)
        {
            return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType() !);
        }

        if (seqType.IsGenericType)
        {
            foreach (Type arg in seqType.GetGenericArguments())
            {
                Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);

                if (ienum.IsAssignableFrom(seqType))
                {
                    return ienum;
                }
            }
        }

        var ifaces = seqType.GetInterfaces();
        if (ifaces != null && ifaces.Length > 0)
        {
            foreach (var iface in ifaces)
            {
                var ienum = FindIEnumerable(iface);

                if (ienum != null)
                {
                    return ienum;
                }
            }
        }

        if (seqType.BaseType != null && seqType.BaseType != typeof(object))
        {
            return FindIEnumerable(seqType.BaseType);
        }

        return null;
    }
}
