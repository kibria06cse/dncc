using BillBoardDNCC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BillBoardDNCC.Controllers
{
    public class ReportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Report
        public ActionResult Index()
        {
            return View();
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
        public ActionResult ByBillboardSearch(int BillboardId,DateTime From,DateTime To)
        {
            var model = db.Taxes.Where(i => i.BillboardId == BillboardId && i.Date>= From && i.Date<=To).ToList();
            var billboard = db.BillBoards.FirstOrDefault(i=>i.ID== BillboardId);
            TempData["Data"] = model;
            TempData["ReportName"]= "Report for Billboard:" + billboard.BillBoardUniqueKey +" From: " +From +" ,To: "+To;
            return View(model);
        }
        [HttpPost]
        public ActionResult ByCompanySearch(int CompanyId, DateTime From, DateTime To)
        {
            var model = db.Taxes.Where(i => i.CompanyId == CompanyId && i.Date >= From && i.Date <= To).ToList();
            var company = db.Companies.FirstOrDefault(c => c.ID == CompanyId);
            TempData["Data"] = model;
            TempData["ReportName"] = "Report for Company:" + company.Name + " From: " + From.ToShortDateString() + " ,To: " + To.ToShortDateString();
            
            return View(model);
        }

        public ActionResult TotalDueByCompany()
        {
            var Company = new SelectList(db.Companies.ToList(), "Id", "Name");
            ViewBag.Company = Company;
            return View();
        }
        [HttpPost]
        public ActionResult TotalDueByCompanySearch(int CompanyId, DateTime From, DateTime To)
        {
            var model = db.Taxes.Where(i => i.CompanyId == CompanyId && i.Date >= From && i.Date <= To).ToList();
            var company = db.Companies.FirstOrDefault(i => i.ID == CompanyId);
            TempData["Data"] = model;
            TempData["ReportName"] = "Report for Company:" + company+ " From: " + From + " ,To: " + To;
            ViewBag.CompanyId = CompanyId;
            ViewBag.From = From;
            ViewBag.To = To;
            return View(model);
           
        }
       
        public ActionResult TotalDueByBillboard()
        {
            var billboard = new SelectList(db.BillBoards.ToList(), "Id", "BillBoardUniqueKey");
            ViewBag.Billboard = billboard;
            return View();
        }
        [HttpPost]
        public ActionResult TotalDueByBillboardSearch(int BillboardId, DateTime From, DateTime To)
        {
            var model = db.Taxes.Where(i => i.BillboardId == BillboardId && i.Date >= From && i.Date <= To).ToList();
            var billboard = db.BillBoards.FirstOrDefault(i => i.ID == BillboardId);
            Session["Data"] = model;
            TempData["ReportName"] = "Report for Billboard:" + billboard.BillBoardUniqueKey + " From: " + From + " ,To: " + To;
            
            return View(model);
        }
        
        [HttpGet]
        public ActionResult Export()
        {
            string filename = Session["Name"] != null ? Session["Name"].ToString() + ".xls" : "filename.xls";
            //string filename = Session["ReportName"] as string;
            StringBuilder sb = new StringBuilder();
            //static file name, can be changes as per requirement
            string sFileName = filename;
            //Bind data list from edmx
            var Data = (List<Tax>)null;
            if (Session["Data"] != null)
                Data = (List<Tax>)Session["Data"];
            //char taka = '\u09F3';
            if (Data != null && Data.Any())
            {
                sb.Append("<table style='1px solid black; font-size:12px;'>");
                sb.Append("<tr>");
                sb.Append("<td style='width:120px;'><b>BillBoard Unique Key</b></td>");
                sb.Append("<td style='width:300px;'><b>Company Name</b></td>");
                sb.Append("<td style='width:120px;'><b>Date</b></td>");
                sb.Append("<td style='width:120px;'><b>Total Tax</b></td>");
                sb.Append("<td style='width:120px;'><b>Paid Tax</b></td>");
                sb.Append("<td style='width:300px;'><b>Due Tax Amount</b></td>");
                sb.Append("</tr>");

                foreach (var result in Data)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + result.Billboard.BillBoardUniqueKey + "</td>");
                    //sb.Append("<td>" + result.PaidTaxDate + "</td>");
                    sb.Append("<td>" + result.Company.Name + "</td>");
                    //sb.Append("<td>" + result.DueTax + "</td>");
                    sb.Append("<td>" + result.Date + "</td>");
                    sb.Append("<td>" + result.TotalTax + "</td>");
                    sb.Append("<td>" + result.PaidTaxAmount + "</td>");
                    sb.Append("<td>" + result.DueTaxAmount + "</td>");
                    sb.Append("</tr>");
                }
            }
            //HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            Response.AddHeader("content-disposition", "attachment; filename=\"" + sFileName + "\"");
            this.Response.ContentType = "application/vnd.ms-excel";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(buffer, "application/vnd.ms-excel");
        }

        [HttpGet]
        public ActionResult ExportTotalAmountCollected()
        {
            string filename = Session["Name"] != null ? Session["Name"].ToString() + ".xls" : "filename.xls";
            //string filename = Session["ReportName"] as string;
            StringBuilder sb = new StringBuilder();
            //static file name, can be changes as per requirement
            string sFileName = filename;
            //Bind data list from edmx
            var Data = (List<Tax>)null;
            if (Session["Data"] != null)
                Data = (List<Tax>)Session["Data"];

            if (Data != null && Data.Any())
            {
                sb.Append("<table style='1px solid black; font-size:12px;'>");
                sb.Append("<tr>");
                sb.Append("<td style='width:120px;'><b>BillBoard Unique Key</b></td>");
                sb.Append("<td style='width:300px;'><b>Company Name</b></td>");
                sb.Append("<td style='width:120px;'><b>Date</b></td>");
                sb.Append("<td style='width:120px;'><b>Total Tax</b></td>");
                sb.Append("<td style='width:120px;'><b>Paid Tax</b></td>");
                sb.Append("<td style='width:300px;'><b>Paid Tax Bank Name</b></td>");
                sb.Append("<td style='width:120px;'><b>Purchase Order</b></td>");
                sb.Append("<td style='width:120px;'><b>Bnak Address</b></td>");
                sb.Append("<td style='width:120px;'><b>Paid Tax Date</b></td>");
                sb.Append("<td style='width:300px;'><b>Due Tax Amount</b></td>");
                sb.Append("</tr>");

                //char taka = '\u09F3';
                foreach (var result in Data)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + result.Billboard.BillBoardUniqueKey + "</td>");
                    //sb.Append("<td>" + result.PaidTaxDate + "</td>");
                    sb.Append("<td>" + result.Company.Name + "</td>");
                    //sb.Append("<td>" + result.DueTax + "</td>");
                    sb.Append("<td>" + result.Date + "</td>");
                    sb.Append("<td>" + result.TotalTax.ToString("#,##0.00") + "</td>");
                    sb.Append("<td>" + result.PaidTaxAmount.ToString("#,##0.00") + "</td>");
                    sb.Append("<td>" + result.PaidTaxBankName + "</td>");
                    sb.Append("<td>" + result.PurchaseOrder + "</td>");
                    sb.Append("<td>" + result.BankAddress + "</td>");
                    sb.Append("<td>" + result.PaidTaxDate + "</td>");
                    sb.Append("<td>" + result.DueTaxAmount.ToString("#,##0.00") + "</td>");
                    sb.Append("</tr>");
                }
            }
            //HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            Response.AddHeader("content-disposition", "attachment; filename=\"" + sFileName + "\"");
            this.Response.ContentType = "application/vnd.ms-excel";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(buffer, "application/vnd.ms-excel");
        }

    }
}