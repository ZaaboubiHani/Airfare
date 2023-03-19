using Airfare.DataContext;
using Airfare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Servies
{
    public class UserServices:BaseServices
    {
        public async Task AddUser(UserModel user)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        context.Users.Add(user);
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
       
        public async Task UpdateUser(UserModel user)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        var foundedUser = context.Users.ToList().Find(u => u.Id == user.Id);
                        foundedUser.Name = user.Name;
                        foundedUser.Logo = user.Logo;
                        foundedUser.KeepSigned = user.KeepSigned;

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

     
        public async Task<List<UserModel>> GetAllUsers()
        {
            var users = new List<UserModel>();
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        users = context.Users.ToList();
                    }
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return users;
        }

        public async Task<UserModel?> GetUser()
        {
            UserModel user = null;
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        user = context.Users.Where(u=>u.IsAdmin).FirstOrDefault();
                    }
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return user;
        }
    }
}
