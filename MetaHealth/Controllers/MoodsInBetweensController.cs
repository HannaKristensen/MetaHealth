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
    public class MoodsInBetweensController : Controller
    {
        private HelpAlongDB db = new HelpAlongDB();

        // GET: MoodsInBetweens
        public ActionResult Index()
        {
            var moodsInBetweens = db.MoodsInBetweens.Include(m => m.AspNetUser);
            return View(moodsInBetweens.ToList());
        }

        // GET: MoodsInBetweens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoodsInBetween moodsInBetween = db.MoodsInBetweens.Find(id);
            if (moodsInBetween == null)
            {
                return HttpNotFound();
            }
            return View(moodsInBetween);
        }

        // GET: MoodsInBetweens/Create
        public ActionResult Create()
        {
            ViewBag.FK_UserTable = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: MoodsInBetweens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK,FK_UserTable")] MoodsInBetween moodsInBetween)
        {
            if (ModelState.IsValid)
            {   if (moodsInBetween.AspNetUser == null) {
                    var currentUser = User.Identity.GetUserName();//why is the user null here?
                    var userFK = db.AspNetUsers.Where(x => x.UserName == currentUser).FirstOrDefault().ToString();
                    moodsInBetween.FK_UserTable = userFK;
                }
                db.MoodsInBetweens.Add(moodsInBetween);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_UserTable = new SelectList(db.AspNetUsers, "Id", "Email", moodsInBetween.FK_UserTable);
            return View(moodsInBetween);
        }

        // GET: MoodsInBetweens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoodsInBetween moodsInBetween = db.MoodsInBetweens.Find(id);
            if (moodsInBetween == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_UserTable = new SelectList(db.AspNetUsers, "Id", "Email", moodsInBetween.FK_UserTable);
            return View(moodsInBetween);
        }

        // POST: MoodsInBetweens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK,FK_UserTable")] MoodsInBetween moodsInBetween)
        {
            if (ModelState.IsValid)
            {
                db.Entry(moodsInBetween).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_UserTable = new SelectList(db.AspNetUsers, "Id", "Email", moodsInBetween.FK_UserTable);
            return View(moodsInBetween);
        }

        // GET: MoodsInBetweens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoodsInBetween moodsInBetween = db.MoodsInBetweens.Find(id);
            if (moodsInBetween == null)
            {
                return HttpNotFound();
            }
            return View(moodsInBetween);
        }

        // POST: MoodsInBetweens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MoodsInBetween moodsInBetween = db.MoodsInBetweens.Find(id);
            db.MoodsInBetweens.Remove(moodsInBetween);
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
