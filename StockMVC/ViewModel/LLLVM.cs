using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using StockMVC.Models;

namespace StockMVC.ViewModel
{
    [NotMapped]
    public class LLLVM
    {
        public IEnumerable<OrderedItem> orderedItems { get; set; }
        public IEnumerable<OtherDebit> OtherDebits { get; set; }
        public IEnumerable<NewStockLists> NewStockLists { get; set; }
    }
}
