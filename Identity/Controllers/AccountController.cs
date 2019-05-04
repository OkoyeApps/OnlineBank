using Identity.Infrastructure;
using Identity.Infrastructure.ViewModels;
using Identity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Identity.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private ApplicationUserManager UserManager
        {
            get => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        private IAuthenticationManager AuthenticationManager
        {
            get => HttpContext.GetOwinContext().Authentication;
        }
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await UserManager.FindAsync(model.Name, model.Password);
                if(user == null)
                {
                    ModelState.AddModelError("", "Invalid name or password");
                }
                else
                {
                    ClaimsIdentity identity =
                        await UserManager.CreateIdentityAsync
                        (user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, identity);
                    return Redirect(returnUrl);
                }
            }
            return View(model);
        }
    }
}