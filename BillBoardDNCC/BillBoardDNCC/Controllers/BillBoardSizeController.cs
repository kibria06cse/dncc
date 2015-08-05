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
    [Authorize(Roles="admin")]
    public class BillBoardSizeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BillBoardSizes
        public ActionResult Index()
        {
            return View(db.BillBoardSizes.ToList());
        }

        // GET: BillBoardSizes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillBoardSize billBoardSize = db.BillBoardSizes.Find(id);
            if (billBoardSize == null)
            {
                return HttpNotFound();
            }
            return View(billBoardSize);
        }

        // GET: BillBoardSizes/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BillBoardSizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Height,Width")] BillBoardSize billBoardSize)
        {
            if (ModelState.IsValid)
            {
                db.BillBoardSizes.Add(billBoardSize);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(billBoardSize);
        }

        // GET: BillBoardSizes/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillBoardSize billBoardSize = db.BillBoardSizes.Find(id);
            if (billBoardSize == null)
            {
                return HttpNotFound();
            }
            return View(billBoardSize);
        }

        // POST: BillBoardSizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Height,Width")] BillBoardSize billBoardSize)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billBoardSize).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(billBoardSize);
        }
        [Authorize(Roles = "admin")]
        // GET: BillBoardSizes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillBoardSize billBoardSize = db.BillBoardSizes.Find(id);
            if (billBoardSize == null)
            {
                return HttpNotFound();
            }
            return View(billBoardSize);
        }

        // POST: BillBoardSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BillBoardSize billBoardSize = db.BillBoardSizes.Find(id);
            db.BillBoardSizes.Remove(billBoardSize);
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
