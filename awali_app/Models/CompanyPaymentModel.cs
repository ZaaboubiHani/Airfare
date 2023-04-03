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
    public class CompanyPaymentModel:BaseModel
    {
        [Key]
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private float? _amount;
        public float? Amount
        {
            get { return _amount; }
            set { SetProperty(ref _amount, value); }
        }

        private int? _companyId;
        public int? CompanyId
        {
            get { return _companyId; }
            set { SetProperty(ref _companyId, value); }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        private CompanyModel? _company;
        public CompanyModel? Company
        {
            get { return _company; }
            set { SetProperty(ref _company, value); }
        }

        private int? _flightId;
        public int? FlightId
        {
            get { return _flightId; }
            set { SetProperty(ref _flightId, value); }
        }

        private FlightModel? _flight;
        public FlightModel? Flight
        {
            get { return _flight; }
            set { SetProperty(ref _flight, value); }
        }

        private List<PaymentModel>? _payments;
        public List<PaymentModel>? Payments
        {
            get { return _payments; }
            set { SetProperty(ref _payments, value); }
        }

    }
}
