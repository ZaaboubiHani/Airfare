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
    public class UserServices:BaseServices
    {
        public async Task AddUser(UserModel user)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                    Error = false;
                }
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
                LogService.LogError(e.Message, this);
            }
        }

        public async Task UpdateUser(UserModel user)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.Users.FindAsync(user.Id);
                    if (entity == null)
                    {
                        ErrorMessage = "User not found";
                        Error = true;
                        return;
                    }
                    entity.Name = user.Name;
                    entity.Logo = user.Logo;
                    entity.KeepSigned = user.KeepSigned;
                    await context.SaveChangesAsync();
                    Error = false;
                }
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
                LogService.LogError(e.Message, this);
            }
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var users = await context.Users.ToListAsync().ConfigureAwait(false);
                    Error = false;
                    return users;
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

        public async Task<UserModel?> GetUser()
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var user = await context.Users.FirstOrDefaultAsync(u => u.IsAdmin);
                    Error = false;
                    return user;
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
