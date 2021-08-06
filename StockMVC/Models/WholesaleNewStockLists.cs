using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.Models
{
    public class WholesaleNewStockLists
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        [Display(Name = "Entry Date")]
        public DateTime NewOrderDate { get; set; }
        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        public string UserId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
