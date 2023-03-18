using Airfare.DataContext;
using Airfare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Servies
{
    public class RoomServices:BaseServices
    {
        public async Task AddRoom(RoomModel room)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        context.Rooms.Add(room);
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

        public async Task UpdateRoom(RoomModel room)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        var foundedRoom = context.Rooms.ToList().Find(r=>r.Id==room.Id);
                        foundedRoom.Capacity = room.Capacity;
                        foundedRoom.Color = room.Color;
                        foundedRoom.Type = room.Type;
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

        public async Task<RoomModel> GetRoom(int id)
        {
            var room = new RoomModel();
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        room = context.Rooms.Where(r => r.Id == id).FirstOrDefault();
                    }
                    
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return room;
        }

        public async Task<List<RoomModel>> GetAllRooms()
        {
            var rooms = new List<RoomModel>();
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        rooms = context.Rooms.ToList();
                    }
                    
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return rooms;
        }

        public async Task RemoveRoom(RoomModel room)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        context.HotelsRooms.RemoveRange(context.HotelsRooms.Where(hr => hr.RoomId == room.Id).ToList());
                        if (!context.Rooms.Local.Contains(room))
                        {
                            context.Rooms.Attach(room);
                        }
                        context.Rooms.Remove(room);
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
