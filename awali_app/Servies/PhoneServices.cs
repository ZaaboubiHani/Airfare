using Airfare.DataContext;
using Airfare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Servies
{
    public class PhoneServices: BaseServices
    {
        public async Task AddPhone(PhoneModel phone)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        context.Phones.Add(phone);
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

        public async Task AddPhoneIfNotExist(PhoneModel phone)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        var existinPhone = context.Phones.Where(p => p.Id == phone.Id).ToList().FirstOrDefault();
                        if (existinPhone == null)
                            context.Phones.Add(phone);
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

        public async Task RemovePhone(PhoneModel phone)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {if (!context.Phones.Local.Contains(phone))
                        {
                            context.Phones.Attach(phone);
                        }
                        context.Phones.Remove(phone);
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

        public async Task<List<PhoneModel>> GetPhonesOfClient(int clientId)
        {
            var phones = new List<PhoneModel>();
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        phones = context.Phones.Where(p => p.ClientId == clientId).ToList();
                    }
                    
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return phones;
        }
    }
}
