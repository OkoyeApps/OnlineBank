using Identity.Infrastructure;
using Microsoft.AspNet.Identity;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Identity
{
    public partial class IdentityConfig
    {
        public void ConfigurationAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<ApplicationDbContext>(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            app.UseCookieAuthentication(
                new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
                {
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                    LoginPath = new Microsoft.Owin.PathString("/Account/Login")
                });

        }
    }
}