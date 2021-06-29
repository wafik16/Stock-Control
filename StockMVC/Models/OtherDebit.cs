using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.Models
{
    public class OtherDebit
    {
        [Key]
        public int DebitId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DebitDate { get; set; }
        [Required]
        public decimal Quantity { get; set; }

        public decimal TotalAmount { get; set; }

        [Required]
        public string UserId { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [Display(Name = "Product Name")]
        public Product product { get; set; }

        public int DebitModeId { get; set; }

        [ForeignKey("DebitModeId")]
        [Display(Name = "Debit/Credit Mode")]
        public DebitMode debitMode { get; set; }

    }
}
