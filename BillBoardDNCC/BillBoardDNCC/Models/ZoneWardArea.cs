using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillBoardDNCC.Models
{
    public class ZoneWardArea
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="Zone No")]
        public int ZoneNo { get; set; }
        [Required]
        [Display(Name = "Ward No")]
        public int WardNo { get; set; }
        [Required]
        public string Area { get; set; }
    }
}