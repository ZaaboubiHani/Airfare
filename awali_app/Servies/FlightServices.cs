using Airfare.DataContext;
using Airfare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Servies
{
    public class FlightServices:BaseServices
    {
        public async Task AddFlight(FlightModel flight)
        {
            await Task.Run(() =>
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
                            context.SaveChanges();
                        }
                        Error = false;
                    }
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
        }

        public async Task UpdateFlight(FlightModel flight)
        {
            await Task.Run(() =>
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
                            var foundedFlight = context.Flights.ToList().Find(f => f.Id == flight.Id);
                            foundedFlight.ReturnItinerary = flight.ReturnItinerary;
                            foundedFlight.DepartItinerary = flight.DepartItinerary;
                            foundedFlight.ReturntDate = flight.ReturntDate;
                            foundedFlight.DepartDate = flight.DepartDate;
                            foundedFlight.Category = flight.Category;
                            foundedFlight.Capacity = flight.Capacity;
                            foundedFlight.ReturnName = flight.ReturnName;
                            foundedFlight.DepartName = flight.DepartName;
                            context.SaveChanges();
                        }
                        Error = false;
                    }
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
        }

        public async Task<FlightModel> GetFlight(int id)
        {
            var flight = new FlightModel();
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        flight = context.Flights.Where(f => (f.Id == id && f.SeasonId == Configuration.CurrentSeason.Id)).FirstOrDefault();
                    }
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return flight;
        }

        public async Task<List<FlightModel>> GetAllFlightsOfSeason(int id)
        {
            var flights = new List<FlightModel>();
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        flights = context.Flights.Where(f => f.SeasonId == id).ToList();
                    }
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return flights;
        }

        public async Task<List<FlightModel>> GetAllFlightsOfSeasonWithoutHotels(int id)
        {
            var flights = new List<FlightModel>();
            await Task.Run(async () =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        flights = context.Flights.Where(f => f.SeasonId == id).ToList();
                        FlightHotelServices flightHotelServices = new();
                        var flightswithhotels = (await flightHotelServices.GetAllFlightHotels()).Select(fh => fh.Flight).ToList().GroupBy(f => f.Id).Select(obj => obj.ToList().FirstOrDefault()).Where(f=>f.SeasonId == Configuration.CurrentSeason.Id).ToList();
                        if(flightswithhotels.Count > 0)
                            flights.RemoveAll(f1 => flightswithhotels.Any(f2 => f2.Id == f1.Id));
                    }
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return flights;
        }

        public async Task RemoveFlight(FlightModel flight)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {   
                        if (!context.Flights.Local.Contains(flight))
                        {
                            context.Flights.Attach(flight);
                        }
                        context.Clients.RemoveRange( context.Hosts.Where(h => h.HotelRoom.FlightHotel.FlightId == flight.Id).Select(h => h.Client).ToList() );

                        context.Flights.Remove(flight);
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
