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
    public class ZoneAndWardsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ZoneAndWards
        public ActionResult Index()
        {
            return View(db.ZoneAndWards.ToList());
        }

        // GET: ZoneAndWards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZoneAndWard zoneAndWard = db.ZoneAndWards.Find(id);
            if (zoneAndWard == null)
            {
                return HttpNotFound();
            }
            return View(zoneAndWard);
        }

        // GET: ZoneAndWards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ZoneAndWards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TotalZone,TotalWard")] ZoneAndWard zoneAndWard)
        {
            if (ModelState.IsValid)
            {
                db.ZoneAndWards.Add(zoneAndWard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(zoneAndWard);
        }

        // GET: ZoneAndWards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZoneAndWard zoneAndWard = db.ZoneAndWards.Find(id);
            if (zoneAndWard == null)
            {
                return HttpNotFound();
            }
            return View(zoneAndWard);
        }

        // POST: ZoneAndWards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TotalZone,TotalWard")] ZoneAndWard zoneAndWard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zoneAndWard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zoneAndWard);
        }

        // GET: ZoneAndWards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZoneAndWard zoneAndWard = db.ZoneAndWards.Find(id);
            if (zoneAndWard == null)
            {
                return HttpNotFound();
            }
            return View(zoneAndWard);
        }

        // POST: ZoneAndWards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ZoneAndWard zoneAndWard = db.ZoneAndWards.Find(id);
            db.ZoneAndWards.Remove(zoneAndWard);
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
