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
    public class TransportPoolController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TransportPool
        public ActionResult Index()
        {
            var transportPools = db.TransportPools.Include(t => t.VehicleType);
            return View(transportPools.ToList());
        }

        // GET: TransportPool/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportPool transportPool = db.TransportPools.Find(id);
            if (transportPool == null)
            {
                return HttpNotFound();
            }
            return View(transportPool);
        }

        // GET: TransportPool/Create
        public ActionResult Create()
        {
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Name");
            return View();
        }

        // POST: TransportPool/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,VehicleTypeId,RegistrationNo,EngineNo,ChasisNo,Model,Manufacturer,Country,YearOfPurchase,ProcurementCost,Fund")] TransportPool transportPool)
        {
            if (ModelState.IsValid)
            {
                db.TransportPools.Add(transportPool);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Name", transportPool.VehicleTypeId);
            return View(transportPool);
        }

        // GET: TransportPool/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportPool transportPool = db.TransportPools.Find(id);
            if (transportPool == null)
            {
                return HttpNotFound();
            }
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Name", transportPool.VehicleTypeId);
            return View(transportPool);
        }

        // POST: TransportPool/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VehicleTypeId,RegistrationNo,EngineNo,ChasisNo,Model,Manufacturer,Country,YearOfPurchase,ProcurementCost,Fund")] TransportPool transportPool)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transportPool).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Name", transportPool.VehicleTypeId);
            return View(transportPool);
        }

        // GET: TransportPool/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportPool transportPool = db.TransportPools.Find(id);
            if (transportPool == null)
            {
                return HttpNotFound();
            }
            return View(transportPool);
        }

        // POST: TransportPool/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TransportPool transportPool = db.TransportPools.Find(id);
            db.TransportPools.Remove(transportPool);
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
