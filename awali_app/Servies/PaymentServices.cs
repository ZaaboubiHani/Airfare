using Airfare.DataContext;
using Airfare.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Servies
{
    public class PaymentServices : BaseServices
    {

        public async Task AddPayment(PaymentModel payment)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    context.Payments.Add(payment);
                    await context.SaveChangesAsync();
                }
                Error = false;
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
                LogService.LogError(e.Message, this);
            }
        }

        public async Task AddPaymentIfNotExist(PaymentModel payment)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var existingPayment = await context.Payments.FindAsync(payment.Id);
                    if (existingPayment == null)
                    {
                        context.Payments.Add(payment);
                        await context.SaveChangesAsync();
                    }
                }
                Error = false;
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
                LogService.LogError(e.Message, this);
            }
        }

        public async Task<List<PaymentModel>> GetPaymentsOfHost(int hostId)
        {

            try
            {
                using (var context = new DataBaseContext())
                {
                    var payments = await context.Payments
                        .Where(p => p.HostId == hostId && p.Host.HotelRoom.FlightHotel.Flight.SeasonId == Configuration.CurrentSeason.Id)
                        .ToListAsync().ConfigureAwait(false);
                    Error = false;
                    return payments;
                }

            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
                LogService.LogError(e.Message, this);
                return null;
            }
        }

        public async Task RemovePayment(PaymentModel payment)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.Payments.FindAsync(payment.Id);
                    if (entity == null)
                    {
                        Error = true;
                        ErrorMessage = "Payment not found";
                        return;
                    }
                    context.Payments.Remove(entity);
                    await context.SaveChangesAsync();
                }
                Error = false;

            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
                LogService.LogError(e.Message, this);
            }
        }

        public async Task<List<PaymentModel>> GetAllPaymentsOfSeason(int seasonId)
        {

            try
            {
                using (var context = new DataBaseContext())
                {
                    var payments = await context.Payments
                        .Include("Host")
                        .Include("Host.HotelRoom")
                        .Include("Host.HotelRoom.FlightHotel")
                        .Include("Host.HotelRoom.FlightHotel.Flight")
                        .Include("Host.HotelRoom.FlightHotel.Hotel")
                        .Where(p => p.Host.HotelRoom.FlightHotel.Flight.SeasonId == seasonId)
                        .ToListAsync().ConfigureAwait(false);
                    Error = false;
                    return payments;
                }

            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
                LogService.LogError(e.Message, this);
                return null;
            }
        }



    }
}
