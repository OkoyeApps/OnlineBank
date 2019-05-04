using Identity.Infrastructure;
using Identity.Infrastructure.ViewModels;
using Identity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Identity.Controllers
{
    
    public class HomeController : Controller
    {
        // GET: Home

        private ApplicationUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }
        [Authorize]
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();              
            return View(UserManager.Users);
        }
        [Authorize]
        public ActionResult Contact()
        {
            return View();
        }
            
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(UserModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser { UserName = model.Email, Email = model.Email };
                IdentityResult result =await UserManager.CreateAsync(user, model.Password);     
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorFromResult(result);
                    
                }
            }
            return View();
        }
        
        public void AddErrorFromResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}