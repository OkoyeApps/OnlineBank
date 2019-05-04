using Identity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Identity.Infrastructure
{
    public class ApplicationRoleManager : RoleManager<AppRole>, IDisposable
    {
        public ApplicationRoleManager(IRoleStore<AppRole, string> store) : base(store)
        {
        }

        public static ApplicationRoleManager Create(
            IdentityFactoryOptions<ApplicationRoleManager> options, 
            IOwinContext Context)
        {
            ApplicationDbContext dbContext = Context.Get<ApplicationDbContext>();
            return new ApplicationRoleManager(new RoleStore<AppRole>(dbContext));
        }
    }
}