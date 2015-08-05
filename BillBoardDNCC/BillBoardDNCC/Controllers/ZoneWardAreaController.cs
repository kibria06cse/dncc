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
    public class ZoneWardAreaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ZoneWardArea
        public ActionResult Index()
        {
            return View(db.ZoneWardAreas.ToList());
        }

        // GET: ZoneWardArea/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZoneWardArea zoneWardArea = db.ZoneWardAreas.Find(id);
            if (zoneWardArea == null)
            {
                return HttpNotFound();
            }
            return View(zoneWardArea);
        }

        // GET: ZoneWardArea/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ZoneWardArea/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ZoneNo,WardNo,Area")] ZoneWardArea zoneWardArea)
        {
            if (ModelState.IsValid)
            {
                db.ZoneWardAreas.Add(zoneWardArea);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(zoneWardArea);
        }

        // GET: ZoneWardArea/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZoneWardArea zoneWardArea = db.ZoneWardAreas.Find(id);
            if (zoneWardArea == null)
            {
                return HttpNotFound();
            }
            return View(zoneWardArea);
        }

        // POST: ZoneWardArea/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ZoneNo,WardNo,Area")] ZoneWardArea zoneWardArea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zoneWardArea).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zoneWardArea);
        }

        // GET: ZoneWardArea/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZoneWardArea zoneWardArea = db.ZoneWardAreas.Find(id);
            if (zoneWardArea == null)
            {
                return HttpNotFound();
            }
            return View(zoneWardArea);
        }

        // POST: ZoneWardArea/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ZoneWardArea zoneWardArea = db.ZoneWardAreas.Find(id);
            db.ZoneWardAreas.Remove(zoneWardArea);
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
