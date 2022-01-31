using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MCBACommon.Utilities.Extensions;

public static class IHtmlHelperExtension
{
    public static IHtmlContent McbaTextArea<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
    {
        return new HtmlContentBuilder()
            .AppendHtml(htmlHelper.LabelFor(expression, htmlHelper.DisplayNameFor(expression)))
            .AppendHtml(htmlHelper.TextAreaFor(expression, new {@class = "form-control"}))
            .AppendHtml(htmlHelper.ValidationMessageFor(expression, null, new {@class = "text-danger"}));
    }

    public static IHtmlContent McbaDropDown<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression, IEnumerable<SelectListItem> selectList, string? optionLabel = null)
    {
        return new HtmlContentBuilder()
            .AppendHtml(htmlHelper.LabelFor(expression, htmlHelper.DisplayNameFor(expression)))
            .AppendHtml(htmlHelper.DropDownListFor(expression, selectList, optionLabel, new { @class = "form-control" }))
            .AppendHtml(htmlHelper.ValidationMessageFor(expression, null, new { @class = "text-danger" }));
    }

    public static IHtmlContent McbaTextBox<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
    {
        return new HtmlContentBuilder()
            .AppendHtml(htmlHelper.LabelFor(expression, htmlHelper.DisplayNameFor(expression)))
            .AppendHtml(htmlHelper.TextBoxFor(expression, new { @class = "form-control" }))
            .AppendHtml(htmlHelper.ValidationMessageFor(expression, null, new { @class = "text-danger" }));
    }
    public static IHtmlContent McbaDateTime<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
    {
        return new HtmlContentBuilder()
            .AppendHtml(htmlHelper.LabelFor(expression, htmlHelper.DisplayNameFor(expression)))
            .AppendHtml(htmlHelper.TextBoxFor(expression, new { @class = "form-control", type = "datetime-local" }))
            .AppendHtml(htmlHelper.ValidationMessageFor(expression, null, new { @class = "text-danger" }));
    }

    public static IHtmlContent McbaPassword<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
    {
        return new HtmlContentBuilder()
            .AppendHtml(htmlHelper.LabelFor(expression, htmlHelper.DisplayNameFor(expression)))
            .AppendHtml(htmlHelper.PasswordFor(expression, new { @class = "form-control" }))
            .AppendHtml(htmlHelper.ValidationMessageFor(expression, null, new { @class = "text-danger" }));
    }
}