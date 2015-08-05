using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BillBoardDNCC.Models;

namespace BillBoardDNCC.Models
{
    public class SearchViewModel
    {
        [DataType(DataType.Date)]
        public DateTime? From { get; set; }
        [DataType(DataType.Date)]
        public DateTime? To { get; set; }
        public IEnumerable<BillBoard> BillBoards { get; set; }
    }
}