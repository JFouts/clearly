using Microsoft.AspNetCore.Mvc;

namespace DomainModeling.Crud.WebUi.Helpers;

public static class UrlHelpers
{
    public static string LinkToParamater(this IUrlHelper helper, string parameter, string value)
    {
        var request = helper.ActionContext.HttpContext.Request;

        var queryString = System.Web.HttpUtility.ParseQueryString(request.QueryString.ToString() ?? string.Empty) ;

        queryString[parameter] = value;

        var uri = new UriBuilder {
            Scheme = request.Scheme,
            Host = request.Host.Host,
            Path = request.Path,
            Query = queryString.ToString()
        };

        if (request.Host.Port != null) {
            uri.Port = request.Host.Port.Value;
        }

        return uri.ToString();
    }
}