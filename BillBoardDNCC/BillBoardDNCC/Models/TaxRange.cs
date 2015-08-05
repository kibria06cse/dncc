using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillBoardDNCC.Models
{
    public class TaxRange
    {
        public int ID { get; set; }
        public string TaxFrom { get; set; }
        public string TaxTo { get; set; }
    }
}