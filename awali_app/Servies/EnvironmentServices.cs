using Airfare.DataContext;
using Airfare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Servies
{
    public class EnvironmentServices:BaseServices
    {
        public async Task AddEnvironment(EnvironmentModel environment)
        {
            
            try
            {
                using (var context = new DataBaseContext())
                {
                    context.Environments.Add(environment);
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
        public async Task RemoveEnvironment(int environmentId)
        {
           
            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.Environments.FindAsync(environmentId);
                    if (entity == null)
                    {
                        Error = true;
                        ErrorMessage = "Environment not found";
                        return;
                    }
                    context.Environments.Remove(entity);
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
        public async Task UpdateEnvironment(EnvironmentModel environment)
        {
           
            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await  context.Environments.FindAsync(environment.Id);
                    if (entity == null)
                    {
                        Error = true;
                        ErrorMessage = "Environment not found";
                        return;
                    }
                    entity.ClientContractContent = environment.ClientContractContent;
                    entity.FooterSource = environment.FooterSource;
                    entity.HeaderSource = environment.HeaderSource;
                   
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

        public async Task<EnvironmentModel> GetEnvironment()
        {
            
            try
            {
                using (var context = new DataBaseContext())
                {
                    Error = false;
                    return context.Environments.FirstOrDefault();
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
    }
}
