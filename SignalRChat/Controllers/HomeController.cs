using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using System.Web.Mvc;
using SignalRChat.QueryEngine;


namespace SignalRChat.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            return View();
        }

        // This is unused now.
        public ActionResult Confessor()
        {
            var q = new Queryer();
            var confessions = q.LastXConfessions(10).Result;

            return View(confessions);
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