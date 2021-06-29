using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.Models
{
    public class DebitMode
    {
        public int DebitModeId { get; set; }
        [Display(Name = "Debit/Credit Mode")]
        public string ModeOfDebit { get; set; }

        public ICollection<OtherDebit> OtherDebits { get; set; }
    }
}
