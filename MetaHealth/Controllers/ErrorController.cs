using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetaHealth.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        [HandleError]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        //RETRIEVED FROM https://stackoverflow.com/questions/23565098/asp-net-mvc-5-custom-error-page/29712033#29712033
        [AllowAnonymous]
        public ActionResult Error(int statusCode, Exception exception)
        {
            Response.StatusCode = statusCode;
            ViewBag.ErrorNumber = statusCode;
            ViewBag.StatusCode = statusCode + " error has occured ";
            return View();
        }
    }
}