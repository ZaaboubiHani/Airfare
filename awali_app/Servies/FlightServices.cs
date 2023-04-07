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
    public class FlightServices:BaseServices
    {
        public async Task AddFlight(FlightModel flight)
        {
            
            try
            {
                if (flight.ReturntDate.Year < 1753 || flight.DepartDate.Year < 1753)
                {
                        Error = true;
                        ErrorMessage = "عليك كتابة تاريخ الرحلة";
                }
                else
                {
                    using (var context = new DataBaseContext())
                    {
                        context.Flights.Add(flight);
                        await context.SaveChangesAsync();
                    }
                    Error = false;
                }
            }
            catch (Exception e)
            {

                LogService.LogError(e.Message, this);
                Error = true;
                ErrorMessage = e.Message;
            
            }
        }

        public async Task UpdateFlight(FlightModel flight)
        {
        
            try
            {
                if (flight.ReturntDate.Year < 1753 || flight.DepartDate.Year < 1753)
                {
                    Error = true;
                    ErrorMessage = "عليك كتابة تاريخ الرحلة";
                }
                else
                {
                    using (var context = new DataBaseContext())
                    {
                        var entity = await context.Flights.FindAsync(flight.Id);
                        if(entity == null)
                        {
                            Error = true;
                            ErrorMessage = "Flight not found";
                            return;
                        }
                        entity.ReturnItinerary = flight.ReturnItinerary;
                        entity.DepartItinerary = flight.DepartItinerary;
                        entity.ReturntDate = flight.ReturntDate;
                        entity.DepartDate = flight.DepartDate;
                        entity.Category = flight.Category;
                        entity.Capacity = flight.Capacity;
                        entity.ReturnName = flight.ReturnName;
                        entity.DepartName = flight.DepartName;

                        await context.SaveChangesAsync();
                    }
                    Error = false;
                }
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                Error = true;
                ErrorMessage = e.Message;
            }
        }

        public async Task<FlightModel> GetFlight(int flightId)
        {
            
            try
            {
                using (var context = new DataBaseContext())
                {
                    Error = false;
                    return await context.Flights.FindAsync(flightId);
                }
            }
            catch (Exception e)
            {

                LogService.LogError(e.Message, this);
                Error = true;
                ErrorMessage = e.Message;
            }
            return null;
        }

        public async Task<List<FlightModel>> GetAllFlightsOfSeason(int id)
        {
           
           
            try
            {
                using (var context = new DataBaseContext())
                {
                    var flights = await context.Flights.Where(f=>f.SeasonId == Configuration.CurrentSeason.Id).ToListAsync()
                    .ConfigureAwait(false);
                    Error = false;
                    return flights;
                }
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                Error = true;
                ErrorMessage = e.Message;
            }
            return null;
        }
        public async Task<List<FlightModel>> GetAllFlightsOfSeasonWithoutHotels(int id)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var flights = await context.Flights.Where(f => f.SeasonId == id).ToListAsync();
                    FlightHotelServices flightHotelServices = new();
                    var flightswithhotels = (await flightHotelServices.GetAllFlightHotels()).Select(fh => fh.Flight).ToList().GroupBy(f => f.Id).Select(obj => obj.ToList().FirstOrDefault()).Where(f => f.SeasonId == Configuration.CurrentSeason.Id).ToList();
                    if (flightswithhotels.Count > 0)
                        flights.RemoveAll(f1 => flightswithhotels.Any(f2 => f2.Id == f1.Id));
                    return flights;
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


       

        public async Task RemoveFlight(int flightId)
        {
           
            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.Flights.FindAsync(flightId);
                    if(entity == null)
                    {
                        Error = true;
                        ErrorMessage = "Flight no found";
                        return;
                    }
           
                    context.Flights.Remove(entity);
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
