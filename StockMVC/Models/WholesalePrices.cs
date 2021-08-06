using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.Models
{
    public class WholesalePrices
    {
        [Required]
        [Key, Column(Order = 0)]
        public int CustomerId { get; set; }

        [Required]
        [Key, Column(Order = 1)]
        public int ProductId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("CustomerId")]
        public Customer customer { get; set; }

        [ForeignKey("ProductId")]
        public Product product { get; set; }
    }
}
