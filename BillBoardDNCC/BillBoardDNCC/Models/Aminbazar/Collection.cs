using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillBoardDNCC.Models
{
    public class Collection
    {
        public int Id { get; set; }
        [Display(Name="Licence Plate No")]
        public string LicencePlateNo { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }
        [Display(Name = "Zone No")]
        public int ZoneNo { get; set; }
        [Display(Name = "Ward No")]
        public int WardNo { get; set; }
        [Display(Name = "Weight")]
        public double TotalWeight { get; set; }
        //public string mdbFileID { get; set; }
    }
}