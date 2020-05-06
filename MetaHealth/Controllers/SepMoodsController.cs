using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MetaHealth.Models;
using Microsoft.AspNet.Identity;
using MetaHealth.DAL;

namespace MetaHealth.Controllers
{
    public class SepMoodsController : Controller
    {
        private Model db = new Model();

        // GET: SepMoods
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            List<SepMood> sepMoods = db.SepMoods.Where(x => x.UserID == userId).OrderByDescending(y => y.Date).ToList();

            DateTime today = DateTime.Today;
            DateTime week = DateTime.Today.AddDays(-7);

            List<SepMood> todaysMoods = db.SepMoods.Where(x => x.UserID == userId).Where(y => y.Date.Day == today.Day).OrderByDescending(z => z.Date).ToList();


            List<SepMood> thisWeeksMoods = db.SepMoods.Where(x => x.UserID == userId).Where(y => y.Date >= week ).OrderByDescending(z => z.Date).ToList();

            ViewBag.todaysMoods = todaysMoods;
            ViewBag.weeksMoods = thisWeeksMoods;



            return View(sepMoods);
        }


        public ActionResult todaysMoodsP()
        {
            var userId = User.Identity.GetUserId();
            DateTime today = DateTime.Today;

            List<SepMood> todaysMoods = db.SepMoods.Where(x => x.UserID == userId).Where(y => y.Date.Day == today.Day).OrderByDescending(z => z.Date).ToList();

            return View(todaysMoods);

        }

        // GET: SepMoods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SepMood sepMood = db.SepMoods.Find(id);
            if (sepMood == null)
            {
                return HttpNotFound();
            }
            return View(sepMood);
        }

        // GET: SepMoods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SepMoods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK,UserID,MoodNum,Date,Reason")] SepMood sepMood)
        {
            sepMood.UserID = User.Identity.GetUserId();
            sepMood.Date = DateTime.Now;
            if (sepMood.UserID == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                db.SepMoods.Add(sepMood);
                db.SaveChanges();
                //MessageBox.Show("Your mood was added successfully", "Complete!" );
                return RedirectToAction("UpcomingEvents", "Calendar");
            }

            return View(sepMood);
        }

        // GET: SepMoods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SepMood sepMood = db.SepMoods.Find(id);
            if (sepMood == null)
            {
                return HttpNotFound();
            }
            return View(sepMood);
        }

        // POST: SepMoods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK,UserID,MoodNum,Date,Reason")] SepMood sepMood)
        {
            sepMood.UserID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(sepMood).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sepMood);
        }

        // GET: SepMoods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SepMood sepMood = db.SepMoods.Find(id);
            if (sepMood == null)
            {
                return HttpNotFound();
            }
            return View(sepMood);
        }

        // POST: SepMoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SepMood sepMood = db.SepMoods.Find(id);
            db.SepMoods.Remove(sepMood);
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

        //This function assumes that you've already grabbed the mood data for the currently logged in user
        public double AverageDailyMood(List<SepMood> userMoods, DateTime dateWanted)
        {
            double sumMood = 0;
            int entryCounter = 0;
            foreach (SepMood entry in userMoods)
            {
                if (entry.Date == dateWanted)
                {
                    sumMood += entry.MoodNum;
                    entryCounter++;
                }
            }
            return sumMood / entryCounter;
        }

        //overload that assumes list has already been filtered for correct date
        public double AverageDailyMood(List<SepMood> userMoods)
        {
            double sumMood = 0;
            int entryCounter = 0;
            foreach (SepMood entry in userMoods)
            {
                sumMood += entry.MoodNum;
                entryCounter++;
            }
            return sumMood / entryCounter;
        }

        public double AverageDailyMood(List<int> userMoods)
        {
            double sumMood = 0;
            int entryCounter = 0;
            foreach (int entry in userMoods)
            {
                sumMood += entry;
                entryCounter++;
            }
            return sumMood / entryCounter;
        }

        public List<int> GetMoodsByDate(DateTime dateWanted)
        {
            var retVal = db.SepMoods.Where(d => d.Date == dateWanted).Select(m => m.MoodNum).ToList();
            return retVal;
        }

        public List<int> GetMoodsByDate(List<SepMood> sepMoods, DateTime dateWanted)
        {
            var retVal = new List<int>();
            foreach (var item in sepMoods)
            {
                if (item.Date.Date == dateWanted)
                {
                    retVal.Add(item.MoodNum);
                }
            }
            return retVal;
        }
    }
}