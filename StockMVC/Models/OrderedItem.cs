using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.Models
{
    [Table("OrderedItem")]
    public class OrderedItem
    {
        [Required]
        [Key, Column(Order = 0)]
        public int OrderId { get; set; }

        [Required]
        [Key, Column(Order = 1)]
        public int ProductId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        public decimal Total { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
