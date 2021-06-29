using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.ViewModel
{
    [NotMapped]
    public class ActivityVM
    {
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Start Date")]
        public DateTime fromDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime toDate { get; set; }
    }
}
