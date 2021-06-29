using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.ViewModel
{
    [NotMapped]
    public class DetailsViewModel
    {
     
        public string ProductName { get; set; }

    
        public decimal Price { get; set; }


        public decimal Quantity { get; set; }

        public decimal Total { get; set; }

    }
}
