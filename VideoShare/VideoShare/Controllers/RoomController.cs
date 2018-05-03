using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VideoShare.Models;

namespace VideoShare.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class RoomController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Room
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            return View(await db.RoomModels.ToListAsync());
        }

        // GET: Room/Details/5
        // Action allows controls of the Video Feed
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RoomModels roomModels = await db.RoomModels.FindAsync(id);

            // User can only navigate to Details room page if they are the owner
            if (User.IsInRole("User") && User.Identity.Name != roomModels.RoomOwner)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(roomModels);
        }

        // GET: Room/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Room/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RoomID,RoomName,RoomCode,RoomUrl")] RoomModels roomModels)
        {
            // Room owner must be current logged in user
            roomModels.RoomOwner = User.Identity.Name;

            if (ModelState.IsValid)
            {
                db.RoomModels.Add(roomModels);
                await db.SaveChangesAsync();

                if (User.IsInRole("Admin"))
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Account", "User");
            }

            return View(roomModels);
        }

        // GET: Room/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomModels roomModels = await db.RoomModels.FindAsync(id);
            if (roomModels == null)
            {
                return HttpNotFound();
            }

            if (User.IsInRole("User") && User.Identity.Name != roomModels.RoomOwner)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(roomModels);
        }

        // POST: Room/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RoomID,RoomName,RoomCode,RoomUrl")] RoomModels roomModels)
        {
            RoomModels roomModel = await db.RoomModels.FindAsync(roomModels.RoomID);

            if (User.IsInRole("User") && User.Identity.Name != roomModel.RoomOwner)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            
            if (ModelState.IsValid)
            {
                roomModel.RoomCode = roomModels.RoomCode;
                roomModel.RoomName = roomModels.RoomName;
                roomModel.RoomUrl = roomModels.RoomUrl;

                db.Entry(roomModel).State = EntityState.Modified;
                await db.SaveChangesAsync();

                if (User.IsInRole("Admin"))
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Account", "User");
            }
            return View(roomModels);
        }

        // GET: Room/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomModels roomModels = await db.RoomModels.FindAsync(id);
            if (roomModels == null)
            {
                return HttpNotFound();
            }

            if (User.IsInRole("User") && User.Identity.Name != roomModels.RoomOwner)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(roomModels);
        }

        // POST: Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RoomModels roomModels = await db.RoomModels.FindAsync(id);

            if (User.IsInRole("User") && User.Identity.Name != roomModels.RoomOwner)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            db.RoomModels.Remove(roomModels);
            await db.SaveChangesAsync();

            if (User.IsInRole("Admin"))
                return RedirectToAction("Index");
            else
                return RedirectToAction("Account", "User");
        }

        public async Task<ActionResult> Lookup(string roomCode)
        {
            RoomModels roomModels = await db.RoomModels.
                FirstOrDefaultAsync(r => r.RoomCode == roomCode);

            if (roomModels == null)
            {
                return HttpNotFound();
            }
            
            return View(roomModels);
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
