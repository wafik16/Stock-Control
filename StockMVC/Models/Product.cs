using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [StringLength(100)]
        [Display(Name = "Product Name")]
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string ProductName { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }

        [Display(Name = "Cost Price")]
        public decimal CostPrice { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public ProductCategory Category { get; set; }

        public ICollection<OrderedItem> OrderItems { get; set; }
        public ICollection<WholesaleOrderedItem> WholesaleOrderItems { get; set; }

        public ICollection<OtherDebit> OtherDebits { get; set; }
    }

  

    
}
