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
    public class PaymentModel:ICloneable
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

        public object Clone()
        {
            return new PaymentModel
            {
                Id = this.Id,
                Amount = this.Amount,
                Date = this.Date,
                PaidByCompany = this.PaidByCompany,
                CompanyPayment = this.CompanyPayment?.Clone() as CompanyPaymentModel,
                CompanyPaymentId = this.CompanyPaymentId,
                HostId = this.HostId,
                Host = this.Host?.Clone() as HostModel
            };
        }
    }
}
