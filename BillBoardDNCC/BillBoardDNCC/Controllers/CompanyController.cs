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

namespace BillBoardDNCC.Controllers
{
    [Authorize(Roles = "admin")]
    public class CompanyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Companies
        public ActionResult Index()
        {
            return View(db.Companies.ToList());
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Address,TradeLicence,TradeLicenceImageID,TIN,TINImageID,ContactPerson,Mobile,Phone,Email,Website")] Company company,HttpPostedFileBase TradeLicenceFile, HttpPostedFileBase TINFile)
        {
            if(TINFile== null|| TradeLicenceFile == null)
            {
                ModelState.AddModelError("", "Please Upload all necessary documents");
            }
            if (ModelState.IsValid)
            {
                var TINimageID = CommonService.UploadFile(TINFile);
                var TradeLicenceImageId = CommonService.UploadFile(TradeLicenceFile);
                company.TINImageID = TINimageID;
                company.TradeLicenceImageID = TradeLicenceImageId;
                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(company);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Address,TradeLicence,TradeLicenceImageID,TIN,TINImageID,ContactPerson,Mobile,Phone,Email,Website")] Company company, HttpPostedFileBase TradeLicenceFile, HttpPostedFileBase TINFile)
        {
            if (ModelState.IsValid)
            {
                if (TINFile != null)
                {
                    var TINimageID = CommonService.UploadFile(TINFile);
                    company.TINImageID = TINimageID;
                }

                if (TradeLicenceFile != null)
                {
                    var TradeLicenceImageId = CommonService.UploadFile(TradeLicenceFile);
                    company.TradeLicenceImageID = TradeLicenceImageId;
                }

                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
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
