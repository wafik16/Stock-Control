using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.Models
{
    [Table("Staff")]
    public class staff
    {
        [Key]
        public int StaffID { get; set; }

        [StringLength(100)]
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string StaffName { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Address { get; set; }

        [Column(TypeName = "varchar(50)")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(11)")]
        [RegularExpression("^[a-z]{4}|[0-9]{11,}", ErrorMessage = "Not a valid Mobile number")]
        public string Mobile { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RegisterDate { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string PhotoPath { get; set; }

    }
}
