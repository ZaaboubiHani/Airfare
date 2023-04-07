using Airfare.DataContext;
using Airfare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;



namespace Airfare.Servies
{
    public class CompanyServices : BaseServices
    {
        public async Task AddCompany(CompanyModel company)
        {

            try
            {
                if (!company.HasErrors)
                {
                    using (var context = new DataBaseContext())
                    {
                        context.Companies.Add(company);
                        await context.SaveChangesAsync();
                    }
                    Error = false;
                }
                else
                {
                    Error = true;
                    var errors = company.GetAllErrors();
                    ErrorMessage = errors.FirstOrDefault() ?? "Unknown error";
                    return;
                }
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                Error = true;
                ErrorMessage = e.Message;
            }
        }
        public async Task RemoveCompany(int companyId)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.Companies.FindAsync(companyId);
                    if (entity == null)
                    {
                        Error = true;
                        ErrorMessage = "Client not found";
                        return;
                    }
                   
                    context.Companies.Remove(entity);
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

        public async Task UpdateCompany(CompanyModel company)
        {

            try
            {
                using (var context = new DataBaseContext())
                {
                    var entity = await context.Companies.FindAsync(company.Id);
                    if (entity == null)
                    {
                        Error = true;
                        ErrorMessage = "Client not found";
                        return;
                    }
                    // update properties
                    entity.Name = company.Name;
                    entity.Logo = company.Logo;
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

        public async Task<CompanyModel> GetCompany(int companyId)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var company = await context.Companies.FindAsync(companyId);
                    Error = false;
                    return company;
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



        public async Task<CompanyModel> GetCompanyProperties(int companyId)
        {
           
            try
            {
                using (var context = new DataBaseContext())
                {
                    var hosts = await context.Companies
                        .Where(c => c.Id == companyId)
                        .SelectMany(c => c.Hosts)
                        .Include(h => h.Client)
                        .Include(h => h.HotelRoom.Room)
                        .Include(h => h.HotelRoom.FlightHotel.Flight)
                        .Include(h => h.HotelRoom.FlightHotel.Hotel)
                        .Where(h => h.HotelRoom.FlightHotel.Flight.SeasonId == Configuration.CurrentSeason.Id)
                        .ToListAsync();
                    var payments = await context.Companies
                        .Where(c => c.Id == companyId)
                        .SelectMany(c => c.Payments)
                        .Include(p => p.Company)
                        .Include(p => p.Payments)
                        .Where(p => p.Flight.SeasonId == Configuration.CurrentSeason.Id)
                        .ToListAsync();
                    var company = await context.Companies.FindAsync(companyId);
                    company.Hosts = hosts;
                    company.Payments = payments;
                    Error = false;
                    return company;
                }
                
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message,this);
                Error = true;
                ErrorMessage = e.Message;
            }
            return null;
        }


        public async Task<List<CompanyModel>> GetAllCompanies()
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var companies = await context.Companies
                    .ToListAsync()
                    .ConfigureAwait(false);

                    return companies;
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
    }
}
