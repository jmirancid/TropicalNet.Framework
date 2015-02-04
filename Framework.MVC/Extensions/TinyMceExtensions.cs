using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Script.Serialization;
using Framework.Common.Extensions;

namespace Framework.MVC.Extensions
{
    public static class TinyMceExtensions
    {
        public static MvcHtmlString TinyMce
            (this HtmlHelper htmlHelper, string name, string value)
        {
            return TinyMce(htmlHelper, name, value, null);
        }

        public static MvcHtmlString TinyMce
            (this HtmlHelper htmlHelper, string name, string value, object options)
        {
            return TinyMce(htmlHelper, name, value, HtmlHelper.AnonymousObjectToHtmlAttributes(options));
        }

        public static MvcHtmlString TinyMce
            (this HtmlHelper htmlHelper, string name, string value, IDictionary<string, object> options)
        {
            var textArea =
                htmlHelper.TextArea(name, value, new { @id = name });

            var settings =
                (options == null ? string.Empty : (new JavaScriptSerializer()).Serialize(options.ToDictionary(o => o.Key.ToString(), o => o.Value.ToString())));

            var script = new TagBuilder("script");
            script.Attributes["type"] = "text/javascript";

            script.InnerHtml = @"
                    $(function() {
                        $('#" + name + "').tinymce(" + settings + @");
                    });";

            return MvcHtmlString.Create(textArea + script.ToString());
        }

        public static MvcHtmlString TinyMceFor<TModel, TProperty>
            (this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
            where TModel : class
        {
            return TinyMceFor(htmlHelper, expression, null);
        }

        public static MvcHtmlString TinyMceFor<TModel, TProperty>
            (this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object options)
            where TModel : class
        {
            return TinyMceFor(htmlHelper, expression, HtmlHelper.AnonymousObjectToHtmlAttributes(options));
        }

        public static MvcHtmlString TinyMceFor<TModel, TProperty>
            (this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> options)
            where TModel : class
        {
            return TinyMce(htmlHelper, expression.GetNameFor(), expression.GetValueFrom(htmlHelper.ViewData.Model).ToString(), options);
        }
    }
}
