using Airfare.Commands;
using Airfare.DataContext;
using Airfare.Models;
using Airfare.Servies;
using Airfare.ViewModels.DialogViewModels;
using Airfare.Views.Dialogs;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Airfare.ViewModels.UserControlViewModels
{
    public class CompaniesViewModel:BaseViewModel, IDisposable
    {
        #region Stylize
        private bool _LoadingCircle;

        public bool LoadingCircle
        {
            get { return _LoadingCircle; }
            set { _LoadingCircle = value;
                OnPropertyChanged(nameof(LoadingCircle));
            }
        }

        private bool _CompaniesLoadingLine;

        public bool CompaniesLoadingLine
        {
            get { return _CompaniesLoadingLine; }
            set {
                _CompaniesLoadingLine = value;
                OnPropertyChanged(nameof(CompaniesLoadingLine));
            }
        }

        private bool _HostsLoadingLine;

        public bool HostsLoadingLine
        {
            get { return _HostsLoadingLine; }
            set { _HostsLoadingLine = value;
                OnPropertyChanged(nameof(HostsLoadingLine));
            }
        }


        #endregion

        #region Properties
        private CompanyModel _Company;

        public CompanyModel Company
        {
            get { return _Company; }
            set { _Company = value;
                OnPropertyChanged(nameof(Company));
            }
        }

        private ObservableCollection<CompanyModel> _CompaniesList;

        public ObservableCollection<CompanyModel> CompaniesList
        {
            get { return _CompaniesList; }
            set { _CompaniesList = value;
                OnPropertyChanged(nameof(CompaniesList));
            }
        }


        private ICollectionView _SelectedCompanyHostsList;

        public ICollectionView SelectedCompanyHostsList
        {
            get { return _SelectedCompanyHostsList; }
            set
            {
                _SelectedCompanyHostsList = value;
                OnPropertyChanged(nameof(SelectedCompanyHostsList));
                
            }
        }

        private CompanyModel _SelectedCompany;

        public CompanyModel SelectedCompany
        {
            get { return _SelectedCompany; }
            set { _SelectedCompany = value;
                OnPropertyChanged(nameof(SelectedCompany));
                GetSelectedCompanyHosts();
                if(SelectedCompany != null)
                {
                    Paymentslist = CollectionViewSource.GetDefaultView(SelectedCompany.Payments);
                    PaymentsTotal = SelectedCompany.Payments.Select(p => p.Amount).Sum() ?? 0;
                }
            }
        }

        private float _PaidPrice;

        public float PaidPrice
        {
            get { return _PaidPrice; }
            set { _PaidPrice = value;
                OnPropertyChanged(nameof(PaidPrice));
            }
        }

        private float _DiscountPrice;

        public float DiscountPrice
        {
            get { return _DiscountPrice; }
            set { _DiscountPrice = value;
                OnPropertyChanged(nameof(DiscountPrice));
            }
        }

        private int _HostsNumber;

        public int HostsNumber
        {
            get { return _HostsNumber; }
            set { _HostsNumber = value;
                OnPropertyChanged(nameof(HostsNumber));
            }
        }

        private FlightModel _SelectedFlight;

        public FlightModel SelectedFlight
        {
            get { return _SelectedFlight; }
            set { _SelectedFlight = value;
                OnPropertyChanged(nameof(SelectedFlight));
                SelectedCompanyHostsList.Refresh();
                if(CompanyContrastList != null)
                    CompanyContrastList.Refresh();
                if (SelectedFlight != null && SelectedCompany != null)
                {
                    Paymentslist = CollectionViewSource.GetDefaultView(SelectedCompany.Payments.Where(p=>p.FlightId == SelectedFlight.Id));
                    PaymentsTotal = SelectedCompany.Payments.Where(p => p.FlightId == SelectedFlight.Id).Select(p => p.Amount).Sum() ?? 0;
                    DiscountPrice = SelectedCompanyHostsList.Cast<HostModel>().Select(h => h.Discount).Sum();
                    HostsNumber = SelectedCompanyHostsList.Cast<HostModel>().Count();
                    PaidPrice = SelectedCompanyHostsList.Cast<HostModel>().Select(h => h.FullPrice).Sum() - SelectedCompanyHostsList.Cast<HostModel>().Select(h => h.PaidPrice).Sum();
                }
                else
                {
                    Paymentslist = CollectionViewSource.GetDefaultView(SelectedCompany.Payments);
                    PaymentsTotal = SelectedCompany.Payments.Select(p => p.Amount).Sum() ?? 0;
                    DiscountPrice = SelectedCompanyHostsList.Cast<HostModel>().Select(h => h.Discount).Sum();
                    HostsNumber = SelectedCompanyHostsList.Cast<HostModel>().Count();
                    PaidPrice = SelectedCompanyHostsList.Cast<HostModel>().Select(h => h.FullPrice).Sum() - SelectedCompanyHostsList.Cast<HostModel>().Select(h => h.PaidPrice).Sum();

                }
            }
        }

        private float _PaymentAmount;

        public float PaymentAmount
        {
            get { return _PaymentAmount; }
            set { _PaymentAmount = value;
                OnPropertyChanged(nameof(PaymentAmount));
            }
        }

        private ICollectionView _Paymentslist;

        public ICollectionView Paymentslist
        {
            get { return _Paymentslist; }
            set { _Paymentslist = value;
                OnPropertyChanged(nameof(Paymentslist));
            }
        }

        private float _PaymentsTotal;

        public float PaymentsTotal
        {
            get { return _PaymentsTotal; }
            set { _PaymentsTotal = value;
                OnPropertyChanged(nameof(PaymentsTotal));
            }
        }

        private CompanyPaymentModel _SelectedCompanyPayment;

        public CompanyPaymentModel SelectedCompanyPayment
        {
            get { return _SelectedCompanyPayment; }
            set
            {
                _SelectedCompanyPayment = value;
                OnPropertyChanged(nameof(SelectedCompanyPayment));
            }
        }

        private ICollectionView _CompanyContrastList;

        public ICollectionView CompanyContrastList
        {
            get { return _CompanyContrastList; }
            set { _CompanyContrastList = value;
                OnPropertyChanged(nameof(CompanyContrastList));
            }
        }

        private bool _AddingContractLoading;

        public bool AddingContractLoading
        {
            get { return _AddingContractLoading; }
            set { _AddingContractLoading = value;
                OnPropertyChanged(nameof(AddingContractLoading));
            }
        }

        private bool _ExportingContractData;

        public bool ExportingContractData
        {
            get { return _ExportingContractData; }
            set { _ExportingContractData = value;
                OnPropertyChanged(nameof(ExportingContractData));
            }
        }

        private CompanyContractModel _SelectedCompanyContract;

        public CompanyContractModel SelectedCompanyContract
        {
            get { return _SelectedCompanyContract; }
            set { _SelectedCompanyContract = value;
                OnPropertyChanged(nameof(SelectedCompanyContract));
            }
        }
        private bool _ExporContractLoadingCircle;

        public bool ExporContractLoadingCircle
        {
            get { return _ExporContractLoadingCircle; }
            set { _ExporContractLoadingCircle = value;
                OnPropertyChanged(nameof(ExporContractLoadingCircle));
            }
        }


        public FlightModel[] FlightsList { get; set; }
        #endregion

        #region Services
        private readonly CompanyServices companyServices = new();
        private readonly FlightServices flightServices = new();
        private readonly HostServices hostServices = new();
        private readonly CompanyPaymentServices companyPaymentServices = new();
        private readonly PaymentServices paymentServices = new();
        private readonly FlightHotelServices flightHotelServices = new();
        private readonly RoomServices roomServices = new();
        private readonly HotelRoomServices hotelRoomServices = new();
        private readonly SeasonServices seasonServices = new();
        private readonly CompanyContractServices companyContractServices = new();
        private readonly ExportationServices exportationServices = new();
        #endregion

        #region Commands
        public RelayCommand AddCompanyCommand { get; set; }
        public RelayCommand RemoveCompanyCommand { get; set; }
        public RelayCommand UpdateCompanyCommand { get; set; }
        public RelayCommand AddPaymentCommand { get; set; }
        public RelayCommand RemovePaymentCommand { get; set; }
        public RelayCommand AddCompanyContractCommand { get; set; }
        public RelayCommand RemoveCompanyContractCommand { get; set; }
        public RelayCommand ExportCompanyContractCommand { get; set; }
        #endregion

        public CompaniesViewModel()
        {
            InitData();
            AddCompanyCommand = new RelayCommand(AddCompany);
            UpdateCompanyCommand = new RelayCommand(UpdateCompany);
            RemoveCompanyCommand = new RelayCommand(RemoveCompany);
            AddPaymentCommand = new RelayCommand(AddPayment);
            RemovePaymentCommand = new RelayCommand(RemovePayment);
            AddCompanyContractCommand = new RelayCommand(AddCompanyContract);
            RemoveCompanyContractCommand = new RelayCommand(RemoveCompanyContract);
            ExportCompanyContractCommand = new RelayCommand(ExportCompanyContract);
        }
        
        #region Methods

        private async void ExportCompanyContract()
        {
            try
            {
                ExporContractLoadingCircle = true;
                if (SelectedCompany != null)
                {
                    var contracts = await companyContractServices.GetAllCompanyContractsOfCompany(SelectedCompany.Id);
                    await exportationServices.ExportCompanyWordData(SelectedCompany, contracts);
                }
                ExporContractLoadingCircle = false;
            }
            catch (Exception)
            {
                ExporContractLoadingCircle = false;
                Growl.Error("an error occurred while trying to remove company contract");
            }
        }

        private async void RemoveCompanyContract()
        {
            try
            {
                if (SelectedCompanyContract != null)
                {
                    HostsLoadingLine = true;
                    bool res = await Dialog.Show<YesNoDialog>().Initialize<YesNoDialogViewModel>(vm => vm.Description = "هل أنت متأكد أنك تريد حذف عقد هذا المتعامل؟").GetResultAsync<bool>();
                    if (res)
                    {
                        await companyContractServices.RemoveCompanyContract(SelectedCompanyContract);
                        CompanyContrastList = CollectionViewSource.GetDefaultView(await companyContractServices.GetAllCompanyContractsOfCompany(SelectedCompany.Id));
                        CompanyContrastList.Filter = CompanyContrastListFilter;
                        CompanyContrastList.Refresh();
                    }
                    HostsLoadingLine = false;
                }
            }
            catch (Exception)
            {
                Growl.Error("an error occurred while trying to remove company contract");
            }
        }

        private async void AddCompanyContract()
        {
            try
            {
                if (SelectedCompany != null)
                {
                    AddingContractLoading = true;
                    var flighthotels = await flightHotelServices.GetAllFlightHotels();
                    var flights = await flightServices.GetAllFlightsOfSeason(Configuration.CurrentSeason.Id);
                    var rooms = await roomServices.GetAllRooms();
                    var hotelrooms = (await hotelRoomServices.GetAllHotelsRooms()).ToArray();

                    var res = await Dialog.Show<CompanyContractDialog>().Initialize<CompanyContractDialogViewModel>( vm => {
                        vm.CompanyContract = new() { CompanyId = SelectedCompany.Id};
                        vm.FlightHotelsList = CollectionViewSource.GetDefaultView(flighthotels);
                        vm.FlightsList = new ObservableCollection<FlightModel>(flights);
                        vm.SelectedFlight = SelectedFlight != null ?  flights.Where(f => f.Id == SelectedFlight.Id).FirstOrDefault() : null; 
                        vm.SelectedFlightHotel = null;
                        vm.RoomsList = new ObservableCollection<RoomModel>(rooms); 
                        vm.HotelRoomsList = hotelrooms; 
                    }).GetResultAsync<CompanyContractModel>();

                    if(res != null)
                    {
                        var contracts = CompanyContrastList.Cast<CompanyContractModel>().ToList();
                        int paidNumber = SelectedCompanyHostsList.Cast<HostModel>().Where(h => (h.HotelRoomId == res.HotelRoomId && h.IsPaid)).Count();
                        if(paidNumber > 0)
                        {
                            if(paidNumber > res.RoomsNumber)
                                res.PaidNumber = res.RoomsNumber;
                            else
                                res.PaidNumber = paidNumber;

                            await companyContractServices.UpdateCompanyContract(res);
                        }
                        contracts.Add(res);
                        CompanyContrastList = CollectionViewSource.GetDefaultView(contracts);
                        CompanyContrastList.Filter = CompanyContrastListFilter;
                        CompanyContrastList.Refresh();
                    }

                    AddingContractLoading = false;
                }
            }
            catch (Exception)
            {
                Growl.Error("an error occurred while trying to add company contract");
            }
        }

        private async void RemovePayment()
        {
            try
            {
                if(SelectedCompanyPayment != null)
                {
                    HostsLoadingLine = true;
                    bool res = await Dialog.Show<YesNoDialog>().Initialize<YesNoDialogViewModel>(vm => vm.Description = "هل أنت متأكد أنك تريد حذف فاتورة هذا المتعامل؟").GetResultAsync<bool>();

                    if (res)
                    {
                        var hostsPayments = SelectedCompanyPayment.Payments;
                        var contracts = CompanyContrastList.Cast<CompanyContractModel>().ToList();
 
                        for (int i = 0; i < hostsPayments.Count; i++) { 
                            var host = SelectedCompanyHostsList.Cast<HostModel>().Where(h => h.Id == hostsPayments[i].HostId).FirstOrDefault();
                            host.PaidPrice -= hostsPayments[i].Amount;
                            var con = contracts.Where(c => c.HotelRoomId == host.HotelRoomId).FirstOrDefault();
                            host.RemainingPrice += hostsPayments[i].Amount;
                            if(host.FullPrice == host.PaidPrice)
                            {
                                host.IsPaid = true;
                            }
                            else
                            {
                                host.IsPaid = false;
                                if (con != null && con.PaidNumber > 0)
                                    con.PaidNumber--;
                            }
                            await hostServices.UpdateHost(host);
                            await companyContractServices.UpdateCompanyContract(con);
                            hostsPayments[i].CompanyPayment = null;
                            await paymentServices.RemovePayment(hostsPayments[i]);
                        }
                        SelectedCompanyPayment.Company = null;
                        SelectedCompanyPayment.Payments = null;
                        SelectedCompanyPayment.Flight = null;
                        await companyPaymentServices.RemoveCompanyPayment(SelectedCompanyPayment);

                        if (!companyPaymentServices.Error && !paymentServices.Error && !hostServices.Error)
                        {
                            Growl.Success("تم حذف الفاتورة بنجاح");
                        }
                        else
                        {
                            if (companyPaymentServices.Error)
                            {
                                Growl.Error(companyPaymentServices.ErrorMessage);
                            }
                            if (hostServices.Error)
                            {
                                Growl.Error(hostServices.ErrorMessage);
                            }
                            if (paymentServices.Error)
                            {
                                Growl.Error(paymentServices.ErrorMessage);
                            }
                        }
                        SelectedCompany.Payments.Remove(SelectedCompanyPayment);
                        if(SelectedFlight != null)
                        {
                            Paymentslist = CollectionViewSource.GetDefaultView(SelectedCompany.Payments.Where(p => p.FlightId == SelectedFlight.Id));
                        }
                        else
                        {
                            Paymentslist = CollectionViewSource.GetDefaultView(SelectedCompany.Payments);

                        }
                        Paymentslist.Refresh();
                        CompanyContrastList = CollectionViewSource.GetDefaultView(await companyContractServices.GetAllCompanyContractsOfCompany(SelectedCompany.Id));
                        CompanyContrastList.Filter = CompanyContrastListFilter;
                        CompanyContrastList.Refresh();
                        SelectedCompany.Hosts = await companyServices.GetCompanyHostsList(SelectedCompany.Id);
                        SelectedCompanyHostsList = CollectionViewSource.GetDefaultView(SelectedCompany.Hosts.OrderBy(h => h.Id));
                        SelectedCompanyHostsList.Filter = HostsListFilter;
                        SelectedCompanyHostsList.Refresh();
                        if(SelectedFlight != null)
                            PaymentsTotal = SelectedCompany.Payments.Where(p => p.FlightId == SelectedFlight.Id).Select(p => p.Amount).Sum() ?? 0;
                        else
                            PaymentsTotal = SelectedCompany.Payments.Select(p => p.Amount).Sum() ?? 0;
                        HostsLoadingLine = false;

                    }
                }
            }
            catch (Exception)
            {

                Growl.Error("an error occurred while trying to remove the selected company payment");
            }
        }

        private async void AddPayment()
        {
            try
            {
                if (SelectedFlight != null && !SelectedCompanyHostsList.IsEmpty)
                {
                    HostsLoadingLine = true;
                    var hosts = SelectedCompanyHostsList.Cast<HostModel>().Where(h => !h.IsPaid).ToList();
                    var contracts = CompanyContrastList.Cast<CompanyContractModel>().ToList();
                    CompanyPaymentModel companyPayment = new() { Amount = PaymentAmount, Date = DateTime.Now, CompanyId = SelectedCompany.Id, FlightId = SelectedFlight.Id, Payments = new() };
                    await companyPaymentServices.AddCompanyPayment(companyPayment);
                    for (int i = 0; i < hosts.Count; i++)
                    {
                        PaymentModel hostPayment = new() { Date = DateTime.Now, HostId = hosts[i].Id, PaidByCompany = true, CompanyPaymentId = companyPayment.Id };
                        var con = contracts.Where(c => c.HotelRoomId == hosts[i].HotelRoomId).FirstOrDefault();
                        var remain = PaymentAmount - hosts[i].RemainingPrice;
                        if (remain >= 0)
                        {
                            hosts[i].PaidPrice = hosts[i].FullPrice;
                            hostPayment.Amount = hosts[i].RemainingPrice;
                            hosts[i].RemainingPrice = 0;
                            hosts[i].IsPaid = true;
                            PaymentAmount = remain;

                            if (con != null)
                            {
                                con.PaidNumber++;
                            }
                        }
                        else
                        {
                            hosts[i].PaidPrice += PaymentAmount;
                            hostPayment.Amount = PaymentAmount;
                            hosts[i].RemainingPrice = hosts[i].FullPrice - hosts[i].PaidPrice;
                            PaymentAmount = 0;
                        }
                        await paymentServices.AddPayment(hostPayment);
                        companyPayment.Payments.Add(hostPayment);
                        await hostServices.UpdateHost(hosts[i]);
                        if (con != null)
                        {
                            await companyContractServices.UpdateCompanyContract(con);
                        }

                        if (PaymentAmount == 0)
                            break;
                    }

                    if (!companyServices.Error && !hostServices.Error && !paymentServices.Error)
                    {
                        Growl.Success("تم إضافة الدفع بنجاح");

                    }
                    else
                    {
                        if (companyServices.Error)
                        {
                            Growl.Error(companyServices.ErrorMessage);
                        }
                        if (hostServices.Error)
                        {
                            Growl.Error(hostServices.ErrorMessage);
                        }
                        if (paymentServices.Error)
                        {
                            Growl.Error(paymentServices.ErrorMessage);
                        }
                    }
                    companyPayment.Flight = SelectedFlight;
                    SelectedCompany.Payments.Add(companyPayment);
                    Paymentslist = CollectionViewSource.GetDefaultView(SelectedCompany.Payments.Where(p => p.FlightId == SelectedFlight.Id));
                    CompanyContrastList = CollectionViewSource.GetDefaultView(await companyContractServices.GetAllCompanyContractsOfCompany(SelectedCompany.Id));
                    CompanyContrastList.Filter = CompanyContrastListFilter;
                    CompanyContrastList.Refresh();
                    SelectedCompany.Hosts = await companyServices.GetCompanyHostsList(SelectedCompany.Id);
                    SelectedCompanyHostsList = CollectionViewSource.GetDefaultView(SelectedCompany.Hosts.OrderBy(h => h.Id));
                    SelectedCompanyHostsList.Filter = HostsListFilter;
                    SelectedCompanyHostsList.Refresh();
                    if (SelectedFlight != null)
                        PaymentsTotal = SelectedCompany.Payments.Where(p => p.FlightId == SelectedFlight.Id).Select(p => p.Amount).Sum() ?? 0;
                    else
                        PaymentsTotal = SelectedCompany.Payments.Select(p => p.Amount).Sum() ?? 0;
                    HostsLoadingLine = false;
                }
                else if (SelectedCompanyHostsList.IsEmpty)
                {
                    Growl.Warning("لايوجد معتمرون في هذه الرحلة");
                }
                else
                {
                    Growl.Warning("عليك أن تختار رحلة");
                }
            }
            catch (Exception)
            {

                Growl.Error("an error occurred while trying to add company payment");
            }
        }

        private async void GetSelectedCompanyHosts()
        {
            try
            {
                HostsLoadingLine = true;
                if (SelectedCompany != null)
                {
                    if (SelectedCompany.Hosts is null || SelectedCompany.Hosts.Count == 0)
                    {
                        SelectedCompany.Hosts = await companyServices.GetCompanyHostsList(SelectedCompany.Id);
                        SelectedCompanyHostsList = CollectionViewSource.GetDefaultView(SelectedCompany.Hosts.OrderBy(h => h.Id));
                    }
                    else
                    {
                        SelectedCompanyHostsList = CollectionViewSource.GetDefaultView(SelectedCompany.Hosts.OrderBy(h => h.Id));
                    }

                    if (SelectedCompanyHostsList != null)
                    {
                        DiscountPrice = SelectedCompanyHostsList.Cast<HostModel>().Select(h => h.Discount).Sum();
                        PaidPrice = SelectedCompanyHostsList.Cast<HostModel>().Select(h => h.FullPrice).Sum() - SelectedCompanyHostsList.Cast<HostModel>().Select(h => h.PaidPrice).Sum();
                        HostsNumber = SelectedCompanyHostsList.Cast<HostModel>().Count();
                    }
                    else
                    {
                        DiscountPrice = 0;
                        PaidPrice = 0;
                        HostsNumber = 0;
                    }
                    // hosts list filter
                    SelectedCompanyHostsList.Filter = HostsListFilter;
                    CompanyContrastList = CollectionViewSource.GetDefaultView(await companyContractServices.GetAllCompanyContractsOfCompany(SelectedCompany.Id));
                    CompanyContrastList.Filter = CompanyContrastListFilter;
                }
                HostsLoadingLine = false;
            }
            catch (Exception)
            {

                Growl.Error("an error occurred while trying to get the selected company hosts");
            }
        }

        

        private async void RemoveCompany()
        {
            try
            {
                CompaniesLoadingLine = true;
                bool res = await Dialog.Show<YesNoDialog>().Initialize<YesNoDialogViewModel>(vm => vm.Description = "هل أنت متأكد أنك تريد حذف هذا المتعامل؟").GetResultAsync<bool>();
                if (res)
                {
                    SelectedCompany.Hosts = null;
                    SelectedCompanyHostsList = CollectionViewSource.GetDefaultView(null);
                    await companyServices.RemoveCompany(SelectedCompany);
                    if (!companyServices.Error)
                    {
                        InitData();
                        Growl.Success("تم حذف المتعامل بنجاح");
                    }
                    else
                    {
                        Growl.Error("\n:خطأ\n" + companyServices.ErrorMessage);
                    }
                }
                CompaniesLoadingLine = false;
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while removing company");
            }
            
        }

        private async void UpdateCompany()
        {
            try
            {
                CompaniesLoadingLine = true;
                var res = await Dialog.Show<CompanyDialog>().Initialize<CompanyDialogViewModel>(vm => vm.Company = SelectedCompany).GetResultAsync<CompanyModel>();
                if (res is not null)
                {
                    await companyServices.UpdateCompany(SelectedCompany);
                    if (!companyServices.Error)
                    {
                        InitData();
                        Growl.Success("تمت تحديث المتعامل بنجاح");
                    }
                    else
                    {
                        Growl.Error("\n:خطأ\n" + companyServices.ErrorMessage);
                    }
                }
                CompaniesLoadingLine = false;
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while updating company");
            }
        }

        private async void AddCompany()
        {
            try
            {
                LoadingCircle = true;
                await companyServices.AddCompany(Company);
                if (!companyServices.Error)
                {
                    CompaniesList.Add(Company);
                    Company = new();
                    Growl.Success("تمة إضافة المتعامل بنجاح");
                }
                else
                {
                    Growl.Error(companyServices.ErrorMessage);
                }
                LoadingCircle = false;
            }
            catch (Exception)
            {

                Growl.Error("An error occurred while adding company");
            }
        }

        private async void InitData()
        {
            try
            {
                CompaniesLoadingLine = true;
                Company = new();
                // loading all the companies
                CompaniesList = new(await companyServices.GetAllCompanies());
                // making sure that the Configuration data is initiated
                if (Configuration.CurrentSeason is null)
                {
                    var seasons = await seasonServices.GetAllSeasons();
                    Configuration.CurrentSeason = seasons.Where(s => !s.HasEnded).ToList().FirstOrDefault();
                }
                // loading all the flights
                FlightsList = (await flightServices.GetAllFlightsOfSeason(Configuration.CurrentSeason.Id)).ToArray();
                OnPropertyChanged(nameof(FlightsList));

                
                CompaniesLoadingLine = false;
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while InitData in company");
            }
        }

        private bool HostsListFilter(object obj)
        {
            if(obj is HostModel host)
            {
                if (SelectedFlight != null)
                    return host.HotelRoom.FlightHotel.FlightId == SelectedFlight.Id;
                else
                    return true;
            }
            return false;
        }

        private bool CompanyContrastListFilter(object obj)
        {
            if (obj is CompanyContractModel companyContract)
            {
                if (SelectedFlight != null)
                    return companyContract.HotelRoom.FlightHotel.FlightId == SelectedFlight.Id;
                else
                    return true;
            }
            return false;
        }

        public void Dispose()
        {
            GC.Collect();
        }
        #endregion
    }
}
