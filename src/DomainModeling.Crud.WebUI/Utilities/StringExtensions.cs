
using System.Text.RegularExpressions;

namespace DomainModeling.Crud.WebUi.Utilities;

public static class StringExtensions
{
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
}