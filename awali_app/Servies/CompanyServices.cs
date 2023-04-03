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
                    var g = (IEnumerable<string>)company.GetErrors(nameof(company.Name));
                    ErrorMessage = g.FirstOrDefault();
                    return;
                }
            }
            catch (Exception e)
            {
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
                    if (entity != null)
                    {
                        context.Companies.Remove(entity);
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

        public async Task UpdateCompany(CompanyModel company)
        {

            try
            {
                using (var context = new DataBaseContext())
                {
                    var foundedCompany = context.Companies.ToList().Find(c => c.Id == company.Id);
                    foundedCompany.Name = company.Name;
                    foundedCompany.Logo = company.Logo;
                    await context.SaveChangesAsync();
                }
                Error = false;
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
            }
        }

        public async Task<CompanyModel> GetCompany(int id)
        {
            var company = new CompanyModel();
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        company = context.Companies.Where(c => c.Id == id).ToList().FirstOrDefault();
                    }
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return company;
        }



        public async Task<List<HostModel>> GetCompanyHostsList(int id)
        {
            var hosts = new List<HostModel>();
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        hosts = context.Companies.Include("Hosts").Include("Hosts.Client").Include("Hosts.HotelRoom.FlightHotel").Include("Hosts.HotelRoom.FlightHotel.Flight").Include("Hosts.HotelRoom.FlightHotel.Hotel").Include("Hosts.HotelRoom.Room").Include("Hosts.HotelRoom").Where(c => c.Id == id).ToList().FirstOrDefault().Hosts.Where(h => h.HotelRoom.FlightHotel.Flight.SeasonId == Configuration.CurrentSeason.Id).ToList();
                    }
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return hosts;
        }


        public async Task<List<CompanyModel>> GetAllCompanies()
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (var context = new DataBaseContext())
                    {
                        var companies =  context.Companies
         .Include(c => c.Payments.Select(p => p.Flight))
         .Include(c => c.Payments.Select(p => p.Company))
         .Include(c => c.Payments.Select(p => p.Payments))
         .ToList();
                        foreach (var company in companies)
                        {
                            company.Payments = company.Payments.Where(p => p.Flight.SeasonId == Configuration.CurrentSeason.Id).ToList();
                        }
                        return companies;
                    }
                }).ConfigureAwait(false);

               
            }
            catch (Exception e)
            {
                Error = true;
                ErrorMessage = e.Message;
                return null;
            }
        }




    }
}
