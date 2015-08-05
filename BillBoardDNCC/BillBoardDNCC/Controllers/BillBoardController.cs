using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BillBoardDNCC.Models;
using System.IO;
using System.Text;

namespace BillBoardDNCC.Controllers
{
    public class BillBoardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BillBoard
        [HttpGet]
        public ActionResult Index()
        {
            var model = db.BillBoards.ToList();
            ViewBag.AreaList = new SelectList(db.ZoneWardAreas.ToList(), "Id", "Area");
            ViewBag.TypeList = new SelectList(db.BillBoardTypes.ToList(), "Id", "Name");
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(int? AreaId,int? TypeId)
        {
            var model = (List<BillBoard>)null;
            //if (AreaId.HasValue == false && TypeId.HasValue == false)
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (AreaId.HasValue && TypeId.HasValue)
                model = db.BillBoards.Where(i => i.ZoneWardAreaId == AreaId && i.BillBoardTypeId == TypeId).ToList();
            else if (AreaId.HasValue)
                model = db.BillBoards.Where(i => i.ZoneWardAreaId == AreaId).ToList();
            else if (TypeId.HasValue)
                model = db.BillBoards.Where(i => i.BillBoardTypeId == TypeId).ToList();
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.AreaList = new SelectList(db.ZoneWardAreas.ToList(), "Id", "Area");
            ViewBag.TypeList = new SelectList(db.BillBoardTypes.ToList(), "Id", "Name");
            return View(model);
        }
        // GET: BillBoard/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillBoard billBoard = db.BillBoards.Find(id);
            if (billBoard == null)
            {
                return HttpNotFound();
            }
            return View(billBoard);
        }

        // GET: BillBoard/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.Company = new SelectList(db.Companies.ToList(), "Id", "Name");
            ViewBag.BillBoardType = new SelectList(db.BillBoardTypes.ToList(), "Id", "Name");
            var billboardSizes = db.BillBoardSizes.ToList();

            var billboardSizeList = billboardSizes.Select(e => new SelectListItem() { Text = string.Format("{0} X {1}", e.Height, e.Width), Value = e.ID.ToString() });
            var ZoneWardAreas = db.ZoneWardAreas.ToList();
            var areaList = ZoneWardAreas.Select(e => new SelectListItem() { Text = string.Format("{0}", e.Area), Value = e.Id.ToString() });
            ViewBag.AreaList = areaList;

            ViewBag.BillBoardSize = billboardSizeList;
            var zoneAndWard = db.ZoneWardAreas.ToList();
            int totalZone = 0;
            int totalWard = 0;
            if (zoneAndWard != null)
            {
                totalZone = zoneAndWard.Max(r=>r.ZoneNo);
            }
            if (zoneAndWard != null)
            {
                totalWard = zoneAndWard.Max(r=>r.WardNo);
            }
            ViewBag.totalZone = totalZone;
            ViewBag.totalWard = totalWard;
            var taxRanges = db.TaxRanges.ToList();

            var taxRangeDDL = taxRanges.Select(e => new SelectListItem() { Text = string.Format("{0} - {1}", e.TaxFrom, e.TaxTo), Value = e.ID.ToString() });


            ViewBag.TaxRange = taxRangeDDL;
            return View();
        }

        // POST: BillBoard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BillBoard billBoard, HttpPostedFileBase photo)
        {
            ViewBag.Company = new SelectList(db.Companies.ToList(), "Id", "Name");
            ViewBag.BillBoardType = new SelectList(db.BillBoardTypes.ToList(), "Id", "Name");
            //ViewBag.BillBoardSize = new SelectList(db.BillBoardSizes.ToList(), "Id", "Name");
            var zoneAndWard = db.ZoneAndWards.FirstOrDefault();
            int totalZone = 0;
            int totalWard = 0;
            if(zoneAndWard != null)
            {
                totalZone = zoneAndWard.TotalZone;
            }
            if (zoneAndWard != null)
            {
                totalWard = zoneAndWard.TotalWard;
            }
            ViewBag.totalZone = totalZone;
            ViewBag.totalWard = totalWard;
            var billboardSizes = db.BillBoardSizes.ToList();

            var billboardSizeList = billboardSizes.Select(e => new SelectListItem() { Text=string.Format("{0} X {1}",e.Height,e.Width),Value=e.ID.ToString()});

            ViewBag.BillBoardSize = billboardSizeList;

            var taxRanges = db.TaxRanges.ToList();

            var taxRangeDDL= taxRanges.Select(e => new SelectListItem() { Text = string.Format("{0} - {1}", e.TaxFrom, e.TaxTo), Value = e.ID.ToString() });


            ViewBag.TaxRange = taxRangeDDL;
          

            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    string photoId = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                    var path = Path.Combine(Server.MapPath("~/Images/"), photoId);
                    file.SaveAs(path);

                }
            }
            if (ModelState.IsValid)
            {
                var billboardWithGeneratedKey = Services.CommonService.GenerateBillBoardId(billBoard);

                db.BillBoards.Add(billboardWithGeneratedKey);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(billBoard);
        }

        // GET: BillBoard/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Company = new SelectList(db.Companies.ToList(), "Id", "Name");
            ViewBag.BillBoardType = new SelectList(db.BillBoardTypes.ToList(), "Id", "Name");
            //ViewBag.BillBoardSize = new SelectList(db.BillBoardSizes.ToList(), "Id", "Name");
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
            var billboardSizes = db.BillBoardSizes.ToList();

            var billboardSizeList = billboardSizes.Select(e => new SelectListItem() { Text = string.Format("{0} X {1}", e.Height, e.Width), Value = e.ID.ToString() });

            ViewBag.BillBoardSize = billboardSizeList;

            var taxRanges = db.TaxRanges.ToList();

            var taxRangeDDL = taxRanges.Select(e => new SelectListItem() { Text = string.Format("{0} - {1}", e.TaxFrom, e.TaxTo), Value = e.ID.ToString() });


            ViewBag.TaxRange = taxRangeDDL;
            var ZoneWardAreas = db.ZoneWardAreas.ToList();
            
            var areaList = ZoneWardAreas.Select(e => new SelectListItem() { Text = string.Format("{0}", e.Area), Value = e.Id.ToString() });
            ViewBag.AreaList = areaList;
            BillBoard billBoard = db.BillBoards.Find(id);
            if (billBoard == null)
            {
                return HttpNotFound();
            }
            return View(billBoard);
        }

        // POST: BillBoard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BillBoard billBoard, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                var billboardWithGeneratedKey = Services.CommonService.GenerateBillBoardId(billBoard);
                db.Entry(billBoard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(billBoard);
        }

        // GET: BillBoard/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillBoard billBoard = db.BillBoards.Find(id);
            if (billBoard == null)
            {
                return HttpNotFound();
            }
            return View(billBoard);
        }

        // POST: BillBoard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BillBoard billBoard = db.BillBoards.Find(id);
            db.BillBoards.Remove(billBoard);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Export()
        {
            StringBuilder sb = new StringBuilder();
            //static file name, can be changes as per requirement
            string sFileName = "filename.xls";
            //Bind data list from edmx
            var Data = db.BillBoards.ToList();
            if (Data != null && Data.Any())
            {
                sb.Append("<table style='1px solid black; font-size:12px;'>");
                sb.Append("<tr>");
                sb.Append("<td style='width:120px;'><b>Company Name</b></td>");
                sb.Append("<td style='width:300px;'><b>Applicant Name</b></td>");
                sb.Append("<td style='width:120px;'><b>Address</b></td>");
                sb.Append("<td style='width:300px;'><b>DNCC Ref No</b></td>");
                sb.Append("<td style='width:120px;'><b>DNCC Ref Date</b></td>");
                sb.Append("<td style='width:300px;'><b>Place of Permission</b></td>");
                sb.Append("<td style='width:120px;'><b>Bill Board Type</b></td>");
                sb.Append("<td style='width:120px;'><b>Paid Tax Amount</b></td>");
                sb.Append("<td style='width:300px;'><b>Due Tax Amoun</b></td>");
                //sb.Append("<td style='width:300px;'><b>Image</b></td>");
                sb.Append("</tr>");

                foreach (var result in Data)
                {
                    sb.Append("<tr>");
                    //sb.Append("<td>" + result.CompanyName + "</td>");
                    //sb.Append("<td>" + result.ApplicantName + "</td>");
                    //sb.Append("<td>" + result.Address + "</td>");
                    sb.Append("<td>" + "" + "</td>");
                    sb.Append("<td>" + "" + "</td>");
                    sb.Append("<td>" + "" + "</td>");
                    //sb.Append("<td>" + result.DNCCReferenceNo + "</td>");
                    sb.Append("<td>" + "" + "</td>");
                    //sb.Append("<td>" + result.DNCCReferenceDate + "</td>");
                    sb.Append("<td>" + "" + "</td>");
                    sb.Append("<td>" + result.BillBoardType + "</td>");
                    //sb.Append("<td>" + result.PaidTaxDate + "</td>");
                    sb.Append("<td>" + "" + "</td>");
                    //sb.Append("<td>" + result.DueTax + "</td>");
                    sb.Append("<td>" + "" + "</td>");
                    //sb.Append("<td>" + doc.Load(true)+ "</td>");
                    sb.Append("</tr>");
                }
            }
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            this.Response.ContentType = "application/vnd.ms-excel";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(buffer, "application/vnd.ms-excel");
        }

        [HttpPost]
        public JsonResult GetZoneAndWard(int id)
        {
            var area = db.ZoneWardAreas.Where(i => i.Id == id).FirstOrDefault();
            var obj = new { zone = area.ZoneNo, ward = area.WardNo };
            return Json(obj, JsonRequestBehavior.AllowGet);
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
