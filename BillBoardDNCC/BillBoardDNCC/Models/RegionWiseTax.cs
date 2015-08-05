using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillBoardDNCC.Models
{
    public class RegionWiseTax
    {
        public int Id { get; set; }
        [Display(Name="Zone")]
        public int ZoneId { get; set; }
        [Display(Name = "Ward")]
        public int WardId { get; set; }
        [Display(Name = "Tax/Square Ft")]
        public decimal TaxPerSqFt { get; set; }
    }
}