using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Identity.Models
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base() { }
        public  AppRole(string roleName) : base(roleName) { }
    }
}