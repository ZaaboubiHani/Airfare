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
    public class SeasonServices : BaseServices
    {
        public async Task AddSeason(SeasonModel season)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    context.Seasons.Add(season);
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



        public async Task<List<SeasonModel>> GetAllSeasons()
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var seasons = await context.Seasons.ToListAsync();
                    Error = false;
                    return seasons;
                }
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                Error = true;
                ErrorMessage = e.Message;
                return new List<SeasonModel>();
            }
        }

        public async Task<SeasonModel> GetFirstActiveSeason()
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    return await context.Seasons.FirstOrDefaultAsync(s => s.HasEnded == false);
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


        public async Task RemoveSeason(int seasonId)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.Seasons.FindAsync(seasonId);
                    if (entity == null)
                    {
                        ErrorMessage = "Season not found";
                        Error = true;
                        return;
                    }
                    context.Seasons.Remove(entity);
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

        public async Task UpdateSeason(SeasonModel season)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.Seasons.FindAsync(season.Id);
                    if (entity == null)
                    {
                        ErrorMessage = "Season not found";
                        Error = true;
                        return;
                    }

                    entity.StartDate = season.StartDate;
                    entity.EndDate = season.EndDate;
                    entity.Name = season.Name;
                    entity.HasEnded = season.HasEnded;
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
