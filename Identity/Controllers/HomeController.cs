using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Identity.Controllers
{
    
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            
            int a = 2;
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("PlaceholderKey", "PlaceholderValue");
            return View("Index", data);
        }
    }
}