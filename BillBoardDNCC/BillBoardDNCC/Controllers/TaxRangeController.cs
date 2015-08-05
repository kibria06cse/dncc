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
    public class TaxRangeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TaxRanges
        public ActionResult Index()
        {
            return View(db.TaxRanges.ToList());
        }

        // GET: TaxRanges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxRange taxRange = db.TaxRanges.Find(id);
            if (taxRange == null)
            {
                return HttpNotFound();
            }
            return View(taxRange);
        }

        // GET: TaxRanges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaxRanges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TaxFrom,TaxTo")] TaxRange taxRange)
        {
            if (ModelState.IsValid)
            {
                db.TaxRanges.Add(taxRange);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taxRange);
        }

        // GET: TaxRanges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxRange taxRange = db.TaxRanges.Find(id);
            if (taxRange == null)
            {
                return HttpNotFound();
            }
            return View(taxRange);
        }

        // POST: TaxRanges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TaxFrom,TaxTo")] TaxRange taxRange)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taxRange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taxRange);
        }

        // GET: TaxRanges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxRange taxRange = db.TaxRanges.Find(id);
            if (taxRange == null)
            {
                return HttpNotFound();
            }
            return View(taxRange);
        }

        // POST: TaxRanges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaxRange taxRange = db.TaxRanges.Find(id);
            db.TaxRanges.Remove(taxRange);
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
