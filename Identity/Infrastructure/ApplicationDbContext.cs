using Identity.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Identity.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base ("defaultConnection")
        {
        }

        public DbSet<Transaction> Transaction { get; set; }
        static ApplicationDbContext ()
        {
            Database.SetInitializer<ApplicationDbContext>(new IdentityDbInit());
        }
      public  static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class IdentityDbInit : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            PerformInitialDatabaseSetup(context);
            base.Seed(context);
        }

        public void PerformInitialDatabaseSetup(ApplicationDbContext context)
        {
            ApplicationUserManager UserManager = new ApplicationUserManager(new UserStore<AppUser>(context));
            ApplicationUserManager UserManagerFromOwin = 
                HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationDbContext Owincontext =
                HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();
        }
    }
}