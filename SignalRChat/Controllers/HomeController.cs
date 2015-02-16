using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalRChat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Confessor()
        {
            ViewBag.Message = "Anonymously confess things to the internet.";

            return View();
        }

        public ActionResult Chat()
        {
            ViewBag.Message = "Chat, in real time, with anyone viewing berserkerwashington.com.";

            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "The description page for berserkerwashington.com...";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Contact us...";

            return View();
        }
    }
}