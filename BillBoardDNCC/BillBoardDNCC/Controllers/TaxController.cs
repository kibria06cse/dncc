using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BillBoardDNCC.Models;
using BillBoardDNCC.Services;
using System.IO;

namespace BillBoardDNCC.Controllers
{
    [Authorize(Roles = "admin")]
    public class TaxController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Taxes
        public ActionResult Index()
        {
            var taxes = db.Taxes.Include(t => t.Billboard).Include(t => t.Company);
            return View(taxes.ToList());
        }

        // GET: Taxes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tax tax = db.Taxes.Find(id);
            if (tax == null)
            {
                return HttpNotFound();
            }
            return View(tax);
        }

        // GET: Taxes/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.BillboardId = new SelectList(db.BillBoards, "ID", "BillBoardUniqueKey");
            ViewBag.CompanyId = new SelectList(db.Companies, "ID", "Name");
            var model = new Tax();
            return View(model);
        }

        
        // POST: Taxes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BillboardId,CompanyId,Date,ChalanImageId,TotalTax,PaidTaxAmount,PaidTaxBankName,PurchaseOrder,BankAddress,PaidTaxDate,DueTaxAmount")] Tax tax, string Search)
        {
            if (Search == "Search")
            {
                var isBillboardAssignedForCompany = db.BillboardAssigneds.FirstOrDefault(x => x.BillboardId == tax.BillboardId && x.CompanyId == tax.CompanyId);
                if(isBillboardAssignedForCompany == null)
                {
                    var billboardUniqueKey = db.BillBoards.FirstOrDefault(x=>x.ID == tax.BillboardId).BillBoardUniqueKey;
                    var companyName = db.Companies.FirstOrDefault(x=>x.ID == tax.CompanyId).Name;
                    ModelState.AddModelError("", "Billboard Id: " + billboardUniqueKey + " is not assigned for this company: " + companyName);
                    ViewBag.BillboardId = new SelectList(db.BillBoards, "ID", "BillBoardUniqueKey", tax.BillboardId);
                    ViewBag.CompanyId = new SelectList(db.Companies, "ID", "Name", tax.CompanyId);
                    return View(tax);
                }
                var model =  CalculateTax(tax);
                ViewBag.BillboardId = new SelectList(db.BillBoards, "ID", "BillBoardUniqueKey", tax.BillboardId);
                ViewBag.CompanyId = new SelectList(db.Companies, "ID", "Name", tax.CompanyId);
                
                return View("Search", model);
            }
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        string photoId = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                        var path = Path.Combine(Server.MapPath("~/Upload/"), photoId);
                        file.SaveAs(path);

                        tax.ChalanImageId = photoId;
                    }
                }

                db.Taxes.Add(tax);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BillboardId = new SelectList(db.BillBoards, "ID", "BillBoardUniqueKey", tax.BillboardId);
            ViewBag.CompanyId = new SelectList(db.Companies, "ID", "Name", tax.CompanyId);
            return View(tax);
        }

        private Tax CalculateTax(Tax tax)
        {
            var billboard = db.BillBoards.FirstOrDefault(t => t.ID == tax.BillboardId);
            tax.Billboard = billboard;
            var billboardSize = Convert.ToDecimal(tax.Billboard.BillBoardType.Length) * Convert.ToDecimal(tax.Billboard.BillBoardType.Width);
            var billPerSqFtObj = db.RegionWiseTaxs.Where(i => i.Id== tax.Billboard.ZoneWardAreaId).FirstOrDefault();
            decimal billPerSqFt = 0;
            billPerSqFt = billPerSqFtObj != null? billPerSqFtObj.TaxPerSqFt:0;
            var totalBill = billboardSize * billPerSqFt;
            tax.TotalTax = totalBill;
            return tax;
        }

        // GET: Taxes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tax tax = db.Taxes.Find(id);
            if (tax == null)
            {
                return HttpNotFound();
            }
            ViewBag.BillboardId = new SelectList(db.BillBoards, "ID", "BillBoardUniqueKey", tax.BillboardId);
            ViewBag.CompanyId = new SelectList(db.Companies, "ID", "Name", tax.CompanyId);
            return View(tax);
        }

        // POST: Taxes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BillboardId,CompanyId,Date,ChalanImageId,TotalTax,PaidTaxAmount,PaidTaxBankName,PurchaseOrder,BankAddress,PaidTaxDate,DueTaxAmount")] Tax tax)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tax).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BillboardId = new SelectList(db.BillBoards, "ID", "BillBoardUniqueKey", tax.BillboardId);
            ViewBag.CompanyId = new SelectList(db.Companies, "ID", "Name", tax.CompanyId);
            return View(tax);
        }

        // GET: Taxes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tax tax = db.Taxes.Find(id);
            if (tax == null)
            {
                return HttpNotFound();
            }
            return View(tax);
        }

        // POST: Taxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tax tax = db.Taxes.Find(id);
            db.Taxes.Remove(tax);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult GetTaxDescription(int billboardId)
        {
            var billboard = db.BillBoards.FirstOrDefault(x => x.ID == billboardId);
            var billPerSqFtObj = db.RegionWiseTaxs.Where(i => i.Id== billboard.ZoneWardAreaId).FirstOrDefault();
            decimal billPerSqFt = 0;
            billPerSqFt = billPerSqFtObj != null? billPerSqFtObj.TaxPerSqFt:0;
            var msg = string.Empty;
            msg += "Length X Width X Tax/Square Ft = " + billboard.BillBoardType.Length + " X " + billboard.BillBoardType.Width + " X " + billPerSqFt;
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
