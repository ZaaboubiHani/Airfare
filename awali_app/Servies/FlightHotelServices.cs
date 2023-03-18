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
    public class FlightHotelServices:BaseServices
    {
        public async Task AddFlightHotel(FlightHotelModel flightHotel)
        {
            
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {

                        context.FlightHotels.Add(flightHotel);
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

        public async Task UpdateFlightHotel(FlightHotelModel flightHotel)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {

                        var foundedFlightHotel = context.FlightHotels.ToList().Find(fh=>fh.Id==flightHotel.Id);
                        foundedFlightHotel.HotelId = flightHotel.HotelId;
                        foundedFlightHotel.Feed = flightHotel.Feed; 
                        foundedFlightHotel.FlightId=flightHotel.FlightId;
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

        public async Task<FlightHotelModel> GetFlightHotel(int id)
        {
            var flightHotel = new FlightHotelModel();
            await Task.Run( () =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        flightHotel = context.FlightHotels.Include("Hotel").Include("Flight").Where(fh => (fh.Id == id && fh.Flight.SeasonId == Configuration.CurrentSeason.Id)).FirstOrDefault();
                    }
                  
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return flightHotel;
        }

        public async Task<List<FlightHotelModel>> GetAllFlightHotels()
        {
            List<FlightHotelModel> flightHotels = new();
            await Task.Run( () =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        flightHotels = context.FlightHotels.Include("Hotel").Include("Flight").ToList();
                    }
                  
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return flightHotels;
        }

        public async Task<List<FlightHotelModel>> GetFlightHotelsOfFlight(int id)
        {
            List<FlightHotelModel> flightHotels = new();
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        flightHotels = context.FlightHotels.Include("Hotel").Include("Flight").Where(fh => fh.FlightId == id).ToList();
                    }
                   
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return flightHotels;
        }

        public async Task RemoveFlightHotel(FlightHotelModel flightHotel)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        if (!context.FlightHotels.Local.Contains(flightHotel))
                        {
                            context.FlightHotels.Attach(flightHotel);
                        }
                        context.FlightHotels.Remove(flightHotel);
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
