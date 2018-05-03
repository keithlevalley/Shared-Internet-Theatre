using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VideoShare.Models;

namespace VideoShare.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: User
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Users);
            //return View(db.UserModels.ToList());
        }

        // GET: User/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModels userModels = db.UserModels.Find(id);
            if (userModels == null)
            {
                return HttpNotFound();
            }
            return View(userModels);
        }

        public ActionResult Account()
        {
            /*UserModels userModels = db.UserModels.
                FirstOrDefault(model => model.UserName == User.Identity.Name);

            if (userModels == null)
            {
                return HttpNotFound();
            }
            */
            var roomModels = db.RoomModels.Where(r => r.RoomOwner == User.Identity.Name);

            return View(roomModels);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserName")] UserModels userModels)
        {
            if (ModelState.IsValid)
            {
                db.UserModels.Add(userModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userModels);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModels userModels = db.UserModels.Find(id);
            if (userModels == null)
            {
                return HttpNotFound();
            }
            return View(userModels);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserName")] UserModels userModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userModels);
        }

        // GET: User/Delete/5
        public ActionResult Delete(string id)
        {
            var _user = new ApplicationUser();
            _user = db.Users.Find(id);
            db.Users.Remove(_user);
            db.SaveChanges();

            return RedirectToAction("Index");

            /*
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModels userModels = db.UserModels.Find(id);
            if (userModels == null)
            {
                return HttpNotFound();
            }
            return View(userModels);
            */
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var _user = new ApplicationUser();
            _user = db.Users.Find(id);
            db.Users.Remove(_user);
            db.SaveChanges();

            return RedirectToAction("Index");

            /*
            UserModels userModels = db.UserModels.Find(id);
            db.UserModels.Remove(userModels);
            db.SaveChanges();
            return RedirectToAction("Index");
            */
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
