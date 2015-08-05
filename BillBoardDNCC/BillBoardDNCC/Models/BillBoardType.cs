using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillBoardDNCC.Models
{
    public class BillBoardType
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ShortCode { get; set; }
        public string Description { get; set; }
        [Required]
        public double Height { get; set; }
        [Required]
        public double Width { get; set; }
        [Required]
        public double Length { get; set; }
    }
}