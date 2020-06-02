using MetaHealth.DAL;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace MetaHealth.Controllers
{
    public class HomeController : Controller
    {
        private Model db = new Model();
        [AllowAnonymous]
        [HandleError]
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("UpcomingEvents", "Calendar");
            }

            else
            {
                return View();
            }

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


        public ActionResult Resources()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {

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

        [AllowAnonymous]
        public ActionResult Venting()

        {
            return View();
        }

        public ActionResult EditName() {
            //get current logged in user

            //display user name
            //change it
            return RedirectToAction("Index", "Home");
        }
    }
}