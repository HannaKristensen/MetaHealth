using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MetaHealth.Models;
using Microsoft.AspNet.Identity;

namespace MetaHealth.Controllers
{
    public class MoodsController : Controller
    {
        private HelpAlongDB db = new HelpAlongDB();

        // GET: Moods
        public ActionResult Index()
        {
            
            
            
            var moods = db.Moods.Include(m => m.MoodsInBetween);
            return View(moods.ToList());
        }

        // GET: Moods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mood mood = db.Moods.Find(id);
            if (mood == null)
            {
                return HttpNotFound();
            }
            return View(mood);
        }

        // GET: Moods/Create
        public ActionResult Create()
        {//here is where we would need to access the table
            //search for FK is present in MoodsInBetween
            //if not, insert the row, if it does exist just add a mood to Moods
            //use THAT number to append a mood to their table
            //could the foreign key from the asp.net table be inserted as a primary key into the moods table?
        string currentUser = User.Identity.GetUserName();
            var aspUser = db.AspNetUsers.Select(n => n.UserName == currentUser).ToString();
            //var userFK = db.AspNetUsers.Where(x => x.Email == currentUser).Select(y=>y.Id);
            //TODO: Need to figure out why this isn't working!!!
            var userFK = db.AspNetUsers.Where(x => x.UserName == currentUser).FirstOrDefault().ToString();
 
            if (!db.MoodsInBetweens.Any(x=>x.FK_UserTable==userFK)) { 
                MoodsInBetween newLine = new MoodsInBetween {
                    FK_UserTable = userFK
                };
                MoodsInBetweensController temp = new MoodsInBetweensController();
                temp.Create(newLine);
                //db.MoodsInBetweens.Add(newLine);
            }
            ViewBag.FK_MoodsInBetween = new SelectList(db.MoodsInBetweens, "PK", "FK_UserTable");


            return View();
        }

        //public string GetUserName(DbSet<Mood> db, int userKey) {
        //    var user = this.User.Identity;
        //}

        // POST: Moods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK,FK_MoodsInBetween,MoodNum,Date")] Mood mood)
        {
            if (ModelState.IsValid)
            {
                db.Moods.Add(mood);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_MoodsInBetween = new SelectList(db.MoodsInBetweens, "PK", "FK_UserTable", mood.FK_MoodsInBetween);
            return View(mood);
        }

        // GET: Moods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mood mood = db.Moods.Find(id);
            if (mood == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_MoodsInBetween = new SelectList(db.MoodsInBetweens, "PK", "FK_UserTable", mood.FK_MoodsInBetween);
            return View(mood);
        }

        // POST: Moods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK,FK_MoodsInBetween,MoodNum,Date")] Mood mood)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mood).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_MoodsInBetween = new SelectList(db.MoodsInBetweens, "PK", "FK_UserTable", mood.FK_MoodsInBetween);
            return View(mood);
        }

        // GET: Moods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mood mood = db.Moods.Find(id);
            if (mood == null)
            {
                return HttpNotFound();
            }
            return View(mood);
        }

        // POST: Moods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mood mood = db.Moods.Find(id);
            db.Moods.Remove(mood);
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
