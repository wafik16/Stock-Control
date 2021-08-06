using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.ViewModel
{
    public class CustomersPricesVM
    {
        [NotMapped]
        public string ProductName { get; set; }


        public decimal Price { get; set; }
    }
}
