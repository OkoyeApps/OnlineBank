using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Identity.Infrastructure
{
    public static class Helpers
    {
        public static MvcHtmlString FormatUsername(this HtmlHelper html, string id)
        {
            ApplicationUserManager manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return new MvcHtmlString(manager.FindByIdAsync(id).Result.UserName);
        }
    }
}