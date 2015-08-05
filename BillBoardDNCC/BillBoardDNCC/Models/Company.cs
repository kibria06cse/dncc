using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BillBoardDNCC.Models
{
    public class Company
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [Display(Name="Trade Licence")]
        public string TradeLicence { get; set; }
        [Display(Name = "Trade Licence Image")]
        public string TradeLicenceImageID { get; set; }
        public string TIN { get; set; }
        [Display(Name = "IIN Image")]
        public string TINImageID { get; set; }
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Website { get; set; }
    }
}