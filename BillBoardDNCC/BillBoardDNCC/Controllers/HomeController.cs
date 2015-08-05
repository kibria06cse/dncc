using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BillBoardDNCC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole("mech_admin"))
                return RedirectToAction("Mechanical", "Home");
            if (User.IsInRole("aminbazar_admin"))
                return RedirectToAction("AminBazarLandField", "Home");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize(Roles = "mech_admin")]
        public ActionResult Mechanical()
        {
            return View();
        }
         [Authorize(Roles = "aminbazar_admin")]
        public ActionResult AminbazarLandField()
        {
            return View();
        }
    }
}