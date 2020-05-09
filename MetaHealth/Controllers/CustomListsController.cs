using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MetaHealth.DAL;
using MetaHealth.Models;
using Microsoft.AspNet.Identity;

namespace MetaHealth.Controllers
{
    public class CustomListsController : Controller
    {
        private Model db = new Model();

        // GET: CustomLists
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            return View(db.CustomLists.Where(x => x.UserID == userId).ToList());
        }

        // GET: CustomLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomList customList = db.CustomLists.Find(id);
            if (customList == null)
            {
                return HttpNotFound();
            }
            return View(customList);
        }

        // GET: CustomLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK,UserID,TaskTitle")] CustomList customList)
        {
            if (customList.UserID == null)
            {
                var userId = User.Identity.GetUserId();
                customList.UserID = userId;
            }

            if (ModelState.IsValid)
            {
                db.CustomLists.Add(customList);
                db.SaveChanges();
                return RedirectToAction("UpcomingEvents", "Calendar");
            }

            return View(customList);
        }

        // GET: CustomLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomList customList = db.CustomLists.Find(id);
            if (customList == null)
            {
                return HttpNotFound();
            }
            return View(customList);
        }

        // POST: CustomLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK,UserID,TaskTitle")] CustomList customList)
        {
            if (customList.UserID == null)
            {
                var userId = User.Identity.GetUserId();
                customList.UserID = userId;
            }

            if (ModelState.IsValid)
            {
                db.Entry(customList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UpcomingEvents", "Calendar");
            }
            return View(customList);
        }

        // GET: CustomLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomList customList = db.CustomLists.Find(id);
            if (customList == null)
            {
                return HttpNotFound();
            }
            return View(customList);
        }

        // POST: CustomLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomList customList = db.CustomLists.Find(id);
            db.CustomLists.Remove(customList);
            db.SaveChanges();
            return RedirectToAction("UpcomingEvents", "Calendar");
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