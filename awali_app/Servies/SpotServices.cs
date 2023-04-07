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
    public class SpotServices:BaseServices
    {
        public async Task<SpotModel> AddSpot(SpotModel spot)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var addedSpot = context.Spots.Add(spot);
                    await context.SaveChangesAsync();
                    Error = false;
                    return addedSpot;
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

        public async Task RemoveSpot(int spotId)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.Spots.FindAsync(spotId);
                    if (entity == null)
                    {
                        ErrorMessage = "Spot not found";
                        Error = true;
                        return;
                    }
                    
                    context.Spots.Remove(entity);
                    await context.SaveChangesAsync();
                    
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



        public async Task UpdateSpot(SpotModel spot)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.Spots.FindAsync(spot.Id);

                    if (entity == null)
                    {
                        ErrorMessage = "Spot not found";
                        Error = true;
                        return;
                    }

                    entity.Number = spot.Number;
                    entity.Capacity = spot.Capacity;
                    entity.Color = spot.Color;
                    entity.IsEmpty = spot.IsEmpty;
                    entity.Taken = spot.Taken;
                    entity.GroupId = spot.GroupId;

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
