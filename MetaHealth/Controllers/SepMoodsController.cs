using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MetaHealth.Models;
using Microsoft.AspNet.Identity;

namespace MetaHealth.Controllers
{
    public class SepMoodsController : Controller
    {
        private Model db = new Model();

        // GET: SepMoods
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            return View(db.SepMoods.Where(x => x.UserID == userId).OrderByDescending(y=>y.Date));
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
                return RedirectToAction("Index");
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
    }
}