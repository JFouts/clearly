// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq.Expressions;
using Clearly.Crud.Models.EntityGraph;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clearly.Crud.WebUi.Helpers;

// TODO: Remove this after conversion to Blazor

/// <summary>
/// Collection of extension methods for <see cref="IHtmlHelper"/>.
/// </summary>
public static class HtmlHelperExtensions
{
    /// <summary>
    /// Displays a field that has been defined by a <see cref="PropertyDefinitionNode"/>.
    /// </summary>
    /// <typeparam name="TModel">The model of the currently rendering view.</typeparam>
    /// <typeparam name="TResult">The type of the selected field.</typeparam>
    /// <param name="htmlHelper">The currently rendering view's <see cref="IHtmlHelper"/>.</param>
    /// <param name="expression">A expression selecting the field to render from the model.</param>
    /// <param name="definition">The property definition for this field.</param>
    /// <returns>Rendered html content for displaying this property.</returns>
    public static IHtmlContent DisplayForDefinedProperty<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression, PropertyDefinitionNode definition)
    {
        return htmlHelper.Raw(string.Empty);
    }
}
