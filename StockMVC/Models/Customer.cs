using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.Models
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [StringLength(100)]
        [Required]
        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Address { get; set; }

        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }

        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [RegularExpression("^[a-z]{4}|[0-9]{11,}", ErrorMessage = "Not a valid Mobile number")]
        [Column(TypeName = "varchar(11)")]
        public string Mobile { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Registration")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RegisterDate { get; set; }

    }
}
