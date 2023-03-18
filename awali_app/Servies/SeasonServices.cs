using Airfare.DataContext;
using Airfare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Airfare.Servies
{
    public class SeasonServices:BaseServices
    {
        public async Task AddSeason(SeasonModel season)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        context.Seasons.Add(season);
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

        public async Task<List<SeasonModel>> GetAllSeasons()
        {
            var seasons = new List<SeasonModel>();
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        seasons = context.Seasons.ToList();
                    }
                    
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return seasons;
        }

        public async Task RemoveSeason(SeasonModel season)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {if (!context.Seasons.Local.Contains(season))
                        {
                            context.Seasons.Attach(season);
                        }
                        context.Seasons.Remove(season);
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

        public async Task UpdateSeason(SeasonModel season)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        var foundedSeason= context.Seasons.ToList().Find(s=>s.Id==season.Id);
                        foundedSeason.StartDate = season.StartDate;
                        foundedSeason.EndDate = season.EndDate;
                        foundedSeason.Name = season.Name;
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
