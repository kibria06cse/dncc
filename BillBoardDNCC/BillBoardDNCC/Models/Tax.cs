using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillBoardDNCC.Models
{
    public class Tax
    {
        public int Id { get; set; }
        public int BillboardId { get; set; }
        public int CompanyId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Display(Name="Chalan Image")]
        public string ChalanImageId { get; set; }
        [Display(Name = "Total Tax")]
        public decimal TotalTax { get; set; }
        [Display(Name = "Paid Tax")]
        public decimal PaidTaxAmount { get; set; }
        [Display(Name = "Bank Name")]
        public string PaidTaxBankName { get; set; }
        [Display(Name = "Purchase Order")]
        public string PurchaseOrder { get; set; }
        [Display(Name = "Bank Address")]
        public string BankAddress { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Paid Tax Date")]
        public DateTime PaidTaxDate { get; set; }
        [Display(Name = "Due Tax")]
        public decimal DueTaxAmount { get; set; }

        public virtual BillBoard Billboard { get; set; }
        public virtual Company Company { get; set; }
    }
}