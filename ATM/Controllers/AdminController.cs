using ATM.Infrastructure.Services;
using ATM.Infrastructure.ViewModels;
using ATM.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ATM.Controllers
{

    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;
        private CustomerService customerService = new CustomerService();
       
        private ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            set { _userManager = value; }
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCustomer(RegisterCustomerViewModel model)
        {
            ApplicationUser CurrentUser = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            IdentityResult result = await UserManager.CreateAsync(CurrentUser, model.Password);
            if (result.Succeeded)
            {
               int customerId  =  await customerService.AddCustomer(model, CurrentUser.Id);
                await customerService.CreateCustomerAccount(customerId);
                ViewBag.Success = "Customer created successfully";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<ActionResult> AllCustomers()
        {
            return View(await customerService.GetAllCustomers());
        }
    }
}