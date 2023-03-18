using Airfare.DataContext;
using Airfare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Servies
{
    public class CompanyContractServices : BaseServices
    {
        public async Task AddCompanyContract(CompanyContractModel companyContract)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        context.CompanyContracts.Add(companyContract);
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

        public async Task UpdateCompanyContract(CompanyContractModel companyContract)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        var foundedCompanyContract = context.CompanyContracts.ToList().Find(h => h.Id == companyContract.Id);
                        foundedCompanyContract.Price = companyContract.Price;
                        foundedCompanyContract.PaidNumber = companyContract.PaidNumber;
                        foundedCompanyContract.RoomsNumber = companyContract.RoomsNumber;
                        foundedCompanyContract.CompanyId = companyContract.CompanyId;
                        foundedCompanyContract.HotelRoomId = companyContract.HotelRoomId;
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

        public async Task<List<CompanyContractModel>> GetAllCompanyContractsOfCompany(int Id)
        {
            var companyContracts = new List<CompanyContractModel>();
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        companyContracts = context.CompanyContracts.Include("HotelRoom").Include("HotelRoom.Room").Include("HotelRoom.FlightHotel").Include("HotelRoom.FlightHotel.Hotel").Include("HotelRoom.FlightHotel.Flight").Include("Company").Where(cc=>cc.CompanyId == Id).ToList();
                    }

                    Error = false;
                }
                catch (Exception e)
                {
                    Error = true;
                    ErrorMessage = e.Message;
                }
            });
            return companyContracts;
        }

        public async Task RemoveCompanyContract(CompanyContractModel companyContract)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new DataBaseContext())
                    {
                        if (!context.CompanyContracts.Local.Contains(companyContract))
                        {
                            context.CompanyContracts.Attach(companyContract);
                        }
                        context.CompanyContracts.Remove(companyContract);
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
    }
}
