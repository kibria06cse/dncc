using BillBoardDNCC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BillBoardDNCC.Services
{
    public static class CommonService
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static string UploadFile(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                string photoId = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Upload/"), photoId);
                file.SaveAs(path);

                return photoId;
            }
            else
                return null;
        }

        public static BillBoard GenerateBillBoardId(BillBoard model)
        {
            var type = db.BillBoardTypes.FirstOrDefault(i => i.ID == model.BillBoardTypeId);
            //var size = db.BillBoardSizes.FirstOrDefault(i => i.ID == model.BillBoardSizeId);
            int lastId;
            //var list = db.BillBoards.Where(i => i.BillBoardTypeId == model.BillBoardTypeId && i.ZoneId == model.ZoneId && i.WardId == model.WardId);
            //ToDo: Need To change here.
            var list = db.BillBoards.Where(i => i.BillBoardTypeId == model.BillBoardTypeId && i.ZoneWardAreaId == model.ZoneWardAreaId);
            var shortedList = list.OrderByDescending(i => i.BillBoardLastNo).ToList();

            if (shortedList != null && shortedList.Count > 0)
            {
                int i = shortedList.First().BillBoardLastNo;
                i++;
                model.BillBoardLastNo = i;
                lastId = i;
            }
            else
            { 
                lastId = 1;
                model.BillBoardLastNo = 1;
            }
            //lastId = list.Max(r => r.BillBoardLastNo) ;
            //lastId++;
            
            var result = String.Format("{0:0000}", lastId);
            var result2 = lastId.ToString("0000");

            var billboardType = db.BillBoardTypes.FirstOrDefault(x => x.ID == model.BillBoardTypeId);
            var zoneWard = db.ZoneWardAreas.FirstOrDefault(i => i.Id == model.ZoneWardAreaId);
            //var id = type.ShortCode + "-Z" + model.ZoneId + " W" + model.WardId + " " +size.Height + "F X " + size.Width + "F " + result;
            //ToDo: Need TO change here--- Size , ward id , zone id
            var id = type.ShortCode + "-Z" + zoneWard.ZoneNo + " W" + zoneWard.WardNo + " " + billboardType.Length + "F X " + billboardType.Width + "F " + result;
            model.BillBoardUniqueKey = id;
            return model;
            

        }
    }
}