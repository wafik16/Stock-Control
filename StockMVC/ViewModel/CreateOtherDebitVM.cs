using StockMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.ViewModel
{
    [NotMapped]
    public class CreateOtherDebitVM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Date of Debit")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DebitDate { get; set; }
        [Display(Name = "Product Name")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [Display(Name = "Product Name")]
        public Product product { get; set; }

        [Required(ErrorMessage = "Please provide the Quantity")]
        public decimal Quantity { get; set; }
        [Display(Name = "Debit/Credit Mode")]
        public int DebitModeId { get; set; }

        [ForeignKey("DebitModeId")]
        [Display(Name = "Debit/Credit Mode")]
        public DebitMode debitMode { get; set; }

  
        public string RoleName { get; set; }
    }
}
