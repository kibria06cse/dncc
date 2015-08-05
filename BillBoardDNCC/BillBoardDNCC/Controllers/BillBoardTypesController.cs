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
    public class BillBoardTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BillBoardTypes
        public ActionResult Index()
        {
            return View(db.BillBoardTypes.ToList());
        }

        // GET: BillBoardTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillBoardType billBoardType = db.BillBoardTypes.Find(id);
            if (billBoardType == null)
            {
                return HttpNotFound();
            }
            return View(billBoardType);
        }

        // GET: BillBoardTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BillBoardTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BillBoardType billBoardType)
        {
            if (ModelState.IsValid)
            {
                db.BillBoardTypes.Add(billBoardType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(billBoardType);
        }

        // GET: BillBoardTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillBoardType billBoardType = db.BillBoardTypes.Find(id);
            if (billBoardType == null)
            {
                return HttpNotFound();
            }
            return View(billBoardType);
        }

        // POST: BillBoardTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BillBoardType billBoardType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billBoardType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(billBoardType);
        }

        // GET: BillBoardTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillBoardType billBoardType = db.BillBoardTypes.Find(id);
            if (billBoardType == null)
            {
                return HttpNotFound();
            }
            return View(billBoardType);
        }

        // POST: BillBoardTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BillBoardType billBoardType = db.BillBoardTypes.Find(id);
            db.BillBoardTypes.Remove(billBoardType);
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
