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
    public class RoomServices : BaseServices
    {
        public async Task AddRoom(RoomModel room)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    context.Rooms.Add(room);
                    await context.SaveChangesAsync();
                }
                Error = false;
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
                LogService.LogError(e.Message, this);
            }
        }

        public async Task UpdateRoom(RoomModel room)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.Rooms.FindAsync(room.Id);
                    if (entity == null)
                    {
                        ErrorMessage = "Room not found";
                        Error = true;
                        return;
                    }

                    entity.Capacity = room.Capacity;
                    entity.Color = room.Color;
                    entity.Type = room.Type;
                    await context.SaveChangesAsync();
                }
                Error = false;
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
                LogService.LogError(e.Message, this);
            }
        }

        public async Task<RoomModel> GetRoom(int id)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.Rooms.FindAsync(id);
                    if (entity == null)
                    {
                        ErrorMessage = "Room not found";
                        Error = true;
                        return null;
                    }
                    Error = false;
                    return entity;
                }
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
                LogService.LogError(e.Message, this);
                return null;
            }
        }

        public async Task<List<RoomModel>> GetAllRooms()
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var rooms = await context.Rooms.ToListAsync();
                    Error = false;
                    return rooms;
                }
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
                LogService.LogError(e.Message, this);
                return null;
            }
        }

        public async Task RemoveRoom(int roomId)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.Rooms.FindAsync(roomId);
                    if (entity == null)
                    {
                        ErrorMessage = "Room not found";
                        Error = true;
                        return;
                    }

                    context.HotelsRooms.RemoveRange(context.HotelsRooms.Where(hr => hr.RoomId == roomId).ToList());
                    context.Rooms.Remove(entity);
                    await context.SaveChangesAsync();
                }
                Error = false;
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
                LogService.LogError(e.Message, this);
            }
        }
    }

}
