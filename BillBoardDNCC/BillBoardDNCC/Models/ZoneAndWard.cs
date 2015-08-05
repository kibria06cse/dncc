
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillBoardDNCC.Models
{
    public class ZoneAndWard
    {
        public int ID { get; set; }
        [Display(Name = "Total Zone")]
        public int TotalZone { get; set; }
        [Display(Name = "Total Ward")]
        public int TotalWard { get; set; }
    }
}