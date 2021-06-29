using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMVC.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMVC.ViewModel
{
    [NotMapped]
    public class listViewModel
    {
        public IEnumerable<Order> orders { get; set; }
        public IEnumerable<OrderedItem> orderedItems { get; set; }
    }
}
