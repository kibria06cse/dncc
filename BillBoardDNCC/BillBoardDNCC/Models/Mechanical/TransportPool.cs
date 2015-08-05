using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillBoardDNCC.Models
{
    public class TransportPool
    {
        public int Id { get; set;}
        public int VehicleTypeId { get; set; }
        public string RegistrationNo { get; set; }
        public string EngineNo { get; set; }
        public string ChasisNo { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string Country { get; set; }
        public int YearOfPurchase { get; set; }
        public decimal ProcurementCost { get; set; }
        public decimal Fund { get; set; }

        public virtual VehicleType VehicleType { get; set; }
    }
}