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
    public class HotelServices : BaseServices
    {
        public async Task AddHotel(HotelModel hotel)
        {

            try
            {
                using (var context = new DataBaseContext())
                {
                    context.Hotels.Add(hotel);
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

        public async Task UpdateHotel(HotelModel hotel)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        var entity = context.Hotels.Find(hotel.Id);
                        if (entity == null)
                        {
                            Error = true;
                            ErrorMessage = "Hotel not found";
                            return;
                        }

                        entity.Distance = hotel.Distance;
                        entity.Name = hotel.Name;
                        entity.Address = hotel.Address;
                        entity.RoomsNumber = hotel.RoomsNumber;
                        entity.Rate = hotel.Rate;
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
            });
        }


        public async Task<HotelModel> GetHotel(int hotelId)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    Error = false;
                    return await context.Hotels.FindAsync(hotelId);
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


        public async Task<List<HotelModel>> GetAllHotels()
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var hotels = await context.Hotels.ToListAsync().ConfigureAwait(false);
                    return hotels;
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


        public async Task RemoveHotel(int hotelId)
        {

            try
            {
                using (var context = new DataBaseContext())
                {

                    var entity = await context.Hotels.FindAsync(hotelId);
                    if (entity == null)
                    {
                        Error = true;
                        ErrorMessage = "Hotel not found";
                        return;
                    }
                    context.Hotels.Remove(entity);
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
