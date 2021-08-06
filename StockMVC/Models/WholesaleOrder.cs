using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.Models
{
    public class WholesaleOrder
    {
        [Key]
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        [Display(Name = "Customer Name")]
        public Customer customer { get; set; }

        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Sales date")]
        public DateTime OrderDate { get; set; }

        public decimal Discount { get; set; }

        public decimal DeliveryFee { get; set; }

        public decimal TotalAmount { get; set; }

        [Required]
        public string UserId { get; set; }

        public int PaymentModeId { get; set; }

        [ForeignKey("PaymentModeId")]
        [Display(Name = "Mode of payment")]
        public PaymentMode PaymentMode { get; set; }

        public ICollection<WholesaleOrderedItem> ListOfOrders { get; set; }


        [NotMapped]
        public Product Product { get; set; }

        public decimal TotalWithoutVat { get; set; }

        public decimal TotalVat { get; set; }
    }
}
