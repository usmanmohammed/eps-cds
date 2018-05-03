using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace EPS.Models
{
    public static class AppHelpers
    {
        public static string GenerateBreadCrumb(this HtmlHelper helper)
        {
            StringBuilder breadcrumb = new StringBuilder();
            if (helper.ViewContext.RouteData.Values["controller"].ToString() != "Home")
            {
                breadcrumb.Append("<li>").Append(helper.ActionLink("Home", "Index", "Home").ToHtmlString()).Append("</li>");
            }

            breadcrumb.Append("<li>").Append(helper.ActionLink(helper.ViewContext.RouteData.Values["controller"].ToString().ToTitle(),
                "Index", helper.ViewContext.RouteData.Values["controller"].ToString())).Append("</li>");

            if (helper.ViewContext.RouteData.Values["action"].ToString() != "Index")
            {
                breadcrumb.Append("<li>").Append(helper.ActionLink(helper.ViewContext.RouteData.Values["action"].ToString().ToTitle(),
                    helper.ViewContext.RouteData.Values["action"].ToString(),
                    helper.ViewContext.RouteData.Values["controller"].ToString())).Append("</li>");
            }

            return breadcrumb.ToString();
        }
    }
}
