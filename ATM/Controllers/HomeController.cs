using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATM.Controllers
{
    
    public class HomeController : Controller
    {
         [HttpGet]
        public ActionResult Index()
        {
     
            return View();
        }
        [HttpPost]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Event()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Event(string testing)
        {
            ViewBag.Message = testing;
            return View() ;
        }

    }
}