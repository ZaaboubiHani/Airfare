using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Models
{
    [Table("CompanyPayments")]
    public class CompanyPaymentModel
    {
        [Key]
        public int Id { get; set; }
        public float? Amount { get; set; }
        public int? CompanyId { get; set; }
        public DateTime Date { get; set; }
        public CompanyModel? Company { get; set; }
        public int? FlightId { get; set; }
        public FlightModel? Flight { get; set; }
        public List<PaymentModel>? Payments { get; set; }

    }
}
