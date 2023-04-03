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
        public async Task AddCompanyPayment(CompanyPaymentModel payment)
        {
            try
            {
                if (!payment.HasErrors)
                {
                    using (var context = new DataBaseContext())
                    {
                        context.CompanyPayments.Add(payment);
                        await context.SaveChangesAsync();
                    }
                    Error = false;
                }
                else
                {
                    Error = true;
                    var errors = payment.GetAllErrors();
                    ErrorMessage = errors.FirstOrDefault() ?? "Unknown error";
                }
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
            }
        }

        public async Task RemoveCompanyPayment(int paymentId)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.CompanyPayments.FindAsync(paymentId);
                    if (entity != null)
                    {
                        context.CompanyPayments.Remove(entity);
                        await context.SaveChangesAsync();
                    }
                }
                Error = false;
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
            }
        }
    }
}
