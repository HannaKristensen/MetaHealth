using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Class_Project.Models;
using Class_Project.Models.View_Models;

namespace Class_Project.Views
{
    public class AthletesController : Controller
    {
        private DBContext db = new DBContext();

        [HttpGet]
        [Authorize]
        public ActionResult HomePage(string SearchQ)
        {
            var name = db.Athletes.Where(s => s.Name.Contains(SearchQ)).ToList();

            if (name == null)
            {
                return View("Item Not found!");
            }

            return View(name);
        }

        // GET: Athletes

        public ActionResult Index()
        {
            var athletes = db.Athletes.Include(a => a.Team);
            return View(athletes.ToList());
        }

        // GET: Athletes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Athlete athlete = db.Athletes.Find(id);

            if (athlete == null)
            {
                return HttpNotFound();
            }
            AthleteDetailsViewModelcs viewModel = new AthleteDetailsViewModelcs(athlete);

            return View(viewModel);
        }

        // GET: Athletes/Create
        public ActionResult Create()
        {
            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "Name");
            ViewBag.Gender = new SelectList(db.Athletes, "Gender", "Gender");
            return View();
        }

        // POST: Athletes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AthleteID,Name,Gender,TeamID")] Athlete athlete)
        {
            if (ModelState.IsValid)
            {
                db.Athletes.Add(athlete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "Name", athlete.TeamID);
            ViewBag.GenderID = new SelectList(db.Athletes, "Gender", "Gender", athlete.Gender);
            return View(athlete);
        }

        // GET: Athletes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Athlete athlete = db.Athletes.Find(id);
            if (athlete == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "Name", athlete.TeamID);
            return View(athlete);
        }

        // POST: Athletes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AthleteID,Name,Gender,TeamID")] Athlete athlete)
        {
            if (ModelState.IsValid)
            {
                db.Entry(athlete).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "Name", athlete.TeamID);
            return View(athlete);
        }

        // GET: Athletes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Athlete athlete = db.Athletes.Find(id);
            if (athlete == null)
            {
                return HttpNotFound();
            }
            return View(athlete);
        }

        // POST: Athletes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Athlete athlete = db.Athletes.Find(id);
            db.Athletes.Remove(athlete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}