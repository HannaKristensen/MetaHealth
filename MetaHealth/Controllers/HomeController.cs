﻿using System.Web.Mvc;

namespace MetaHealth.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SelfCareQuiz()
        {
            return View();
        }

        public ActionResult HowToOpen()
        {
            return View();
        }

        public ActionResult BreathingSpace()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult gitHubResources()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Resources()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult FlowchartCheck()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Flowchart()
        {
            return View();
        }

        public ActionResult Venting()

        {
            return View();
        }
    }
}