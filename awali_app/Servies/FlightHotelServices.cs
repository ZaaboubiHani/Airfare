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
    public class FlightHotelServices : BaseServices
    {
        public async Task AddFlightHotel(FlightHotelModel flightHotel)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    context.FlightHotels.Add(flightHotel);
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

        public async Task UpdateFlightHotel(FlightHotelModel flightHotel)
        {

            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.FlightHotels.FindAsync(flightHotel.Id);
                    if (entity == null)
                    {
                        Error = true;
                        ErrorMessage = "Flight Hotel not found";
                        return;
                    }

                    entity.HotelId = flightHotel.HotelId;
                    entity.Feed = flightHotel.Feed;
                    entity.FlightId = flightHotel.FlightId;
                    context.SaveChanges();
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

        public async Task<FlightHotelModel> GetFlightHotel(int flightHotelId)
        {

            try
            {
                using (var context = new DataBaseContext())
                {
                    Error = false;
                    return await context.FlightHotels.FindAsync(flightHotelId);
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

        public async Task<List<FlightHotelModel>> GetAllFlightHotels()
        {

            try
            {
                using (var context = new DataBaseContext())
                {
                    var flightHotels = await context.FlightHotels
                        .Include("Hotel")
                        .Include("Flight")
                        .ToListAsync()
                        .ConfigureAwait(false);
                    Error = false;
                    return flightHotels;
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

        public async Task<List<FlightHotelModel>> GetFlightHotelsOfFlight(int flightId)
        {

            try
            {
                using (var context = new DataBaseContext())
                {
                    var flightHotels = await context.FlightHotels
                        .Include("Hotel")
                        .Include("Flight")
                        .Where(fh => fh.FlightId == flightId).ToListAsync()
                        .ConfigureAwait(false);
                    Error = false;
                    return flightHotels;
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

        public async Task RemoveFlightHotel(int flightHotelId)
        {

            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.FlightHotels.FindAsync(flightHotelId);
                    if (entity == null)
                    {
                        Error = true;
                        ErrorMessage = "Flight Hotel not found";
                        return;
                    }
                    context.FlightHotels.Remove(entity);
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
