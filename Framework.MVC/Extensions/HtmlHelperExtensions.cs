using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Framework.MVC.Extensions
{
	public static class HtmlHelperExtensions
	{
		public static MvcHtmlString DisplayNameFor<TModel, TProperty>(
			this HtmlHelper<IEnumerable<TModel>> htmlHelper, Expression<Func<TModel, TProperty>> expression)
		{
			return DisplayNameFor(expression);
		}

		public static MvcHtmlString DisplayNameFor<TModel, TProperty>(
			this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
		{
			return DisplayNameFor(expression);
		}

		private static MvcHtmlString DisplayNameFor<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
			var metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, new ViewDataDictionary<TModel>());
			var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
			string s = metadata.DisplayName ?? (metadata.PropertyName ?? htmlFieldName.Split(new char[] { '.' }).Last<string>());

			return new MvcHtmlString(HttpUtility.HtmlEncode(s));
		}
	}
}
