using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Airfare.Models
{
    [Table("Companies")]
    public class CompanyModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[]? Logo { get; set; }
        public List<HostModel>? Hosts { get; set; }
        public List<CompanyPaymentModel>? Payments { get; set; }

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
