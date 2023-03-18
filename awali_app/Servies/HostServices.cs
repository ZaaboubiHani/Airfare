using Airfare.DataContext;
using Airfare.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Servies
{
    public class HostServices:BaseServices
    {
        public async Task AddHost(HostModel host)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        context.Hosts.Add(host);
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

        public async Task AddAllHost(List<HostModel> hosts)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        context.Hosts.AddRange(hosts);
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

        public async Task UpdateHost(HostModel host)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        var foundedHost = context.Hosts.ToList().Find(h => h.Id == host.Id);
                        foundedHost.IsPaid = host.IsPaid;
                        foundedHost.PaidPrice = host.PaidPrice;
                        foundedHost.RemainingPrice = host.RemainingPrice;
                        foundedHost.FullPrice = host.FullPrice;
                        foundedHost.ClientId = host.ClientId;
                        foundedHost.CompanyId = host.CompanyId;
                        foundedHost.Discount = host.Discount;
                        foundedHost.HotelRoomId = host.HotelRoomId;
                        foundedHost.SpotId = host.SpotId;
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

        public async Task<List<HostModel>> GetAllHosts()
        {
            List<HostModel> hosts = new();
           
            await Task.Run( () =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        hosts = context.Hosts.Include("Spot.Group").Include("Spot.Group.Spots").Include("Spot").Include("Client").Include("HotelRoom").Include("Client.Phones").Include("HotelRoom.Room").Include("HotelRoom.FlightHotel").Include("HotelRoom.FlightHotel.Hotel").Include("HotelRoom.FlightHotel.Flight").Include("Payments").Include("Company").Where(h=>h.HotelRoom.FlightHotel.Flight.SeasonId == Configuration.CurrentSeason.Id).ToList();
                    }  
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return hosts;
        }

        public async Task<List<HostModel>> GetAllHostsOfFlight(int flightId)
        {
            List<HostModel> hosts = new();

            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        hosts = context.Hosts.Include("Client").Include("HotelRoom").Include("Client.Phones").Include("HotelRoom.Room").Include("HotelRoom.FlightHotel").Include("HotelRoom.FlightHotel.Hotel").Include("HotelRoom.FlightHotel.Flight").Include("Payments").Include("Company").Where(h=>(h.HotelRoom.FlightHotel.FlightId == flightId && h.HotelRoom.FlightHotel.Flight.SeasonId == Configuration.CurrentSeason.Id)).ToList();
                    }
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return hosts;
        }

        public async Task RemoveHost(HostModel host)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        if (!context.Hosts.Local.Contains(host))
                        {
                            context.Hosts.Attach(host);
                        }
                       
                        context.Phones.RemoveRange(host.Client.Phones);
                        
                        if (!context.Clients.Local.Contains(host.Client))
                        {
                            context.Clients.Attach(host.Client);
                        }
                        context.Clients.Remove(host.Client);
                       
                        context.Payments.RemoveRange(host.Payments);
                        
                        context.Hosts.Remove(host);
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
