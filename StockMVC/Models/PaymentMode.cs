using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.Models
{
    public class PaymentMode
    {
        public int PaymentModeId { get; set; }
        public string ModeOfPayment { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<WholesaleOrder> WholesaleOrders { get; set; }
    }

}
