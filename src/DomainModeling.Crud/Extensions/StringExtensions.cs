// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Text.RegularExpressions;

namespace DomainModeling.Crud;

public static class StringExtensions
{
    /// <summary>
    /// Attempts to format a code name in camel or pascal case as
    /// a user friendly readable string.
    /// </summary>
    public static string FormatForDisplay(this string str)
    {
        var str1 = Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2");

        return Regex.Replace(str1, @"(\p{Ll})(\P{Ll})", "$1 $2");
    }

    /// <summary>
    /// Converts a Pascal Case string to Camel Case
    /// </summary>
    public static string ToCamelCase(this string str)
    {
        if (str?.Length > 0)
        {
            return char.ToLower(str[0]) + str.Substring(1);
        }

        return str ?? string.Empty;
    }

    /// <summary>
    /// Converts a Camel Case string to Pascal Case
    /// </summary>
    public static string ToPascalCase(this string str)
    {
        if (str?.Length > 0)
        {
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        return str ?? string.Empty;
    }
}
