using Airfare.DataContext;
using Airfare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Servies
{
    public class HotelServices:BaseServices
    {
        public async Task AddHotel(HotelModel hotel)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        context.Hotels.Add(hotel);
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

        public async Task UpdateHotel(HotelModel hotel)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        var foundedHotel = context.Hotels.ToList().Find(h=> h.Id == hotel.Id);
                        foundedHotel.Distance = hotel.Distance;
                        foundedHotel.Name = hotel.Name;
                        foundedHotel.Address = hotel.Address;
                        foundedHotel.RoomsNumber = hotel.RoomsNumber;
                        foundedHotel.Rate = hotel.Rate;
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

        public async Task<HotelModel> GetHotel(int id)
        {
            var hotel = new HotelModel();
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        hotel = context.Hotels.Where(h => h.Id == id).FirstOrDefault();
                    }
                    
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return hotel;
        }

        public async Task<List<HotelModel>> GetAllHotels()
        {
            var hotels = new List<HotelModel>();
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        hotels = context.Hotels.ToList();
                    }
                    
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return hotels;
        }

        public async Task RemoveHotel(HotelModel hotel)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                      
                        context.FlightHotels.RemoveRange(context.FlightHotels.Where((fh) => fh.HotelId == hotel.Id).ToList());
                        context.HotelsRooms.RemoveRange(context.HotelsRooms.Where((hr) => hr.FlightHotel.HotelId == hotel.Id).ToList());
                        if (!context.Hotels.Local.Contains(hotel))
                        {
                            context.Hotels.Attach(hotel);
                        }
                        context.Hotels.Remove(hotel);
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
