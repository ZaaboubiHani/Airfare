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
            try
            {
                if (string.IsNullOrEmpty(client.LastName) || string.IsNullOrWhiteSpace(client.LastName))
                {
                    Error = true;
                    ErrorMessage = "عليك كتابة اسم المعتمر";
                    return;
                }

                if (string.IsNullOrEmpty(client.FirstName) || string.IsNullOrWhiteSpace(client.FirstName))
                {
                    Error = true;
                    ErrorMessage = "عليك كتابة لقب المعتمر";
                    return;
                }
                if (!client.HasErrors)
                {
                    using (var context = new DataBaseContext())
                    {
                        context.Clients.Add(client);
                        await context.SaveChangesAsync();
                    }
                    Error = false;
                }
                else
                {
                    Error = true;
                    var errors = client.GetAllErrors();
                    ErrorMessage = errors.FirstOrDefault() ?? "Unknown error";
                }
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
            }
        }


       

        public async Task UpdateClient(ClientModel client)
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
                        var entity = context.Clients.FirstOrDefault(c => c.Id == client.Id);
                        if (entity == null)
                        {
                            Error = true;
                            ErrorMessage = "Client not found";
                            return;
                        }
                        // update properties
                        entity.IsMinor = client.IsMinor;
                        entity.Color = client.Color;
                        entity.FirstName = client.FirstName;
                        entity.LastName = client.LastName;
                        entity.PassportNumber = client.PassportNumber;
                        entity.Description = client.Description;
                        entity.BirthDate = client.BirthDate;
                        entity.Gender = client.Gender;
                        entity.Feed = client.Feed;
                        entity.HealthStatus = client.HealthStatus;
                        entity.IsGuide = client.IsGuide;

                        await context.SaveChangesAsync();

                    }

                    Error = false;
                }
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
            }
            
        }




        public async Task RemoveClient(int clientId)
        {

            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.Clients.FindAsync(clientId);
                    if (entity != null)
                    {
                        context.Clients.Remove(entity);
                        await context.SaveChangesAsync();
                    }
                }
                Error = false;
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
            }
        }
    }
}
