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
    public class ApplicationUserManager : UserManager<AppUser>
    {
        public ApplicationUserManager(IUserStore<AppUser> store) : base(store)
        {
        }

        public static ApplicationUserManager Create(
                                    IdentityFactoryOptions<ApplicationUserManager> options, 
                                    IOwinContext context)
        {
            ApplicationDbContext db = context.Get<ApplicationDbContext>();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<AppUser>(db));
            return manager;
        }
    }
}