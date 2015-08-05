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
using System.Data.OleDb;
using System.Data.SqlClient;

namespace BillBoardDNCC.Controllers
{
    public class CollectionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Collection
        public ActionResult Index()
        {
            return View(db.Collections.ToList());
        }

        // GET: Collection/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // GET: Collection/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Collection/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LicencePlateNo,DateTime,ZoneNo,WardNo,TotalWeight")] Collection collection)
        {
            if (ModelState.IsValid)
            {
                db.Collections.Add(collection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(collection);
        }

        // GET: Collection/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // POST: Collection/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LicencePlateNo,DateTime,ZoneNo,WardNo,TotalWeight")] Collection collection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(collection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(collection);
        }

        // GET: Collection/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // POST: Collection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Collection collection = db.Collections.Find(id);
            db.Collections.Remove(collection);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ConvertToSQLFromMDB()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ConvertToSQLFromMDB(HttpPostedFileBase collectionReportFile)
        {
            if (collectionReportFile.ContentType != "application/msaccess")
            {
                ModelState.AddModelError("", "Please Upload an mdb file");
            }
          
            var fileId=  CommonService.UploadFile(collectionReportFile);
            if (fileId == null)
                ModelState.AddModelError("", "Sorry ! Unable to upload the given file. Please try again.");
            else
                InsertToSQL(fileId);

            return View();
        }

        public void InsertToSQL(string fileId)
        {
            string path = "~/Upload/" + fileId;
            string strFile = Server.MapPath(path);
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFile;
            var myDataTable = new DataTable();
            using (var connection = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;" + "data source=" + strFile))
            {
                connection.Open();
                var query = "SELECT * FROM Test";
                var command = new OleDbCommand(query, connection);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    var list = new List<Collection>();
                    while (reader.Read())
                    {
                        //var id = (reader.GetInt32(0).ToString() + " - " + reader.GetString(1) + " - " + reader.GetDateTime(2) );
                        var collection = new Collection() {
                            LicencePlateNo = reader.GetString(1),
                            DateTime = reader.GetDateTime(2),
                            ZoneNo = reader.GetInt32(3),
                            WardNo = reader.GetInt32(4),
                            TotalWeight = reader.GetDouble(5)
                            //mdbFileID = fileId
                        };
                        list.Add(collection);
                    }
                    list.ForEach(n => db.Collections.Add(n));
                    db.SaveChanges();
                }
                

                connection.Close();
            }
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
