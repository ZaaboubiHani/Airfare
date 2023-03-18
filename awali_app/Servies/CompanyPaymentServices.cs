using Airfare.DataContext;
using Airfare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Servies
{
    public class CompanyPaymentServices : BaseServices
    {
        public async Task AddCompanyPayment(CompanyPaymentModel companyPayment)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        context.CompanyPayments.Add(companyPayment);
                        context.SaveChanges();
                    }

                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
        }

        public async Task RemoveCompanyPayment(CompanyPaymentModel companyPayment)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        if (!context.CompanyPayments.Local.Contains(companyPayment))
                        {
                            context.CompanyPayments.Attach(companyPayment);
                        }
                        context.CompanyPayments.Remove(companyPayment);
                        context.SaveChanges();
                    }

                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
        }
    }
}
