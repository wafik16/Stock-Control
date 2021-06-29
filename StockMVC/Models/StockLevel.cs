using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.Models
{
    public class StockLevel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        public decimal Balance { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
