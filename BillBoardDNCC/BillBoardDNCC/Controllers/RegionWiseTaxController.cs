using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BillBoardDNCC.Models;

namespace BillBoardDNCC.Controllers
{
    [Authorize(Roles = "admin")]
    public class RegionWiseTaxController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RegionWiseTaxes
        public ActionResult Index()
        {
            return View(db.RegionWiseTaxs.ToList());
        }

        // GET: RegionWiseTaxes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegionWiseTax regionWiseTax = db.RegionWiseTaxs.Find(id);
            if (regionWiseTax == null)
            {
                return HttpNotFound();
            }
            return View(regionWiseTax);
        }

        // GET: RegionWiseTaxes/Create
        public ActionResult Create()
        {
            var zoneAndWard = db.ZoneWardAreas.ToList();
            int totalZone = 0;
            int totalWard = 0;
            if (zoneAndWard != null)
            {
                totalZone = zoneAndWard.Max(r => r.ZoneNo);
            }
            if (zoneAndWard != null)
            {
                totalWard = zoneAndWard.Max(r => r.WardNo);
            }
            ViewBag.totalZone = totalZone;
            ViewBag.totalWard = totalWard;

            return View();
        }

        // POST: RegionWiseTaxes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ZoneId,WardId,TaxPerSqFt")] RegionWiseTax regionWiseTax)
        {
            var zoneAndWard = db.ZoneAndWards.FirstOrDefault();
            int totalZone = 0;
            int totalWard = 0;
            if (zoneAndWard != null)
            {
                totalZone = zoneAndWard.TotalZone;
            }
            if (zoneAndWard != null)
            {
                totalWard = zoneAndWard.TotalWard;
            }
            ViewBag.totalZone = totalZone;
            ViewBag.totalWard = totalWard;


            if (ModelState.IsValid)
            {
                db.RegionWiseTaxs.Add(regionWiseTax);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(regionWiseTax);
        }

        // GET: RegionWiseTaxes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegionWiseTax regionWiseTax = db.RegionWiseTaxs.Find(id);
            if (regionWiseTax == null)
            {
                return HttpNotFound();
            }
            return View(regionWiseTax);
        }

        // POST: RegionWiseTaxes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ZoneId,WardId,TaxPerSqFt")] RegionWiseTax regionWiseTax)
        {
            if (ModelState.IsValid)
            {
                db.Entry(regionWiseTax).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(regionWiseTax);
        }

        // GET: RegionWiseTaxes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegionWiseTax regionWiseTax = db.RegionWiseTaxs.Find(id);
            if (regionWiseTax == null)
            {
                return HttpNotFound();
            }
            return View(regionWiseTax);
        }

        // POST: RegionWiseTaxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegionWiseTax regionWiseTax = db.RegionWiseTaxs.Find(id);
            db.RegionWiseTaxs.Remove(regionWiseTax);
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
