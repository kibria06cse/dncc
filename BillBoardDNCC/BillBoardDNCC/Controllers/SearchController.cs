using BillBoardDNCC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BillBoardDNCC.Controllers
{
    public class SearchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ByDate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchByDate(SearchViewModel viewmodel)
        {
            //var list = db.BillBoards.Where(x => x.DNCCReferenceDate >= viewmodel.From && x.DNCCReferenceDate <= viewmodel.To).ToList();
            //viewmodel.BillBoards = list;
            return View("ByDate", viewmodel);
        }
        public ActionResult ByCompany()
        {
            var Company = new SelectList(db.Companies.ToList(), "Id", "Name");
            ViewBag.Company = Company;
            return View();
        }
        public ActionResult ByBillboard()
        {
            var billboard = new SelectList(db.BillBoards.ToList(), "Id", "BillBoardUniqueKey");
            ViewBag.Billboard = billboard;
            return View();
        }
        [HttpPost]
        public ActionResult ByBillboardSearch(int BillboardId, DateTime From, DateTime To)
        {
            var model = db.Taxes.Where(i => i.BillboardId == BillboardId && i.Date >= From && i.Date <= To).ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult ByCompanySearch(int CompanyId, DateTime From, DateTime To)
        {
            var model = db.Taxes.Where(i => i.CompanyId == CompanyId && i.Date >= From && i.Date <= To).ToList();

            return View(model);
        }

        public ActionResult ByZone()
        {
            int totalZone = 0;
            var zoneWard = db.ZoneAndWards.FirstOrDefault();
            if (zoneWard != null)
            {
                totalZone = zoneWard.TotalZone;
            }
            ViewBag.totalZone = totalZone;
            return View();
        }

        public ActionResult ByWard()
        {
            int totalWard = 0;
            var zoneWard = db.ZoneAndWards.FirstOrDefault();
            if(zoneWard != null)
            {
                totalWard = zoneWard.TotalWard;
            }
            ViewBag.totalWard = totalWard;
            return View();
        }

        [HttpPost]
        public ActionResult ByZoneSearch(int ZoneId, DateTime From, DateTime To)
        {
            var model = db.Taxes.Where(i => i.Billboard.ZoneWardAreaId == ZoneId && i.Date >= From && i.Date <= To).ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult ByWardSearch(int WardId, DateTime From, DateTime To)
        {
            var model = db.Taxes.Where(i => i.Billboard.ZoneWardAreaId == WardId && i.Date >= From && i.Date <= To).ToList();

            return View(model);
        }


        public ActionResult Export()
        {
            string filename = TempData["ReportName "] != null ? TempData["ReportName "].ToString() + ".xls" : "filename.xls";
            StringBuilder sb = new StringBuilder();
            //static file name, can be changes as per requirement
            string sFileName = filename;
            //Bind data list from edmx
            var Data = (List<BillBoard>)null;
            if (TempData["Data"]!= null)
                Data =(List<BillBoard>)TempData["Data"];

            if (Data != null && Data.Any())
            {
                sb.Append("<table style='1px solid black; font-size:12px;'>");
                sb.Append("<tr>");
                sb.Append("<td style='width:120px;'><b>Company Name</b></td>");
                sb.Append("<td style='width:300px;'><b>Applicant Name</b></td>");
                sb.Append("<td style='width:120px;'><b>Address</b></td>");
                sb.Append("<td style='width:300px;'><b>DNCC Ref No</b></td>");
                sb.Append("<td style='width:120px;'><b>DNCC Ref Date</b></td>");
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
    }
}