using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BillBoardDNCC.Models
{
    public class BillBoard
    {
        public int ID { get; set; }
        [Index(IsUnique=true)]
        [MaxLength(450)]
        [Display(Name = "Billboard Unique key")]       
        public string BillBoardUniqueKey { get; set; }
        [Display(Name = "Bill Board Type")]
        public int BillBoardTypeId { get; set; }
        //[Display(Name = "Bill Board Size")]
        //public int BillBoardSizeId { get; set; }
        //[Display(Name = "Place of Permission")]
        //public string PlaceOfPermission { get; set; }        
        //public double Latitude { get; set; }
        //public double Langitude { get; set; }  
        [Required(ErrorMessage="This is required")]
        public int ZoneWardAreaId { get; set; }
        public string GeoLocation { get; set; }
        //public int ZoneId { get; set; }
        //public int WardId { get; set; }
        public int BillBoardLastNo { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }

        public virtual BillBoardType BillBoardType { get; set; }
        public virtual ZoneWardArea ZoneWardArea { get; set; }
    }
}