using Airfare.DataContext;
using Airfare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Servies
{
    public class SpotServices:BaseServices
    {
        public async Task<SpotModel> AddSpot(SpotModel spot)
        {
            SpotModel _spot = new SpotModel();
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        _spot = context.Spots.Add(spot);
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
            return _spot;
        }
        
        public async Task RemoveSpot(SpotModel spot)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        if (!context.Spots.Local.Contains(spot))
                        {
                            context.Spots.Attach(spot);
                        }
                        context.Spots.Remove(spot);
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

        public async Task UpdateSpot(SpotModel spot)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        var foundedSpot = context.Spots.ToList().Find(s => s.Id == spot.Id);
                        foundedSpot.Number = spot.Number;
                        foundedSpot.Capacity = spot.Capacity;
                        foundedSpot.Color = spot.Color;
                        foundedSpot.IsEmpty = spot.IsEmpty;
                        foundedSpot.Taken = spot.Taken;
                        foundedSpot.GroupId = spot.GroupId;

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
