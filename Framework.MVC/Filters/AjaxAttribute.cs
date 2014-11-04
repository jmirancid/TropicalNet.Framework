using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Framework.MVC.Filters
{
	public class AjaxAttribute : ActionMethodSelectorAttribute
	{
		public override bool IsValidForRequest(ControllerContext controllerContext, System.Reflection.MethodInfo methodInfo)
		{
			return controllerContext.HttpContext.Request.IsAjaxRequest();
		}
	}
}
