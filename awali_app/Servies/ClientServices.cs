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
    public class ClientServices:BaseServices
    {
        public async Task AddClient(ClientModel client)
        {
            await Task.Run(() =>
            {
                try
                {
                    if(string.IsNullOrEmpty(client.LastName) || string.IsNullOrWhiteSpace(client.LastName))
                    {
                        Error = true;
                        ErrorMessage = "عليك كتابة اسم المعتمر";
                    }
                    else if (string.IsNullOrEmpty(client.FirstName) || string.IsNullOrWhiteSpace(client.FirstName))
                    {
                        Error = true;
                        ErrorMessage = "عليك كتابة لقب المعتمر";
                    }
                    else
                    {
                        using (var context = new DataBaseContext())
                        {
                            context.Clients.Add(client);
                            context.SaveChanges();
                        }
                        
                        Error = false;
                    }
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
        }

        public async Task UpdateClient(ClientModel client)
        {
            await Task.Run(() =>
            {
                try
                {
                    if (string.IsNullOrEmpty(client.LastName) || string.IsNullOrWhiteSpace(client.LastName))
                    {
                        Error = true;
                        ErrorMessage = "عليك كتابة اسم المعتمر";
                    }
                    else if (string.IsNullOrEmpty(client.FirstName) || string.IsNullOrWhiteSpace(client.FirstName))
                    {
                        Error = true;
                        ErrorMessage = "عليك كتابة لقب المعتمر";
                    }
                    else
                    {
                        using (var context = new DataBaseContext())
                        {
                            var foundedClient = context.Clients.ToList().Find(c => c.Id == client.Id);
                            foundedClient.IsMinor = client.IsMinor;
                            foundedClient.Color = client.Color;
                            foundedClient.FirstName = client.FirstName;
                            foundedClient.LastName = client.LastName;
                            foundedClient.PassportNumber = client.PassportNumber;
                            foundedClient.Description = client.Description;
                            foundedClient.BirthDate = client.BirthDate;
                            foundedClient.Gender = client.Gender;
                            foundedClient.Feed = client.Feed;
                            foundedClient.HealthStatus = client.HealthStatus;
                            foundedClient.IsGuide = client.IsGuide;

                            context.SaveChanges();
                        }
                       
                        Error = false;
                    }
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
        }

       
       

        public async Task RemoveClient(ClientModel client)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        if (!context.Clients.Local.Contains(client))
                        {
                            context.Clients.Attach(client);
                        }
                        context.Clients.Remove(client);
                        context.SaveChanges();

                    }
                    //TODO: Remove all the phones too
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
