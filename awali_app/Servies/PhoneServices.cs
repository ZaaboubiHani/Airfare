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
    public class PhoneServices : BaseServices
    {
        public async Task AddPhone(PhoneModel phone)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    context.Phones.Add(phone);
                    await context.SaveChangesAsync();
                }

                Error = false;
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
                LogService.LogError(e.Message, this);
            }
        }

        public async Task AddPhoneIfNotExist(PhoneModel phone)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var existinPhone = await context.Phones.FindAsync(phone.Id);
                    if (existinPhone == null)
                    {
                        context.Phones.Add(phone);
                        await context.SaveChangesAsync();
                    }
                }

                Error = false;
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
                LogService.LogError(e.Message, this);
            }
        }

        public async Task RemovePhone(PhoneModel phone)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.Phones.FindAsync(phone.Id);
                    if (entity == null)
                    {
                        Error = true;
                        ErrorMessage = "Phone not found";
                        return;
                    }

                    context.Entry(entity).State = EntityState.Deleted;
                    await context.SaveChangesAsync();
                }

                Error = false;
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
                LogService.LogError(e.Message, this);
            }
        }



        public async Task<List<PhoneModel>> GetPhonesOfClient(int clientId)
        {

            try
            {
                using (var context = new DataBaseContext())
                {
                    var phones = await context.Phones
                        .Where(p => p.ClientId == clientId)
                        .ToListAsync()
                        .ConfigureAwait(false);
                    Error = false;
                    return phones;
                }

            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
                LogService.LogError(e.Message, this);
                return null;
            }
        }
    }
}
