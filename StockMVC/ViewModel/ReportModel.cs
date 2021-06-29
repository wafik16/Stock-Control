using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.ViewModel
{
    [NotMapped]
    public class ReportModel
    {
        public int OrderId { get; set; }

        public string InvoiceNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal Discount { get; set; }

        public decimal DeliveryFee { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public decimal Total { get; set; }
        public decimal TotalWithoutVat { get; set; }

        public decimal TotalVat { get; set; }

        public decimal TotalAmount { get; set; }

        

    }
}
