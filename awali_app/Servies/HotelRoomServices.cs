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
    public class HotelRoomServices:BaseServices
    {
        public async Task AddHotelRoom(HotelRoomModel hotelRoom)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                    
                        context.HotelsRooms.Add(hotelRoom);
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

        public async Task UpdateHotelRoom(HotelRoomModel hotelRoom)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        var foundedHotelRoom = context.HotelsRooms.ToList().Find(hr=>hr.Id == hotelRoom.Id);
                        foundedHotelRoom.Price = hotelRoom.Price;
                        foundedHotelRoom.FlightHotelId = hotelRoom.FlightHotelId;
                        foundedHotelRoom.RoomId = hotelRoom.RoomId;
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

        public async Task<HotelRoomModel> GetHotelRoom(int id)
        {
            var hotelRoom = new HotelRoomModel();
           
            await Task.Run( () =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        hotelRoom = context.HotelsRooms.Include("Room").Include("FlightHotel").Include("FlightHotel.Flight").Include("FlightHotel.Hotel").Where(hr => (hr.Id == id && hr.FlightHotel.Flight.SeasonId == Configuration.CurrentSeason.Id)).FirstOrDefault();
                    }
                 
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return hotelRoom;
        }


        public async Task<List<HotelRoomModel>> GetHotelsRoomsOfFlight(int id)
        {
            List<HotelRoomModel> hotelRooms = new();
          
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        hotelRooms = context.HotelsRooms.Include("Room").Include("FlightHotel").Include("FlightHotel.Hotel").Include("FlightHotel.Flight").Where(hr=>hr.FlightHotel.FlightId==id && hr.FlightHotel.Flight.SeasonId == Configuration.CurrentSeason.Id).ToList();
                    }

                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return hotelRooms;
        }

        public async Task<List<HotelRoomModel>> GetAllHotelsRooms()
        {
            var hotelsRooms = new List<HotelRoomModel>();
           
            await Task.Run( () =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        hotelsRooms = context.HotelsRooms.Include("Room").Include("FlightHotel").Include("FlightHotel.Flight").Include("FlightHotel.Hotel").Where(hr=>hr.FlightHotel.Flight.SeasonId == Configuration.CurrentSeason.Id).ToList();
                    }
                
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return hotelsRooms;
        }

        public async Task RemoveHotelRoom(HotelRoomModel hotelRoom)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {if (!context.HotelsRooms.Local.Contains(hotelRoom))
                        {
                            context.HotelsRooms.Attach(hotelRoom);
                        }
                        context.HotelsRooms.Remove(hotelRoom);
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
