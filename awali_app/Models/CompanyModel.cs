using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Airfare.Models
{
    [Table("Companies")]
    public class CompanyModel : BaseModel
    {

        [Key]
        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
                ClearErrors(nameof(Name));

                if (string.IsNullOrWhiteSpace(value))
                {
                    AddError(nameof(Name), "لا يمكن أن يكون الاسم فارغًا");
                }
            }
        }
        private byte[]? _logo;
        public byte[]? Logo
        {
            get => _logo;
            set => SetProperty(ref _logo, value);
        }
        private List<HostModel>? _hosts;
        public List<HostModel>? Hosts
        {
            get => _hosts;
            set => SetProperty(ref _hosts, value);
        }
        private List<CompanyPaymentModel>? _payments;
        public List<CompanyPaymentModel>? Payments
        {
            get => _payments;
            set => SetProperty(ref _payments, value);
        }

        public object Clone()
        {
            return new CompanyModel
            {
                Id = this.Id,
                Name = this.Name,
                Logo = this.Logo?.Clone() as byte[],
                Hosts = this.Hosts,
                Payments = this.Payments,
            };
        }

        public override string ToString()
        {
            return Name;
        }
    
        public static bool operator ==(CompanyModel company1, CompanyModel company2)
        {
            if (company1 is null)
            {
                if (company2 is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            else
            {
                if (company2 is not null)
                {
                    return company1.Equals(company2);
                }

                // Only the left side is null.
                return false;
            }
        }


        public static bool operator !=(CompanyModel company1, CompanyModel company2)
        {
            if ((company1 is null && company2 is not null) || (company1 is not null && company2 is null))
            {
                return true;
            }
            if (company1 is null && company2 is null)
            {
                return false;
            }
            return company1.Id != company2.Id;
        }

        public CompanyModel()
        {
           
        }
    }
}
