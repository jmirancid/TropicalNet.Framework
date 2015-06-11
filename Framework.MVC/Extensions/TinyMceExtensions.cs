using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.Script.Serialization;
using Framework.Common.Extensions;

namespace Framework.MVC.Extensions
{
    public static class TinyMceExtensions
    {
        public static MvcHtmlString TinyMce
            (this HtmlHelper htmlHelper, string name, string value)
        {
            return TinyMce(htmlHelper, name, value, TinyMcePresets.Default);
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
                (options == null) ? string.Empty : (new JavaScriptSerializer()).Serialize(options.ToDictionary(o => o.Key.ToString(), o => o.Value));

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
            return TinyMceFor(htmlHelper, expression, TinyMcePresets.Default);
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
            var value =
                (htmlHelper.ViewData.Model == null) ? string.Empty : expression.GetValueFrom(htmlHelper.ViewData.Model).ToString();

            return TinyMce(htmlHelper, HtmlHelper.GenerateIdFromName(expression.GetNameFor()), value, options);
        }
    }

    public static class TinyMcePresets
    {
        public static IDictionary<string, object> Default
        {
            get
            {
                return new RouteValueDictionary(new
                {
                    language = "en",
                    width = "550px",
                    theme = "modern",
                    plugins = new string[]
                        {
                            "advlist autolink lists link image charmap print preview anchor",
                            "searchreplace visualblocks code fullscreen",
                            "insertdatetime media table contextmenu paste"
                        },
                    dialog_type = "modal",
                    paste_auto_cleanup_on_paste = false,
                    paste_strip_class_attributes = "all",
                    paste_remove_spans = true,
                    force_p_newlines = false,
                    forced_root_block = false,
                    paste_retain_style_properties = "",
                    table_cell_limit = 100,
                    table_row_limit = 5,
                    table_col_limit = 5,
                    theme_advanced_buttons1 = "newdocument,|,bold,italic,underline,|,justifyleft,justifycenter,justifyright,fontselect,fontsizeselect,formatselect",
                    theme_advanced_buttons2 = "cut,copy,paste,pasteword,selectall,|,bullist,numlist,|,outdent,indent,|,undo,redo,|,link,unlink,anchor,image,|,code,|,forecolor,backcolor",
                    theme_advanced_buttons3 = "advhr,,removeformat,|,sub,sup,|,tablecontrols",
                    theme_advanced_toolbar_location = "top",
                    theme_advanced_toolbar_align = "left",
                    theme_advanced_statusbar_location = "bottom",
                    theme_advanced_resizing = true
                });
            }
        }
    }

}
