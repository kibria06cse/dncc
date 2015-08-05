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
    public class BillboardAssignedController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BillboardAssigneds
        public ActionResult Index()
        {
            var billboardAssigneds = db.BillboardAssigneds.Include(b => b.Billboard).Include(b => b.Company);
            ViewBag.Billboard = new SelectList(db.BillBoards.ToList(), "Id", "BillBoardUniqueKey");
            ViewBag.Company = new SelectList(db.Companies.ToList(), "Id", "Name");
            return View(billboardAssigneds.ToList());
        }
        [HttpPost]
        public ActionResult Index(int? billboardId, int? companyId)
        {
            var billboardAssigneds = db.BillboardAssigneds.Include(b => b.Billboard).Include(b => b.Company);
            if (billboardId.HasValue && companyId.HasValue)
                billboardAssigneds = billboardAssigneds.Where(i => i.BillboardId == billboardId && i.CompanyId == companyId);
            else if (billboardId.HasValue)
                billboardAssigneds = billboardAssigneds.Where(i => i.BillboardId == billboardId);
            else if (companyId.HasValue)
                billboardAssigneds = billboardAssigneds.Where(i => i.CompanyId == companyId);
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.Billboard = new SelectList(db.BillBoards.ToList(), "Id", "BillBoardUniqueKey");
            ViewBag.Company = new SelectList(db.Companies.ToList(), "Id", "Name");

            return View(billboardAssigneds.ToList());
        }
        // GET: BillboardAssigneds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillboardAssigned billboardAssigned = db.BillboardAssigneds.Find(id);
            if (billboardAssigned == null)
            {
                return HttpNotFound();
            }
            return View(billboardAssigned);
        }

        // GET: BillboardAssigneds/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.BillboardId = new SelectList(db.BillBoards, "ID", "BillBoardUniqueKey");
            ViewBag.CompanyId = new SelectList(db.Companies, "ID", "Name");
            return View();
        }

        // POST: BillboardAssigneds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BillboardId,CompanyId,DateFrom,DateTo,DNCCRefNo,DNCCRefDate")] BillboardAssigned billboardAssigned)
        {
            var billboard = db.BillboardAssigneds.FirstOrDefault(x => x.BillboardId == billboardAssigned.BillboardId);
            if (billboard != null)
                ModelState.AddModelError("", billboard.Billboard.BillBoardUniqueKey + " has been assigned previously.");
            if (ModelState.IsValid)
            {
                db.BillboardAssigneds.Add(billboardAssigned);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BillboardId = new SelectList(db.BillBoards, "ID", "BillBoardUniqueKey", billboardAssigned.BillboardId);
            ViewBag.CompanyId = new SelectList(db.Companies, "ID", "Name", billboardAssigned.CompanyId);
            return View(billboardAssigned);
        }

        // GET: BillboardAssigneds/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillboardAssigned billboardAssigned = db.BillboardAssigneds.Find(id);
            if (billboardAssigned == null)
            {
                return HttpNotFound();
            }
            ViewBag.BillboardId = new SelectList(db.BillBoards, "ID", "BillBoardUniqueKey", billboardAssigned.BillboardId);
            ViewBag.CompanyId = new SelectList(db.Companies, "ID", "Name", billboardAssigned.CompanyId);
            return View(billboardAssigned);
        }

        // POST: BillboardAssigneds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BillboardId,CompanyId,DateFrom,DateTo,DNCCRefNo,DNCCRefDate")] BillboardAssigned billboardAssigned)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billboardAssigned).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BillboardId = new SelectList(db.BillBoards, "ID", "BillBoardUniqueKey", billboardAssigned.BillboardId);
            ViewBag.CompanyId = new SelectList(db.Companies, "ID", "Name", billboardAssigned.CompanyId);
            return View(billboardAssigned);
        }

        // GET: BillboardAssigneds/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillboardAssigned billboardAssigned = db.BillboardAssigneds.Find(id);
            if (billboardAssigned == null)
            {
                return HttpNotFound();
            }
            return View(billboardAssigned);
        }

        // POST: BillboardAssigneds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BillboardAssigned billboardAssigned = db.BillboardAssigneds.Find(id);
            db.BillboardAssigneds.Remove(billboardAssigned);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult GetBillboardDescription(int id)
        {
            var billboard = db.BillBoards.Where(i => i.ID == id).FirstOrDefault();
            //var obj = new { zone = area.ZoneNo, ward = area.WardNo };
            var msg = string.Empty;
            msg += "Type: " + billboard.BillBoardType.Name + ", Area: " + billboard.ZoneWardArea.Area + ",Zone: " + billboard.ZoneWardArea.ZoneNo + ", Ward: " + billboard.ZoneWardArea.WardNo + ", Address: " + billboard.Address + ", Length X Width: " + billboard.BillBoardType.Length + " X " + billboard.BillBoardType.Width;
            return Json(msg, JsonRequestBehavior.AllowGet);
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
