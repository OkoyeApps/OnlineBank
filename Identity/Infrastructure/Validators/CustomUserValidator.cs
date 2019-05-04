using Identity.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Identity.Infrastructure.Validators
{
    public class CustomUserValidator : UserValidator<AppUser>
    {
        public CustomUserValidator(UserManager<AppUser, string> manager) : base(manager)
        {
        }

        public override async Task<IdentityResult> ValidateAsync(AppUser user)
        {
            IdentityResult result = await  base.ValidateAsync(user);
            if(user.Email.ToLower().Contains("admin") 
                || !user.Email.ToLower().EndsWith("example.com"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Only example.com email addresses are allowed");
                result = new IdentityResult(errors);
            }
            return result;
        }
    }
}