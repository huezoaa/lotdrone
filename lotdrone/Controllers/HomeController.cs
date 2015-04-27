using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lotdrone.Controllers
{
    public class HomeController : Controller
    {
         [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Notification = "Angel Huezo. Miami, FL";

            return View();
        }

         [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Notification = "Angel Huezo.";

            return View();
        }
    }
}