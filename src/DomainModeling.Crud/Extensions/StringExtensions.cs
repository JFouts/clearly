// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Text;
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

    /// <summary>
    /// Formats a string that has tokenized paramaters in the for of [token] and replaces them with the
    /// values passed in the value dictionary
    /// </summary>
    /// <param name="template">A tokenized string with tokens in the form of [token]</param>
    /// <param name="values">Replacement values for tokens in the string</param>
    /// <returns>A string with the tokens replaced by their corresponding values</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static string FormatTokenizedString(this string template, Dictionary<string, string?> values)
    {
        var outputBuilder = new StringBuilder();
        var tokenBuilder = new StringBuilder(capacity: template.Length, maxCapacity: template.Length);
        var buffer = outputBuilder;

        var len = template.Length;
        for (var i = 0; i < len; ++i)
        {
            var c = template[i];
            var next = i + 1 < len ? template[i + 1] : '\0';

            switch (c)
            {
                case '[' when next == '[':
                    buffer.Append(c);
                    i++;
                    break;

                case '[':
                    // If we are still in the token builder then we never closed this token
                    if (buffer == tokenBuilder)
                    {
                        throw new InvalidOperationException("Bracket opened inside of a token!");
                    }

                    buffer = tokenBuilder.Clear();
                    break;

                case ']':
                    // Escaped closing brackets at the end of a token should always go on the outside of the token
                    var closingBracketCount = 0;
                    for (; i < len && template[i] == ']'; ++i, ++closingBracketCount);
                    --i;
                    
                    // if we have an off number of brackets then we are closing
                    if (closingBracketCount % 2 != 0)
                    {
                        // If we are still in the output builder then we never started a token
                        if (buffer == outputBuilder)
                        {
                            throw new InvalidOperationException("Closing bracket has no opening bracket!");
                        }

                        // We have closed out a token
                        if (tokenBuilder.Length == 0)
                        {
                            throw new InvalidOperationException("Empty Token Not Allowed");
                        }

                        var key = tokenBuilder.ToString();
                        if (!values.TryGetValue(key, out var value))
                        {
                            throw new InvalidOperationException($"Replacement value '{key}' not found!");
                        }

                        buffer = outputBuilder.Append(value);
                    }

                    // If there were escaped bracket we can add them now that we have processed the token
                    // Half the number of brackets we saw because they were escaped
                    for (var j = 0; j < closingBracketCount / 2; j++)
                    {
                        buffer.Append(']');
                    }

                    break;
                    
                default:
                    buffer.Append(c);
                    break;
            }
        }

        // If we are still in the token builder then we never closed out of this token
        if (buffer == tokenBuilder)
        {
            throw new InvalidOperationException("String ended without a closing bracket!");
        }

        return outputBuilder.ToString();
    }
}
