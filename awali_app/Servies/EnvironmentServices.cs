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
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        context.Environments.Add(environment);
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
        public async Task RemoveEnvironment(EnvironmentModel environment)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        if (!context.Environments.Local.Contains(environment))
                        {
                            context.Environments.Attach(environment);
                        }
                        context.Environments.Remove(environment);
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
        public async Task UpdateEnvironment(EnvironmentModel environment)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        var foundedEnvironment = context.Environments.ToList().Find(e => e.Id == environment.Id);
                        foundedEnvironment.KeepSigned = environment.KeepSigned;
                        foundedEnvironment.ClientContractContent = environment.ClientContractContent;
                        foundedEnvironment.FooterSource = environment.FooterSource;
                        foundedEnvironment.HeaderSource = environment.HeaderSource;
                        foundedEnvironment.UserName = environment.UserName;
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

        public async Task<EnvironmentModel> GetEnvironment()
        {
            var environment = new EnvironmentModel();
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        environment = context.Environments.FirstOrDefault();
                    }
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return environment;
        }
    }
}
