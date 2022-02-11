using System.Linq.Expressions;
using DomainModeling.Crud.WebUi.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DomainModeling.Crud.WebUi.Helpers;

public static class HtmlHelper
{
    public static IHtmlContent DisplayForDefinedField<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression, FieldDefintion defintion)
    {
        return htmlHelper.DisplayFor(expression, defintion.FieldDisplayType, defintion.FieldName, defintion.Properties);
    }
}