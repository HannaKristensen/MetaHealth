using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MetaHealth;
using MetaHealth.DAL;
using Microsoft.AspNet.Identity;

namespace MetaHealth.Controllers
{
    public class AspNetUsersController : Controller
    {
        private Model db = new Model();

        // GET: AspNetUsers/Edit/5
       
        public ActionResult Edit(string id)
        {
            var userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(userId);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);   
        }
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserName")] AspNetUser aspNetUser)
        {
            aspNetUser.Id = User.Identity.GetUserId();
            var oldUser = db.AspNetUsers.Find(aspNetUser.Id);
            var newUser = aspNetUser;
            aspNetUser = oldUser;
            aspNetUser.UserName = newUser.UserName;
            db.Entry(aspNetUser).Property(x => x.UserName).IsModified = true; 
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("UpcomingEvents","Calendar");
            }
            return View(aspNetUser);
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
