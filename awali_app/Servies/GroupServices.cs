using Airfare.DataContext;
using Airfare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Servies
{
    public class GroupServices : BaseServices
    {
        public async Task AddGroup(GroupModel group)
        {
            
            try
            {
                using (var context = new DataBaseContext())
                {
                    context.Groups.Add(group);
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

        public async Task RemoveGroup(int groupId)
        {
           
            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.Groups.FindAsync(groupId);
                    if (entity == null)
                    {
                        Error = true;
                        ErrorMessage = "Group not found";
                        return;
                    }
                    context.Groups.Remove(entity);
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
