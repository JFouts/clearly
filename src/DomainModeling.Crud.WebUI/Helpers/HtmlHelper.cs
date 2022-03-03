// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DomainModeling.Crud.WebUi.Helpers;

public static class HtmlHelper
{
    public static IHtmlContent DisplayForDefinedField<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression, EntityFieldDefinition definition)
    {
        var metadata = definition.Using<CrudAdminEntityFieldFeature>();

        return htmlHelper.DisplayFor(expression, metadata.DisplayTemplate, definition.Property.Name, metadata.DisplayProperties);
    }
}
