using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Identity.Infrastructure.Validators
{
    public class CustomPasswordValidator : PasswordValidator
    {

        public override async Task<IdentityResult> ValidateAsync(string password)
        {
            IdentityResult result = await base.ValidateAsync(password);
            if (password.Contains("12345"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Password cannot contain a sequence");
                result = new IdentityResult(errors);
            }
            return result;
        }
    }
}