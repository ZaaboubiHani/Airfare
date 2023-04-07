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
    public class CompanyContractServices : BaseServices
    {
        

        public async Task AddCompanyContract(CompanyContractModel contract)
        {
            try
            {
                if (!contract.HasErrors)
                {
                    using (var context = new DataBaseContext())
                    {
                        context.CompanyContracts.Add(contract);
                        await context.SaveChangesAsync();
                    }
                    Error = false;
                }
                else
                {
                    Error = true;
                    var errors = contract.GetAllErrors();
                    ErrorMessage = errors.FirstOrDefault() ?? "Unknown error";
                }
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                Error = true;
                ErrorMessage = e.Message;
            }
        }


        public async Task UpdateCompanyContract(CompanyContractModel companyContract)
        {
         
            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.CompanyContracts.FindAsync(companyContract.Id);
                    if (entity == null)
                    {
                        Error = true;
                        ErrorMessage = "Company Contract not found";
                        return;
                    }
                    entity.Price = companyContract.Price;
                    entity.PaidNumber = companyContract.PaidNumber;
                    entity.RoomsNumber = companyContract.RoomsNumber;
                    entity.CompanyId = companyContract.CompanyId;
                    entity.HotelRoomId = companyContract.HotelRoomId;
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

        public async Task<List<CompanyContractModel>> GetAllCompanyContractsOfCompany(int companyId)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var companyContracts = await context.CompanyContracts
                        .Include(cc => cc.HotelRoom)
                        .Include(cc => cc.HotelRoom.Room)
                        .Include(cc => cc.HotelRoom.FlightHotel)
                        .Include(cc => cc.HotelRoom.FlightHotel.Hotel)
                        .Include(cc => cc.HotelRoom.FlightHotel.Flight)
                        .Include(cc => cc.Company)
                        .AsNoTracking()
                        .Where(cc => cc.CompanyId == companyId)
                        .ToListAsync()
                        .ConfigureAwait(false);
                    Error = false;
                    return companyContracts;
                }
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                Error = true;
                ErrorMessage = e.Message;
            }
            return null;
        }

        public async Task RemoveCompanyContract(int contractId)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.CompanyContracts.FindAsync(contractId);
                    if (entity == null)
                    {
                        Error = true;
                        ErrorMessage = "Company Contract not found";
                        return;
                    }
                   
                    context.CompanyContracts.Remove(entity);
                    await context.SaveChangesAsync();
                    Error = false;
                  
                }
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
