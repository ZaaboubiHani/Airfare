using Airfare.DataContext;
using Airfare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Airfare.Servies
{
    public class CompanyServices:BaseServices
    {
        public async Task AddCompany(CompanyModel company)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        context.Companies.Add(company);
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
        public async Task RemoveCompany(CompanyModel company)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        if (!context.Companies.Local.Contains(company))
                        {
                            context.Companies.Attach(company);
                        }
                        context.Companies.Remove(company);
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
        public async Task UpdateCompany(CompanyModel company)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        var foundedCompany = context.Companies.ToList().Find(c => c.Id == company.Id);
                        foundedCompany.Name = company.Name;
                        foundedCompany.Logo = company.Logo;
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
                        hosts = context.Companies.Include("Hosts").Include("Hosts.Client").Include("Hosts.HotelRoom.FlightHotel").Include("Hosts.HotelRoom.FlightHotel.Flight").Include("Hosts.HotelRoom.FlightHotel.Hotel").Include("Hosts.HotelRoom.Room").Include("Hosts.HotelRoom").Where(c => c.Id == id).ToList().FirstOrDefault().Hosts.Where(h=>h.HotelRoom.FlightHotel.Flight.SeasonId == Configuration.CurrentSeason.Id).ToList();
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
            var companies = new List<CompanyModel>();
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        companies = context.Companies.Include("Payments").Include("Payments.Company").Include("Payments.Payments").Include("Payments.Flight").ToList();
                        for(int i = 0; i < companies.Count; i++)
                        {
                            companies[i].Payments = companies[i].Payments.Where(p => p.Flight.SeasonId == Configuration.CurrentSeason.Id).ToList();
                        }
                    }
                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return companies;
        }

      
    }
}
