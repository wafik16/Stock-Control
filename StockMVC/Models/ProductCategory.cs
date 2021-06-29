using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.Models
{
    [Table("ProductCategory")]
    public class ProductCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string CategoryName { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string CategoryDescription { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
