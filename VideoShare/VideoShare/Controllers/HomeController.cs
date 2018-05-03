using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoShare.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace VideoShare.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "Admin, User")]
        public ActionResult Index()
        {
            ViewBag.Title = "Shared Internet Theatre";
            return View();
        }

        // This function is not used.  Lookup Action from Controller Room is called
        //     directly inside the Index View
        /*
        public ActionResult FindRoom(string roomCode)
        {
            ViewBag.Message = "Find a Room page";

            return RedirectToAction("Lookup", "Room", new { roomCode = roomCode });

            //return View();
        }
        */

        public ActionResult YourRoom()
        {
            ViewBag.Message = "Your Room page";

            return RedirectToAction("Account", "User");
        }
    }
}