using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillBoardDNCC.Models
{
    public class BillboardAssigned
    {
        public int Id { get; set; }
        [Display(Name="Billboard Unique Key")]
        public int BillboardId { get; set; }
        [Display(Name="Company")]
        public int CompanyId { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date From")]
        public DateTime DateFrom { get; set; }
        [Display(Name = "Date To")]
        [DataType(DataType.Date)]
        public DateTime DateTo { get; set; }
        [Display(Name = "DNCC Ref No")]
        public string DNCCRefNo { get; set; }
        [Display(Name = "DNCC Ref Date")]
        [DataType(DataType.Date)]
        public DateTime DNCCRefDate { get; set; }

        public virtual BillBoard Billboard { get; set; }
        public virtual Company Company { get; set; }
    }
}