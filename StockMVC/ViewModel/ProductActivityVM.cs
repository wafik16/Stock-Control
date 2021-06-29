using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.ViewModel
{
    [NotMapped]
    public class ProductActivityVM
    {
        public DateTime Date { get; set; }
        public String InvoiceNumber { get; set; }
        public decimal Quantity { get; set; }
    }
}
