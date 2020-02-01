using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Class_Project.Models;

namespace Class_Project.Views
{
    public class AthleteResultsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: AthleteResults
        public ActionResult Index()
        {
            var athleteResults = db.AthleteResults.Include(a => a.Athlete).Include(a => a.Location).Include(a => a.Result);
            return View(athleteResults.ToList());
        }

        // GET: AthleteResults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AthleteResult athleteResult = db.AthleteResults.Find(id);
            if (athleteResult == null)
            {
                return HttpNotFound();
            }
            return View(athleteResult);
        }

        // GET: AthleteResults/Create
        public ActionResult Create()
        {
            ViewBag.AthleteID = new SelectList(db.Athletes, "AthleteID", "Name");
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "Location1");
            ViewBag.ResultID = new SelectList(db.Results, "ResultID", "Event");
            return View();
        }

        // POST: AthleteResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AthleteResultsID,RaceTime,LocationID,AthleteID,ResultID")] AthleteResult athleteResult)
        {
            if (ModelState.IsValid)
            {
                db.AthleteResults.Add(athleteResult);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.AthleteID = new SelectList(db.Athletes, "AthleteID", "Name", athleteResult.AthleteID);
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "Location1", athleteResult.LocationID);
            ViewBag.ResultID = new SelectList(db.Results, "ResultID", "Event", athleteResult.ResultID);
            return View(athleteResult);
        }

        // GET: AthleteResults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AthleteResult athleteResult = db.AthleteResults.Find(id);
            if (athleteResult == null)
            {
                return HttpNotFound();
            }
            ViewBag.AthleteID = new SelectList(db.Athletes, "AthleteID", "Name", athleteResult.AthleteID);
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "Location1", athleteResult.LocationID);
            ViewBag.ResultID = new SelectList(db.Results, "ResultID", "Event", athleteResult.ResultID);
            return View(athleteResult);
        }

        // POST: AthleteResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AthleteResultsID,RaceTime,LocationID,AthleteID,ResultID")] AthleteResult athleteResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(athleteResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AthleteID = new SelectList(db.Athletes, "AthleteID", "Name", athleteResult.AthleteID);
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "Location1", athleteResult.LocationID);
            ViewBag.ResultID = new SelectList(db.Results, "ResultID", "Event", athleteResult.ResultID);
            return View(athleteResult);
        }

        // GET: AthleteResults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AthleteResult athleteResult = db.AthleteResults.Find(id);
            if (athleteResult == null)
            {
                return HttpNotFound();
            }
            return View(athleteResult);
        }

        // POST: AthleteResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AthleteResult athleteResult = db.AthleteResults.Find(id);
            db.AthleteResults.Remove(athleteResult);
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