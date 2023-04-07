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
    public class HotelRoomServices : BaseServices
    {
        public async Task AddHotelRoom(HotelRoomModel hotelRoom)
        {

            try
            {
                using (var context = new DataBaseContext())
                {

                    context.HotelsRooms.Add(hotelRoom);
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

        public async Task UpdateHotelRoom(HotelRoomModel hotelRoom)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.HotelsRooms.FindAsync(hotelRoom.Id);
                    if (entity == null)
                    {
                        Error = true;
                        ErrorMessage = "Hotel room not found";
                        return;
                    }

                    entity.Price = hotelRoom.Price;
                    entity.FlightHotelId = hotelRoom.FlightHotelId;
                    entity.RoomId = hotelRoom.RoomId;

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


        public async Task<HotelRoomModel> GetHotelRoom(int hotelRoomId)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    Error = false;
                    return await context.HotelsRooms.FindAsync(hotelRoomId);
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



        public async Task<List<HotelRoomModel>> GetHotelsRoomsOfFlight(int id)
        {
            List<HotelRoomModel> hotelRooms = new();

            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        hotelRooms = context.HotelsRooms.Include("Room").Include("FlightHotel").Include("FlightHotel.Hotel").Include("FlightHotel.Flight").Where(hr => hr.FlightHotel.FlightId == id && hr.FlightHotel.Flight.SeasonId == Configuration.CurrentSeason.Id).ToList();
                    }

                    Error = false;
                }
                catch (Exception e)
                {
                    LogService.LogError(e.Message, this);
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return hotelRooms;
        }

        public async Task<List<HotelRoomModel>> GetAllHotelsRooms()
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var hotelsRooms = await context.HotelsRooms
                        .Include("Room")
                        .Include("FlightHotel")
                        .Include("FlightHotel.Flight")
                        .Include("FlightHotel.Hotel")
                        .Where(hr => hr.FlightHotel.Flight.SeasonId == Configuration.CurrentSeason.Id)
                        .ToListAsync()
                        .ConfigureAwait(false);
                    Error = false;
                    return hotelsRooms;
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


        public async Task RemoveHotelRoom(int hotelRoomId)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.HotelsRooms.FindAsync(hotelRoomId);
                    if (entity == null)
                    {
                        Error = true;
                        ErrorMessage = "Hotel Room not found";
                        return;
                    }
                    context.HotelsRooms.Remove(entity);
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
