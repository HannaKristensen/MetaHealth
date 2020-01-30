using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Class_Project.Models.View_Models;
using Class_Project.Models;
using System.Net;

namespace Class_Project.Controllers
{
    public class HomeController : Controller
    {
        private DBContext db = new DBContext(); 

        [HttpGet]
        public ActionResult Index(string SearchQ)
        {
            var name = db.Athletes.Where(s => s.Name.Contains(SearchQ));

            if (name == null)
            {
                return View("Item Not found!");
            }

            return View(name);
        }


        public ActionResult GetDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Athlete theAthlete = db.Athletes.Find(id);

            if (theAthlete == null)
            {
                return HttpNotFound();
            }

            AthleteDetailsViewModelcs viewModel = new AthleteDetailsViewModelcs(theAthlete);

            //viewModel = new StockItemViewModel(theItem);

            return View(viewModel);
        }



    }
}