
using System.Text.RegularExpressions;

namespace DomainModeling.Crud;

public static class StringExtensions
{
    // TODO: Comments and probably give these better names

    public static string SplitCamelCase(this string str)
    {
        return Regex.Replace( 
            Regex.Replace( 
                str, 
                @"(\P{Ll})(\P{Ll}\p{Ll})", 
                "$1 $2" 
            ), 
            @"(\p{Ll})(\P{Ll})", 
            "$1 $2" 
        );
    }    
    
    public static string LowerCamelCase(this string str)
    {
        if (str?.Length > 0)
        {
            return Char.ToLower(str[0]) + str.Substring(1);
        }

        return str ?? string.Empty;
    }

    public static string UpperCamelCase(this string str)
    {
        if (str?.Length > 0)
        {
            return Char.ToUpper(str[0]) + str.Substring(1);
        }

        return str ?? string.Empty;
    }
}