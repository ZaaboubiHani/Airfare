using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Models
{
    [Table("Payments")]
    public class PaymentModel
    {
        [Key]
        public int Id { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public bool? PaidByCompany { get; set; }
        public CompanyPaymentModel? CompanyPayment { get; set; }
        public int? CompanyPaymentId { get; set; }
        public int HostId { get; set; }
        public HostModel Host { get; set; }
    }
}
