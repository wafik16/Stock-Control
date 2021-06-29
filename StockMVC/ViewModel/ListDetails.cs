using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.ViewModel
{
    [NotMapped]
    public class ListDetails
    {
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }


        public string InvoiceNumber { get; set; }

        public decimal CostPrice { get; set; }
        public decimal Quantity { get; set; }

        public decimal Total { get; set; }

        public DateTime Date { get; set; }
    }
}
