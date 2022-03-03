// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clearly.Crud.WebUi.Helpers;

public static class UrlHelpers
{
    public static string LinkToParamater(this IUrlHelper helper, string parameter, string value)
    {
        var uri = helper.ActionContext.HttpContext.Request.RequestUri();

        var queryString = System.Web.HttpUtility.ParseQueryString(uri.Query ?? string.Empty);
        queryString[parameter] = value;
        uri.Query = queryString.ToString();

        return uri.ToString();
    }

    public static string ChildPath(this IUrlHelper helper, string path)
    {
        var uri = helper.ActionContext.HttpContext.Request.RequestUri();

        uri.Path = $"{uri.Path?.TrimEnd('/')}/{path?.TrimStart('/')}";

        return uri.ToString();
    }

    private static UriBuilder RequestUri(this HttpRequest request)
    {
        var uri = new UriBuilder
        {
            Scheme = request.Scheme,
            Host = request.Host.Host,
            Path = request.Path,
            Query = request.QueryString.ToString(),
        };

        if (request.Host.Port != null)
        {
            uri.Port = request.Host.Port.Value;
        }

        return uri;
    }
}
