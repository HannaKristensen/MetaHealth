using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetaHealth.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult JSGame()
        {
            return View();
        }
    }
}