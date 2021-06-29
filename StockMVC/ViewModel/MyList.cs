using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.ViewModel
{
    [NotMapped]
    public class MyList
    {
        public decimal UnitPrice { get; set; }
        public decimal StockBalance { get; set; }
    }
}
