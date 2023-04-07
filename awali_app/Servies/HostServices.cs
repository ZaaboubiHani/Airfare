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
    public class HostServices:BaseServices
    {
        public Exception HotelServiceException { get; set; }

        public async Task AddHost(HostModel host)
        {
           
            try
            {
                using (var context = new DataBaseContext())
                {
                    context.Hosts.Add(host);
                    await  context.SaveChangesAsync();
                }
                Error = false;
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                Error = true;
                ErrorMessage = e.Message;
            }
        }

        public async Task AddAllHost(List<HostModel> hosts)
        {
           
            try
            {
                using (var context = new DataBaseContext())
                {
                    context.Hosts.AddRange(hosts);
                    await context.SaveChangesAsync();
                }
                Error = false;
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                Error = true;
                ErrorMessage = e.Message;
            }
        }

        public async Task UpdateHost(HostModel host)
        {
           
            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.Hosts.FindAsync(host.Id);
                    if (entity == null)
                    {
                        Error = true;
                        ErrorMessage = "Host not found";
                        return;
                    }
                    entity.IsPaid = host.IsPaid;
                    entity.PaidPrice = host.PaidPrice;
                    entity.RemainingPrice = host.RemainingPrice;
                    entity.FullPrice = host.FullPrice;
                    entity.ClientId = host.ClientId;
                    entity.CompanyId = host.CompanyId;
                    entity.Discount = host.Discount;
                    entity.HotelRoomId = host.HotelRoomId;
                    entity.SpotId = host.SpotId;
                    await context.SaveChangesAsync();
                }
                Error = false;
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                Error = true;
                ErrorMessage = e.Message;
            }
        }

        public async Task<List<HostModel>> GetAllHosts()
        {
            
            try
            {
                using (var context = new DataBaseContext())
                {
                    var hosts = await context.Hosts
                        .Include("Spot.Group")
                        .Include("Spot.Group.Spots")
                        .Include("Spot")
                        .Include("Client")
                        .Include("HotelRoom")
                        .Include("Client.Phones")
                        .Include("HotelRoom.Room")
                        .Include("HotelRoom.FlightHotel")
                        .Include("HotelRoom.FlightHotel.Hotel")
                        .Include("HotelRoom.FlightHotel.Flight")
                        .Include("Payments")
                        .Include("Company")
                        .Where(h => h.HotelRoom.FlightHotel.Flight.SeasonId == Configuration.CurrentSeason.Id)
                        .ToListAsync().ConfigureAwait(false);
                    Error = false;
                    return hosts;
                }  
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                Error = true;
                ErrorMessage = e.Message;
                return null;
            }
        }

        public async Task<List<HostModel>> GetAllHostsOfFlight(int flightId)
        {

           
            try
            {
                using (var context = new DataBaseContext())
                {
                    var hosts = await context.Hosts
                    .Include("Client")
                    .Include("HotelRoom")
                    .Include("Client.Phones")
                    .Include("HotelRoom.Room")
                    .Include("HotelRoom.FlightHotel")
                    .Include("HotelRoom.FlightHotel.Hotel")
                    .Include("HotelRoom.FlightHotel.Flight")
                    .Include("Payments")
                    .Include("Company")
                    .Where(h=>(h.HotelRoom.FlightHotel.FlightId == flightId && h.HotelRoom.FlightHotel.Flight.SeasonId == Configuration.CurrentSeason.Id))
                    .ToListAsync().ConfigureAwait(false);
                    return hosts;
                }
                Error = false;
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                Error = true;
                ErrorMessage = e.Message;
                return null;
            }
                
            
        }

        public async Task RemoveHost(int hostId)
        {
          
            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.Hosts.FindAsync(hostId);
                    if (entity == null)
                    {
                        Error = true;
                        ErrorMessage = "Host not found";
                        return;
                    }
                    context.Hosts.Remove(entity);
                    await context.SaveChangesAsync();
                }
                Error = false;

            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                Error = true;
                ErrorMessage = e.Message;
            }
        }


    }
}
