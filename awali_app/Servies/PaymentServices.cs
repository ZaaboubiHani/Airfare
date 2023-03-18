using Airfare.DataContext;
using Airfare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Servies
{
    public class PaymentServices : BaseServices
    {
        
        public async Task AddPayment(PaymentModel payment)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        context.Payments.Add(payment);
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

        public async Task AddPaymentIfNotExist(PaymentModel payment)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {

                        var existingPayment = context.Payments.Where(p => p.Id == payment.Id).ToList().FirstOrDefault();
                        if (existingPayment == null)
                            context.Payments.Add(payment);
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

        public async Task<List<PaymentModel>> GetPaymentsOfHost(int hostId)
        {
            var payments = new List<PaymentModel>();
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        payments = context.Payments.Where(p => p.HostId == hostId && p.Host.HotelRoom.FlightHotel.Flight.SeasonId == Configuration.CurrentSeason.Id).ToList();
                    }
                    
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return payments;
        }

        public async Task RemovePayment(PaymentModel payment)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        if (!context.Payments.Local.Contains(payment))
                        {
                            context.Payments.Attach(payment);
                        }
                        context.Payments.Remove(payment);
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

        public async Task<List<PaymentModel>> GetAllPaymentsOfSeason(int seasonId)
        {
            var payments = new List<PaymentModel>();
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        payments = context.Payments.Include("Host").Include("Host.HotelRoom").Include("Host.HotelRoom.FlightHotel").Include("Host.HotelRoom.FlightHotel.Flight").Include("Host.HotelRoom.FlightHotel.Hotel").Where(p => p.Host.HotelRoom.FlightHotel.Flight.SeasonId == seasonId).ToList();
                    }

                    Error = false;

                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return payments;
        }

    }
}
