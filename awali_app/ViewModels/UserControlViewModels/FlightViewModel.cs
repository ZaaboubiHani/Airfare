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
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;

namespace Airfare.ViewModels.UserControlViewModels
{
    public class FlightViewModel : BaseViewModel , IDisposable
    {
        #region Stylize

        private bool _ExporContractLoadingCircle;

        public bool ExporContractLoadingCircle
        {
            get { return _ExporContractLoadingCircle; }
            set { _ExporContractLoadingCircle = value;
                OnPropertyChanged(nameof(ExporContractLoadingCircle));
            }
        }

        private bool _MonthsLoadingLine;

        public bool MonthsLoadingLine
        {
            get { return _MonthsLoadingLine; }
            set
            {
                _MonthsLoadingLine = value;
                OnPropertyChanged(nameof(MonthsLoadingLine));
            }
        }

        private bool _AddHotelLoadingCircle;

        public bool AddHotelLoadingCircle
        {
            get { return _AddHotelLoadingCircle; }
            set
            {
                _AddHotelLoadingCircle = value;
                OnPropertyChanged(nameof(AddHotelLoadingCircle));
            }
        }

        private bool _UpdateFlightLoadingCircle;

        public bool UpdateFlightLoadingCircle
        {
            get { return _UpdateFlightLoadingCircle; }
            set
            {
                _UpdateFlightLoadingCircle = value;
                OnPropertyChanged(nameof(UpdateFlightLoadingCircle));
            }
        }

        private bool _ExportDataLoadingCircle;

        public bool ExportDataLoadingCircle
        {
            get { return _ExportDataLoadingCircle; }
            set
            {
                _ExportDataLoadingCircle = value;
                OnPropertyChanged(nameof(ExportDataLoadingCircle));
            }
        }

      
        private bool _AddFlightLoadingCircle;

        public bool AddFlightLoadingCircle
        {
            get { return _AddFlightLoadingCircle; }
            set
            {
                _AddFlightLoadingCircle = value;
                OnPropertyChanged(nameof(AddFlightLoadingCircle));
            }
        }


        private bool _DeleteFlightLoadingCircle;

        public bool DeleteFlightLoadingCircle
        {
            get { return _DeleteFlightLoadingCircle; }
            set { _DeleteFlightLoadingCircle = value;
                OnPropertyChanged(nameof(DeleteFlightLoadingCircle));
            }
        }

        private bool _AddRoomLoadingCircle;

        public bool AddRoomLoadingCircle
        {
            get { return _AddRoomLoadingCircle; }
            set
            {
                _AddRoomLoadingCircle = value;
                OnPropertyChanged(nameof(AddRoomLoadingCircle));
            }
        }

        private bool _AddClientLoadingCircle;

        public bool AddClientLoadingCircle
        {
            get { return _AddClientLoadingCircle; }
            set
            {
                _AddClientLoadingCircle = value;
                OnPropertyChanged(nameof(AddClientLoadingCircle));
            }
        }

        private bool _HotelsLoadingLine;

        public bool HotelsLoadingLine
        {
            get { return _HotelsLoadingLine; }
            set
            {
                _HotelsLoadingLine = value;
                OnPropertyChanged(nameof(HotelsLoadingLine));
            }
        }
        
        #endregion

        #region Properties

        private FlightModel _SelectedFlight;

        public FlightModel SelectedFlight
        {
            get { return _SelectedFlight; }
            set
            {
                _SelectedFlight = value;
                OnPropertyChanged(nameof(SelectedFlight));
                GetClients();

            }
        }

        private FlightModel _Flight;

        public FlightModel Flight
        {
            get { return _Flight; }
            set
            {
                _Flight = value;
                OnPropertyChanged(nameof(Flight));
            }
        }

       

        private ObservableCollection<HotelModel> _HotelsList;

        public ObservableCollection<HotelModel> HotelsList
        {
            get { return _HotelsList; }
            set
            {
                _HotelsList = value;
                OnPropertyChanged(nameof(HotelsList));
            }
        }

        private ObservableCollection<FlightHotelModel> _SelectedFlightHotelsList;

        public ObservableCollection<FlightHotelModel> SelectedFlightHotelsList
        {
            get { return _SelectedFlightHotelsList; }
            set
            {
                _SelectedFlightHotelsList = value;
                OnPropertyChanged(nameof(SelectedFlightHotelsList));
            }
        }

      

        private FlightHotelModel _SelectedFlightHotel;

        public FlightHotelModel SelectedFlightHotel
        {
            get { return _SelectedFlightHotel; }
            set
            {
                _SelectedFlightHotel = value;
                OnPropertyChanged(nameof(SelectedFlightHotel));
                if (value != null)
                {
                    if (HotelsRooms.Count > 0)
                    {
                        DisplayedHotelsRooms = new ObservableCollection<HotelRoomModel>( HotelsRooms.Where(hr => hr.FlightHotel == SelectedFlightHotel).ToList() );
                    }
                    else
                    {
                        DisplayedHotelsRooms.Clear();
                        for (int i = 0; i < Rooms.Length; i++)
                        {
                            DisplayedHotelsRooms.Add(new HotelRoomModel { Room = Rooms[i], RoomId = Rooms[i].Id,Price = 0.0f,FlightHotel = SelectedFlightHotel });
                        }
                    }
                }
                else
                {
                    DisplayedHotelsRooms.Clear();
                }
            }
        }

      
        private ICollectionView _HostsList;

        public ICollectionView HostsList
        {
            get { return _HostsList; }
            set
            {
                _HostsList = value;
                OnPropertyChanged(nameof(HostsList));
            }
        }

        private ICollectionView _FlightsList;

        public ICollectionView FlightsList
        {
            get { return _FlightsList; }
            set
            {
                _FlightsList = value;
                OnPropertyChanged(nameof(FlightsList));
            }
        }

        private ObservableCollection<HotelRoomModel> _DisplayedHotelsRooms;

        public ObservableCollection<HotelRoomModel> DisplayedHotelsRooms
        {
            get { return _DisplayedHotelsRooms; }
            set
            {
                _DisplayedHotelsRooms = value;
                OnPropertyChanged(nameof(DisplayedHotelsRooms));
            }
        }

        private string _SelectedMonth;

        public string SelectedMonth
        {
            get { return _SelectedMonth; }
            set
            {
                _SelectedMonth = value;
                OnPropertyChanged(nameof(SelectedMonth));
                _FlightsList.Refresh();
            }
        }

        private HostModel _SelectedHost;

        public HostModel SelectedHost
        {
            get { return _SelectedHost; }
            set
            {
                _SelectedHost = value;
                OnPropertyChanged(nameof(SelectedHost));
            }
        }

        private int _OrderMethod = -1;

        public int OrderMethod
        {
            get { return _OrderMethod; }
            set
            {
                _OrderMethod = value;
                OnPropertyChanged(nameof(OrderMethod));
                ApplySorting();

            }
        }

        private float _FullPriceTotal;

        public float FullPriceTotal
        {
            get { return _FullPriceTotal; }
            set
            {
                _FullPriceTotal = value;
                OnPropertyChanged(nameof(FullPriceTotal));
            }
        }

        private float _RemainingPriceTotal;

        public float RemainingPriceTotal
        {
            get { return _RemainingPriceTotal; }
            set
            {
                _RemainingPriceTotal = value;
                OnPropertyChanged(nameof(RemainingPriceTotal));
            }
        }

        private float _DiscountTotal;

        public float DiscountTotal
        {
            get { return _DiscountTotal; }
            set
            {
                _DiscountTotal = value;
                OnPropertyChanged(nameof(DiscountTotal));
            }
        }

        private float _PaidPriceTotal;

        public float PaidPriceTotal
        {
            get { return _PaidPriceTotal; }
            set
            {
                _PaidPriceTotal = value;
                OnPropertyChanged(nameof(PaidPriceTotal));
            }
        }

        private int _HostsTotal;

        public int HostsTotal
        {
            get { return _HostsTotal; }
            set
            {
                _HostsTotal = value;
                OnPropertyChanged(nameof(HostsTotal));
            }
        }

        private int _AdultHostsTotal;

        public int AdultHostsTotal
        {
            get { return _AdultHostsTotal; }
            set
            {
                _AdultHostsTotal = value;
                OnPropertyChanged(nameof(AdultHostsTotal));
            }
        }

        private int _CHDHostsTotal;

        public int CHDHostsTotal
        {
            get { return _CHDHostsTotal; }
            set
            {
                _CHDHostsTotal = value;
                OnPropertyChanged(nameof(CHDHostsTotal));
            }
        }

        private int _INFHostsTotal;

        public int INFHostsTotal
        {
            get { return _INFHostsTotal; }
            set
            {
                _INFHostsTotal = value;
                OnPropertyChanged(nameof(INFHostsTotal));
            }
        }

        private List<string> _Filters;

        public List<string> Filters
        {
            get { return _Filters; }
            set
            {
                _Filters = value;
                OnPropertyChanged(nameof(Filters));
               
                if (SelectedFlight != null && !loadinState)
                {
                    HostsList.Refresh();
                    RecalculateStats();
                }
                   
            }
        }

        private ObservableCollection<string> _AvailableFilters = new ObservableCollection<string>( new List<string> { "الاعاشة : موجودة", "الاعاشة : غير موجودة", "الجنس : ذكر", "الجنس : أنثى", "الدفع : مكتمل", "الدفع : غير مكتمل" });

        public ObservableCollection<string> AvailableFilters
        {
            get { return _AvailableFilters; }
            set
            {
                _AvailableFilters = value;
                OnPropertyChanged(nameof(AvailableFilters));
            }
        }

        private ObservableCollection<string> _Months;
        public ObservableCollection<string> Months
        {
            get { return _Months; }
            set
            {
                _Months = value;
                OnPropertyChanged(nameof(Months));
            }
        }

        private HotelModel _SelectedHotel;

        public HotelModel SelectedHotel
        {
            get { return _SelectedHotel; }
            set { _SelectedHotel = value;
                OnPropertyChanged(nameof(SelectedHotel));
            }
        }

        private bool _RemoveHotelLoadingCircle;

        public bool RemoveHotelLoadingCircle
        {
            get { return _RemoveHotelLoadingCircle; }
            set { _RemoveHotelLoadingCircle = value;
                OnPropertyChanged(nameof(RemoveHotelLoadingCircle));
            }
        }

        private bool _UpdateHotelLoadingCircle;

        public bool UpdateHotelLoadingCircle
        {
            get { return _UpdateHotelLoadingCircle; }
            set
            {
                _UpdateHotelLoadingCircle = value;
                OnPropertyChanged(nameof(UpdateHotelLoadingCircle));
            }
        }

        private bool _FlightFuntionsButonsVisibility;

        public bool FlightFuntionsButonsVisibility
        {
            get { return _FlightFuntionsButonsVisibility; }
            set { _FlightFuntionsButonsVisibility = value;
                OnPropertyChanged(nameof(FlightFuntionsButonsVisibility));
            }
        }

        private bool _ImportDataLoadingCircle;

        public bool ImportDataLoadingCircle
        {
            get { return _ImportDataLoadingCircle; }
            set
            {
                _ImportDataLoadingCircle = value;
                OnPropertyChanged(nameof(ImportDataLoadingCircle));
            }
        }

        private bool _UpdatingClientLoading;

        public bool UpdatingClientLoading
        {
            get { return _UpdatingClientLoading; }
            set { _UpdatingClientLoading = value;
                OnPropertyChanged(nameof(UpdatingClientLoading));
            }
        }

        private bool _ShowingClients;

        public bool ShowingClients
        {
            get { return _ShowingClients; }
            set { _ShowingClients = value;
                OnPropertyChanged(nameof(ShowingClients));
            }
        }


        public RoomModel[] Rooms { get; set; }
        public List<HotelRoomModel> HotelsRooms { get; set; }
        public List<HotelRoomModel> SelectedFlightHotelRooms { get; set; }
        private bool loadinState { get; set; }

        #endregion

        #region Services
        private readonly RoomServices roomServices = new();
        private readonly FlightServices flightServices = new();
        private readonly HotelServices hotelServices = new();
        private readonly SeasonServices seasonServices = new();
        private readonly HotelRoomServices hotelRoomServices = new();
        private readonly HostServices hostServices = new();
        private readonly FlightHotelServices flightHotelServices = new();
        private readonly CompanyServices companyServices = new();
        private readonly ExportationServices exportationServices = new();
        #endregion

        #region Commads
        public RelayCommand ShowHotelDialogCommand { get; set; }
        public RelayCommand AddClientCommand { get; set; }
        public RelayCommand ShowRoomDialogCommand { get; set; }
        public RelayCommand AddFlightCommand { get; set; }
        public RelayCommand ShowFlightDialogCommand { get; set; }
        public RelayCommand ExportExcelDataCommand { get; set; }
        public RelayCommand ImportExcelDataCommand { get; set; }
        public RelayCommand ExportWordDataCommand { get; set; }
        public RelayCommand RemoveHostCommand { get; set; }
        public RelayCommand RemoveFlightCommand { get; set; }
        public RelayCommand RemoveHotelCommand { get; set; }
        public RelayCommand UpdateHotelCommand { get; set; }
        public RelayCommand ExportClientPaymentCommand { get; set; }
        #endregion

        public FlightViewModel()
        {
            InitCommands();
            Task.Run(() => InitData());
        }

        #region Methods
        private void InitCommands()
        {
            try
            {
                Parallel.Invoke(
                    ()=>{ShowHotelDialogCommand = new RelayCommand(ShowHotelDialog);  },
                    ()=>{AddClientCommand = new RelayCommand(AddClient);  },
                    ()=>{ShowRoomDialogCommand = new RelayCommand(ShowRoomDialog);  },
                    ()=>{AddFlightCommand = new RelayCommand(AddFlight);  },
                    ()=>{ShowFlightDialogCommand = new RelayCommand(ShowFlightDialog);  },
                    ()=>{ExportExcelDataCommand = new RelayCommand(ExportExcelData);  },
                    ()=>{ExportWordDataCommand = new RelayCommand(ExportWordData);  },
                    ()=>{RemoveHostCommand = new RelayCommand(RemoveHost);  },
                    ()=>{RemoveFlightCommand = new RelayCommand(RemoveFlight);  },
                    ()=>{RemoveHotelCommand = new RelayCommand(RemoveHotel);  },
                    ()=>{UpdateHotelCommand = new RelayCommand(UpdateHotel);  },
                    ()=>{ImportExcelDataCommand = new RelayCommand(ImportExcelData);  },
                    ()=>{ ExportClientPaymentCommand = new RelayCommand(ExportClientPayment); }
                 );
                
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                Growl.Error("An error occurred while InitCommands in flight");
            }
        
        }

        private async void ExportClientPayment()
        {
            try
            {
                ExporContractLoadingCircle = true;
                if (SelectedHost.Payments.Count > 0)
                {
                    await exportationServices.ExportHostPaymentData(SelectedHost);

                    if (exportationServices.Error)
                    {
                        Growl.Error("An error occurred while trying to export word data");

                    }
                }
                else
                {
                    Growl.Warning("لايملك هذا المعتمر أي فواتير دفع, من أجل اضافة فاتور قم بتعديل المعتمر");
                }
               
                ExporContractLoadingCircle = false;
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                Growl.Error("An error occurred while exporting client payment");
            }
        }

        private async void ImportExcelData()
        {
            try
            {
                ImportDataLoadingCircle = true;
                var importedHosts = await exportationServices.ImportFlightExcelData(SelectedFlight.Id);
                if(importedHosts != null)
                {
                    await hostServices.AddAllHost(importedHosts);
                    if (!hostServices.Error)
                    {
                        Growl.Success("تم تحميل البيانات بنجاح");
                        InitData();
                    }
                    else
                    {
                        Growl.Error("\n:خطأ\n" + hostServices.ErrorMessage);
                    }
                }
                ImportDataLoadingCircle = false;
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                ImportDataLoadingCircle = false;
                Growl.Error("An error occurred while Import Data to flight");
            }
           
        }

        private async void UpdateHotel()
        {
            try
            {
                UpdateHotelLoadingCircle = true;
                if (SelectedHotel != null)
                {
                    var res = await Dialog.Show<HotelDialog>().Initialize<HotelDialogViewModel>(vm =>
                    {
                        vm.UpdateMode = true;
                        vm.Hotel = SelectedHotel.Clone() as HotelModel;
                       

                    }).GetResultAsync<HotelModel>();
                    if (res != null)
                    {
                        SelectedHotel = res;
                    }
                }
                UpdateHotelLoadingCircle = false;
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                UpdateHotelLoadingCircle = false;
                Growl.Error("An error occurred while trying to update the hotel");
            }
          
        }
        

        private async void RemoveHotel()
        {
            try
            {
                RemoveHotelLoadingCircle = true;
                if (SelectedHotel != null)
                {
                    bool result = await Dialog.Show<YesNoDialog>().Initialize<YesNoDialogViewModel>(vm => vm.Description = "هل أنت متأكد أنك تريد حذف هذا الفندق؟").GetResultAsync<bool>();
                    if (result)
                    {
                        await hotelServices.RemoveHotel(SelectedHotel.Id);
                        if (!hotelServices.Error)
                        {
                            InitData();
                        }
                        else
                        {
                            Growl.Error("\n:خطأ\n" + hotelServices.ErrorMessage);
                        }
                    }
                   
                }
                else
                {
                    Growl.Warning("عليك اختيار فندق من قائمة الفنادق المتاحة");
                    RemoveHotelLoadingCircle = false;
                }
                RemoveHotelLoadingCircle = false;
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                RemoveHotelLoadingCircle = false;
                Growl.Error("An error occurred while trying to remove the hotel");
            }
          
        }

        private async void RemoveFlight()
        {
            try
            {
                DeleteFlightLoadingCircle = true;
                bool result = await Dialog.Show<YesNoDialog>().Initialize<YesNoDialogViewModel>(vm => vm.Description = "هل أنت متأكد أنك تريد حذف هذه الرحلة؟").GetResultAsync<bool>();
                if (result)
                {
                    await flightServices.RemoveFlight(SelectedFlight.Id);
                    if (!flightServices.Error)
                    {
                        InitData();
                        Growl.Success("تم حذف الرحلة بنجاح");
                    }
                    else
                    {
                        Growl.Error("\n:خطأ\n" + hostServices.ErrorMessage);
                    }
                }
                DeleteFlightLoadingCircle = false;
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                DeleteFlightLoadingCircle = false;
                Growl.Error("An error occurred while trying to remove the flight");
            }
            
        }

        private async void RemoveHost()
        {
            try
            {
                bool result = await Dialog.Show<YesNoDialog>().Initialize<YesNoDialogViewModel>(vm => vm.Description = "هل أنت متأكد أنك تريد حذف هذا المعتمر؟").GetResultAsync<bool>();
                if (result)
                {
                    ShowingClients = true;
                    await hostServices.RemoveHost(SelectedHost.Id);
                    if (!hostServices.Error)
                    {
                        var hosts = (List<HostModel>)HostsList.SourceCollection;
                        hosts.Remove(SelectedHost);
                        HostsList.Refresh();
                        RecalculateStats();
                        Growl.Success("تم حذف المعتمر بنجاح");
                    }
                    else
                    {
                        Growl.Error("\n:خطأ\n" + hostServices.ErrorMessage);
                    }
                }
            }
            catch (Exception e)
            {

                LogService.LogError(e.Message, this);
                Growl.Error("An error occurred while trying to remove this client");
            }
            finally
            {
                ShowingClients = false;
            }
            
        }

        private  async void ExportWordData()
        {

            try
            {
                ExporContractLoadingCircle = true;
                await exportationServices.ExportHostWordData(SelectedHost, SelectedFlight);

                if (exportationServices.Error)
                {
                    Growl.Error("An error occurred while trying to export word data");

                }
                ExporContractLoadingCircle = false;
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                ExporContractLoadingCircle = false;
                Growl.Error("An error occurred while trying to export client word data");
            }
        }

        private async void ExportExcelData()
        {

            try
            {
                ExportDataLoadingCircle = true;

                if (SelectedFlight != null)
                {
                    await exportationServices.ExportFlightExcelData(HostsList.Cast<HostModel>().ToList(), SelectedFlight, HostsTotal, AdultHostsTotal, INFHostsTotal, CHDHostsTotal);
                    if (exportationServices.Error)
                    {
                        Growl.Error("An error occurred while trying to export excel data");
                    }
                }
                else
                {
                    Growl.Warning("لم تقم باختيار رحلة بعد");
                }
                ExportDataLoadingCircle = false;
            }
            catch (Exception)
            {
                ExportDataLoadingCircle = false;
                Growl.Error("An error occurred while trying to export flight excel data");
            }
        }

        public async void UpdateClient()
        {
            try
            {
                ShowingClients = true;
                var flighthotels = await flightHotelServices.GetFlightHotelsOfFlight(SelectedFlight.Id);
                var hotelsRooms = await hotelRoomServices.GetHotelsRoomsOfFlight(SelectedFlight.Id);
                var companies = await companyServices.GetAllCompanies();
                var host = SelectedHost.Clone() as HostModel;
                var res = await Dialog.Show<ClientDialog>().Initialize<ClientDialogViewModel>(vm =>
                {
                    vm.UpdateMode = true;
                    vm.Client = host.Client;
                    vm.Host = host;
                    vm.PhonesList = new ObservableCollection<PhoneModel>(host.Client.Phones ?? new());
                    vm.PaymentsList = new ObservableCollection<PaymentModel>(host.Payments ?? new());
                    vm.Feed = host.Client.Feed;
                    vm.IsGuide = host.Client.IsGuide;
                    vm.RemainingPrice = host.RemainingPrice;
                    vm.Discount = host.Discount;
                    vm.PaidPrice = host.PaidPrice;
                    vm.Rooms = Rooms.ToList();
                    vm.Companies = companies;
                    vm.SelectedCompany = host.Company;
                    vm.hiddenCompanySelector = host.Company;
                    vm.SelectedRoom = host.HotelRoom.Room;
                    vm.HotelsRooms = hotelsRooms;
                    vm.FlightHotels = flighthotels;
                    vm.SelectedFlightHotel = host.HotelRoom.FlightHotel;
                }).GetResultAsync<HostModel>();
                if (res != null)
                {
                    var hostslist = (List<HostModel>)HostsList.SourceCollection;
                    if (string.IsNullOrEmpty(res.Client.Color))
                        res.Client.Color = res.HotelRoom.Room.Color;

                    hostslist.UpdateValue(SelectedHost, res);
                    HostsList.Refresh();
                    RecalculateStats();
                }
            
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                Growl.Error("An error occurred while trying to update the client");
                
            }
            finally
            {
                ShowingClients = false;
            }
            
        }

        private async Task RecalculateStats()
        {
          
            try
            {
                ShowingClients = true;
         
                if (SelectedFlight != null)
                {
                    FlightFuntionsButonsVisibility = true;

                    // Convert the selected hosts list to an array
                    var hostsList = HostsList.Cast<HostModel>().ToArray();
         
                    // Calculate the number of adult, child, and infant hosts
                    AdultHostsTotal = hostsList.Count(h => (SelectedFlight.DepartDate - h.Client.BirthDate).TotalDays / 365.25 >= 12);
                    CHDHostsTotal = hostsList.Count(h => (SelectedFlight.DepartDate - h.Client.BirthDate).TotalDays / 365.25 > 2 && (SelectedFlight.DepartDate - h.Client.BirthDate).TotalDays / 365.25 < 12);
                    INFHostsTotal = hostsList.Count(h => (SelectedFlight.DepartDate - h.Client.BirthDate).TotalDays / 365.25 <= 2);
         
                    // Calculate the total prices
                    FullPriceTotal = hostsList.Select(h => h.FullPrice).Sum();
                    DiscountTotal = hostsList.Select(h => h.Discount).Sum();
                    PaidPriceTotal = hostsList.Select(h => h.PaidPrice).Sum();
                    RemainingPriceTotal = hostsList.Select(h => h.RemainingPrice).Sum();
         
                    // Set the total number of hosts
                    HostsTotal = hostsList.Count();
         
                    // Hide the flight client loading line
                    
                }
         
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                Growl.Error("An error occurred while trying to execute GetClients Function");
            }
            finally
            {
                ShowingClients = false;
            }
        }

        private async Task GetClients()
        {
            try
            {

                ShowingClients = true;
                if (SelectedFlight != null)
                {
                    FlightFuntionsButonsVisibility = true;
                    var hostsList = await hostServices.GetAllHostsOfFlight(SelectedFlight.Id);
                    hostsList = hostsList.OrderBy(h => h.Id).OrderBy(h => h.Client.IsGuide).ToList();
                    HostsList = CollectionViewSource.GetDefaultView(hostsList);
                    HostsList.Filter = HostsListFilter;
                    HostsList.Refresh();

                    hostsList = (List<HostModel>)HostsList.SourceCollection;

                    for (int i = 0; i < hostsList.Count; i++)
                    {

                        if (string.IsNullOrEmpty(hostsList[i].Client.Color))
                        {
                            hostsList[i].Client.Color = hostsList[i].HotelRoom.Room.Color;
                        }
                    }
                    hostsList = HostsList.Cast<HostModel>().ToList();
                    FullPriceTotal = hostsList.Select(h=>h.FullPrice).Sum();
                    DiscountTotal = hostsList.Select(h => h.Discount).Sum();
                    PaidPriceTotal = hostsList.Select(h => h.PaidPrice).Sum();
                    RemainingPriceTotal = hostsList.Select(h => h.RemainingPrice).Sum();
                    AdultHostsTotal = hostsList.Count(h => (SelectedFlight.DepartDate - h.Client.BirthDate).TotalDays / 365.25 >= 12);
                    CHDHostsTotal = hostsList.Count(h => (SelectedFlight.DepartDate - h.Client.BirthDate).TotalDays / 365.25 > 2 && (SelectedFlight.DepartDate - h.Client.BirthDate).TotalDays / 365.25 < 12);
                    INFHostsTotal = hostsList.Count(h => (SelectedFlight.DepartDate - h.Client.BirthDate).TotalDays / 365.25 <= 2);
                    HostsTotal = hostsList.Count;
                }
               
                
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                Growl.Error("An error occurred while trying to execute ShowClients Function");
            }
            finally
            {
                ShowingClients = false;
            }
        }
        private bool RoomFilter(HostModel host)
        {
            // Get a list of rooms that match the selected room types in the filter.
            var rooms = Rooms.Where(r => Filters.Contains("نوع الغرفة : " + r.Type)).ToList();
            // If no room types are selected in the filter, include the host in the filtered list.
            if (rooms.Count == 0)
            {
                return true;
            }
            // Check if the host's room matches any of the selected room types in the filter.
            return rooms.Any(r => r.Id == host.HotelRoom.RoomId);
        }

       
        private bool FeedFilter(HostModel host)
        {
            // Check if the filter exists for the feed
            bool feedFilterExist = Filters.Contains("الاعاشة : موجودة") || Filters.Contains("الاعاشة : غير موجودة");

            if (feedFilterExist)
            {
                // Check if the filter is set to "الاعاشة : موجودة"
                if (Filters.Contains("الاعاشة : موجودة"))
                {
                    // Return true if the client has feed
                    return host.Client.Feed;
                }
                else
                {
                    // Return true if the client doesn't have feed
                    return !host.Client.Feed;
                }
            }
            else
            {
                // If the filter doesn't exist, return true
                return true;
            }
        }

       
        private bool PaidFilter(HostModel host)
        {
            // Check if any paid filters exist
            bool paidFilterExist = Filters.Contains("الدفع : مكتمل") || Filters.Contains("الدفع : غير مكتمل");

            // If no paid filters exist, return true
            if (!paidFilterExist)
            {
                return true;
            }

            // Check if the paid filter is for "مكتمل"
            if (Filters.Contains("الدفع : مكتمل"))
            {
                // If the host is paid, return true
                if (host.IsPaid)
                {
                    return true;
                }
            }
            // Otherwise, check if the paid filter is for "غير مكتمل"
            else
            {
                // If the host is not paid, return true
                if (!host.IsPaid)
                {
                    return true;
                }
            }

            // If the host does not match the paid filter, return false
            return false;
        }

        private bool GenderFilter(HostModel host)
        {
            if (!Filters.Any(filter => filter.StartsWith("الجنس :")))
                return true; // no gender filter exists

            bool isMale = !host.Client.Gender; // assuming true for female and false for male
            bool isMaleFilter = Filters.Contains("الجنس : ذكر");
            bool isFemaleFilter = Filters.Contains("الجنس : أنثى");

            if (isMaleFilter && isFemaleFilter)
                return true; // both filters exist, so ignore gender filter
            else if (isMaleFilter && isMale)
                return true; // male filter exists and host is male
            else if (isFemaleFilter && !isMale)
                return true; // female filter exists and host is female
            else
                return false; // gender filter exists but host doesn't match
        }

       
        private bool HotelFilter(HostModel host)
        {
            // Extract the names of the hotels from the HotelsList and store them in an array
            var hotelsNames = HotelsList.Select(h => h.Name).ToArray();

            // Check if any hotel names from the HotelsList are included in the Filters property
            bool hotelFilterExist = hotelsNames.Any(name => Filters.Contains("نوع الفندق : " + name));

            // If there is at least one matching hotel name, filter the HostModels based on whether they
            // have a HotelModel with a matching Id in the HotelsList
            if (hotelFilterExist)
            {
                // Loop through each HotelModel in the HotelsList
                for (int i = 0; i < HotelsList.Count; i++)
                {
                    // If the current hotel name is included in the Filters property
                    if (Filters.Contains("نوع الفندق : " + HotelsList[i].Name))
                    {
                        // Check if the HostModel has a HotelModel with the same Id as the current HotelModel
                        if (host.HotelRoom.FlightHotel.HotelId == HotelsList[i].Id)
                        {
                            // If so, return true to indicate that the HostModel should be included in the filter results
                            return true;
                        }
                    }
                }
            }
            // If no matching hotel names are found in the Filters property, include all HostModels in the filter results
            else
            {
                return true;
            }
            // If the HostModel does not match the filter conditions, return false
            return false;
        }



        private async void ShowFlightDialog()
        {
            try
            {
                UpdateFlightLoadingCircle = true;
                if (SelectedFlight != null)
                {
                    var flighthotels = await flightHotelServices.GetFlightHotelsOfFlight(SelectedFlight.Id);
                    var hotelrooms = await hotelRoomServices.GetHotelsRoomsOfFlight(SelectedFlight.Id);
                    var res = await Dialog.Show<FlightDialog>().Initialize<FlightDialogViewModel>(vm =>
                    {
                        vm.hotelRoomsControlList = hotelrooms.ToList();
                        vm.Flight = SelectedFlight;
                        vm.HotelsList = HotelsList;
                        vm.SelectedFlightHotelsList = new ObservableCollection<FlightHotelModel>(flighthotels);
                        vm.HotelRooms = hotelrooms.ToList();
                        vm.flightHotelsControlList = flighthotels;
                    }).GetResultAsync<FlightModel>();
                    if (res is not null)
                    {
                        InitData();
                        Filters.Clear();
                        SelectedMonth = null;
                        Growl.Success("تم تحديث الرحلة بنجاح");
                    }
                }
                else
                {
                    Growl.Warning("لم تقم باختيار رحلة بعد");
                }
               
            }
            catch (Exception e)
            {

                LogService.LogError(e.Message, this);
                Growl.Error("An error occurred while trying to display the flight dialog");
            }
            finally
            {
                UpdateFlightLoadingCircle = false;
            }
           
        }

       

        private async void AddFlight()
        {
            try
            {
                AddFlightLoadingCircle = true;
                
                
                if (SelectedFlightHotelsList.Count > 0 && Flight.Id == 0)
                {
                    var seasons = await seasonServices.GetAllSeasons();
                    Configuration.CurrentSeason = seasons.Where(s => !s.HasEnded).ToList().FirstOrDefault();
                    Flight.SeasonId = Configuration.CurrentSeason.Id;
                    if (!string.IsNullOrEmpty(Flight.ReturnName) && !string.IsNullOrEmpty(Flight.DepartName))
                    {
                        if (Flight.Capacity is null)
                            Flight.Capacity = 0;
                        await flightServices.AddFlight(Flight);
                    }
                    else
                    {
                        Growl.Warning("عليك كتابة معلومات الرحلة");
                    }
                    for (int i = 0; i < SelectedFlightHotelsList.Count; i ++)
                    {
                        SelectedFlightHotelsList[i].FlightId = Flight.Id;
                        SelectedFlightHotelsList[i].HotelId = SelectedFlightHotelsList[i].Hotel.Id;
                        SelectedFlightHotelsList[i].Hotel = null;
                        await flightHotelServices.AddFlightHotel(SelectedFlightHotelsList[i]);
                        var hotelrooms = HotelsRooms.Where(hr => hr.FlightHotel == SelectedFlightHotelsList[i]).ToArray();
                        for (int j = 0; j < hotelrooms.Length; j ++)
                        {
                            hotelrooms[j].FlightHotelId = SelectedFlightHotelsList[i].Id;
                            hotelrooms[j].FlightHotel = null;
                            hotelrooms[j].Room = null;
                            await hotelRoomServices.AddHotelRoom(hotelrooms[j]);
                        }
                    }
                    if (!flightHotelServices.Error && !hotelRoomServices.Error && !flightServices.Error)
                    {
                        InitData();
                        Growl.Success("تمت إضافة الرحلة بنجاح");
                    }
                }
                else if(Flight.Id == 0)
                {
                    var seasons = await seasonServices.GetAllSeasons();
                    Configuration.CurrentSeason = seasons.Where(s => !s.HasEnded).ToList().FirstOrDefault();
                    Flight.SeasonId = Configuration.CurrentSeason.Id;
                    if (!string.IsNullOrEmpty(Flight.ReturnName) && !string.IsNullOrEmpty(Flight.DepartName))
                    {
                        if (Flight.Capacity is null)
                            Flight.Capacity = 0;
                        await flightServices.AddFlight(Flight);
                        if (!flightServices.Error)
                        {
                            Flight = new() { SeasonId = Configuration.CurrentSeason.Id, DepartDate = DateTime.Now, ReturntDate = DateTime.Now, DepartTime = DateTime.Now.TimeOfDay, ReturnTime = DateTime.Now.TimeOfDay };
                            Growl.Success("تمت إضافة الرحلة بنجاح");
                        }
                    }
                    else
                    {
                        Growl.Error("error occurred while trying to add flight");
                    }
                }
                else
                {
                    Growl.Warning("لم تقم بتحديد رحلة بعد");
                }

               
            }
            catch (Exception e)
            {

                LogService.LogError(e.Message, this);
                Growl.Error("An error occurred while trying to add a flight");
            }
            finally
            {
                AddFlightLoadingCircle = false;
            }
            
        }

        private async void ShowRoomDialog()
        {
            try
            {
                AddRoomLoadingCircle = true;
                RoomModel? res = await Dialog.Show<RoomDialog>().GetResultAsync<RoomModel>();
                if (res != null)
                {
                    Rooms.Append(res);

                    for (int i = 0; i < SelectedFlightHotelsList.Count; i++)
                    {
                        HotelsRooms.Add(new HotelRoomModel { FlightHotel = SelectedFlightHotelsList[i], Room = res, RoomId = res.Id });
                    }
                    var selectedflighthotel = SelectedFlightHotel;
                    DisplayedHotelsRooms.Add(new HotelRoomModel { FlightHotel = SelectedFlightHotel, Room = res, RoomId = res.Id });
                    List<FlightHotelModel> flightHotels = new List<FlightHotelModel>();
                    flightHotels = SelectedFlightHotelsList.ToList();
                    SelectedFlightHotelsList.Clear();
                    SelectedFlightHotelsList = new ObservableCollection<FlightHotelModel>(flightHotels.ToList());
                    SelectedFlightHotel = selectedflighthotel;
                }
               
            }
            catch (Exception e)
            {

                LogService.LogError(e.Message, this);
                Growl.Error("An error occurred while trying to display the room dialog");
            }
            finally
            {
                AddRoomLoadingCircle = false;
            }
           
        }

        private async void AddClient()
        {
            try
            {
                AddClientLoadingCircle = true;
                if (SelectedFlight != null)
                {
                    var flighthotels = await flightHotelServices.GetFlightHotelsOfFlight(SelectedFlight.Id);
                    var hotelsRooms = await hotelRoomServices.GetHotelsRoomsOfFlight(SelectedFlight.Id);
                    if (hotelsRooms.Count > 0 && flighthotels.Count > 0)
                    {
                        var res = await Dialog.Show<ClientDialog>().Initialize<ClientDialogViewModel>(vm => { vm.FlightHotels = flighthotels; vm.Rooms = Rooms.ToList(); vm.HotelsRooms = hotelsRooms; }).GetResultAsync<HostModel>();
                        if (res != null)
                        {
                            var hostslist = (List<HostModel>)HostsList.SourceCollection;
                            if (string.IsNullOrEmpty(res.Client.Color))
                                res.Client.Color = res.HotelRoom.Room.Color;

                            hostslist.Add(res);
                            HostsList.Refresh();
                            RecalculateStats();
                        }
                    }
                    else
                    {
                        Growl.Warning("لا تحتوي هذه الرحلة على فناد متاحة, من فضلك قم بإضافتها من خلال تعديل الرحلة");
                    }
                }
                else
                {
                    Growl.Warning("لم تقم باختيار رحلة بعد");
                }
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                Growl.Error("An error occurred while trying to display the client dialog");
            }
            finally
            {
                AddClientLoadingCircle = false;

            }

        }

        private async void ShowHotelDialog()
        {
            try
            {
                AddHotelLoadingCircle = true;
                HotelModel? res = await Dialog.Show<HotelDialog>().GetResultAsync<HotelModel>();
                if (res != null)
                {
                    HotelsList.Add(res);
                }
                
            }
            catch (Exception e)
            {

                LogService.LogError(e.Message, this);
                Growl.Error("An error occurred while tring to display the hotel dialog");
            }
            finally
            {
                AddHotelLoadingCircle = false;
            }
            
        }

        private async Task InitData()
        {
            try 
            {
                // loading indicators
                loadinState = true;
                MonthsLoadingLine = true;
                HotelsLoadingLine = true;

                // making sure that the Configuration data is initiated
                if(Configuration.CurrentSeason is null)
                {
                    Configuration.CurrentSeason = await seasonServices.GetFirstActiveSeason();
                }
                // init flight
                Flight = new() { SeasonId = Configuration.CurrentSeason.Id, DepartDate = DateTime.Now, ReturntDate = DateTime.Now, DepartTime = DateTime.Now.TimeOfDay, ReturnTime = DateTime.Now.TimeOfDay };

                // loading all the available hotels
                HotelsList = new ObservableCollection<HotelModel>(await hotelServices.GetAllHotels());

                // loading all the flights
                var flights = await flightServices.GetAllFlightsOfSeason(Configuration.CurrentSeason.Id);
                FlightsList = CollectionViewSource.GetDefaultView(flights);
                FlightsList.Filter = FlightsListFilter;
                // loading all the months
                Months = new(flights.Select(f => f.DepartDate.ToString("MMMM", new CultureInfo("ar-DZ"))).Distinct());
              
                // loading rooms
                Rooms = (await roomServices.GetAllRooms()).ToArray();

                // loading the available filters from hotels and rooms
                AvailableFilters.AddRange(HotelsList.Select(h => "نوع الفندق : " + h.Name));
                AvailableFilters.AddRange(Rooms.Select(r => "نوع الغرفة : " + r.Type));
                // loading the hotel rooms
                SelectedFlightHotelRooms = await hotelRoomServices.GetAllHotelsRooms();

                // assgining the rest of the properties
               
                SelectedFlightHotelsList = new ObservableCollection<FlightHotelModel>();
                HotelsRooms = new List<HotelRoomModel>();
                DisplayedHotelsRooms = new ObservableCollection<HotelRoomModel>();
               
                Filters = new();
            }
            catch (Exception e)
            {

                LogService.LogError(e.Message, this);
                Growl.Error("An error occurred while trying to InitData in flight"); 
            }
            finally
            {
                HotelsLoadingLine = false;
                loadinState = false;
                MonthsLoadingLine = false;
            }
    
        }

        private bool FlightsListFilter(object obj)
        {
            if (obj is FlightModel flight)
            {
                return SelectedMonth != null ? flight.DepartDate.ToString("MMMM", new CultureInfo("ar-DZ")) == SelectedMonth : false;
            }
            return false;
        }

        private bool HostsListFilter(object obj)
        {
            if(obj is HostModel host)
            {
                return SelectedFlight != null ? (GenderFilter(host) && FeedFilter(host) && RoomFilter(host) && HotelFilter(host) && PaidFilter(host)) : false; 
            }
            return false;
        }
        private async Task ApplySorting()
        {
            try
            {
                ShowingClients = true;

                if (SelectedFlight == null)
                {
                    Growl.Warning("لم تقم باختيار رحلة بعد");
                    return;
                }

                var sortedList = HostsList.Cast<HostModel>().OrderBy(h => h.Client.IsGuide);

                switch (_OrderMethod)
                {
                    case 0:
                        sortedList = sortedList.ThenBy(h => h.Client.LastName);
                        break;
                    case 1:
                        sortedList = sortedList.ThenBy(h => h.Client.FirstName);
                        break;
                    case 2:
                        sortedList = sortedList.ThenBy(h => h.Client.Gender);
                        break;
                    case 3:
                        sortedList = sortedList.ThenBy(h => h.Client.Feed);
                        break;
                    case 4:
                        sortedList = sortedList.ThenBy(h => h.FullPrice);
                        break;
                    case 5:
                        sortedList = sortedList.ThenBy(h => h.PaidPrice);
                        break;
                    case 6:
                        sortedList = sortedList.ThenBy(h => h.RemainingPrice);
                        break;
                    case 7:
                        sortedList = sortedList.ThenBy(h => h.HotelRoom.Room.Type);
                        break;
                    case 8:
                        sortedList = sortedList.ThenBy(h => h.HotelRoom.FlightHotel.Hotel.Name);
                        break;
                    default:
                        sortedList = sortedList.ThenBy(h => h.Id);
                        break;
                }

                HostsList = CollectionViewSource.GetDefaultView(sortedList);

            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                Growl.Error("An error occurred while trying to sort hosts list");
            }
            finally
            {
                ShowingClients = false;
            }
        }


        //private async Task ApplySorting()
        //{
        //    try
        //    {
        //        ShowingClients = true;
        //        if (SelectedFlight != null)
        //        {
        //            switch (_OrderMethod)
        //            {
        //                case 0:
        //                    HostsList = CollectionViewSource.GetDefaultView(HostsList.Cast<HostModel>().OrderBy(host => host.Client.LastName).OrderBy(h => h.Client.IsGuide));
        //                    break;
        //                case 1:
        //                    HostsList = CollectionViewSource.GetDefaultView(HostsList.Cast<HostModel>().OrderBy(host => host.Client.FirstName).OrderBy(h => h.Client.IsGuide));
        //                    break;
        //                case 2:
        //                    HostsList = CollectionViewSource.GetDefaultView(HostsList.Cast<HostModel>().OrderBy(host => host.Client.Gender).OrderBy(h => h.Client.IsGuide));
        //                    break;
        //                case 3:
        //                    HostsList = CollectionViewSource.GetDefaultView(HostsList.Cast<HostModel>().OrderBy(host => host.Client.Feed).OrderBy(h => h.Client.IsGuide));
        //                    break;
        //                case 4:
        //                    HostsList = CollectionViewSource.GetDefaultView(HostsList.Cast<HostModel>().OrderBy(host => host.FullPrice).OrderBy(h => h.Client.IsGuide));
        //                    break;
        //                case 5:
        //                    HostsList = CollectionViewSource.GetDefaultView(HostsList.Cast<HostModel>().OrderBy(host => host.PaidPrice).OrderBy(h => h.Client.IsGuide));
        //                    break;
        //                case 6:
        //                    HostsList = CollectionViewSource.GetDefaultView(HostsList.Cast<HostModel>().OrderBy(host => host.RemainingPrice).OrderBy(h => h.Client.IsGuide));
        //                    break;
        //                case 7:
        //                    HostsList = CollectionViewSource.GetDefaultView(HostsList.Cast<HostModel>().OrderBy(host => host.HotelRoom.Room.Type).OrderBy(h => h.Client.IsGuide));
        //                    break;
        //                case 8:
        //                    HostsList = CollectionViewSource.GetDefaultView(HostsList.Cast<HostModel>().OrderBy(host => host.HotelRoom.FlightHotel.Hotel.Name).OrderBy(h => h.Client.IsGuide));
        //                    break;
        //                default:
        //                    HostsList = CollectionViewSource.GetDefaultView(HostsList.Cast<HostModel>().OrderBy(host => host.Id).OrderBy(h => h.Client.IsGuide));
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            Growl.Warning("لم تقم باختيار رحلة بعد");
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //        LogService.LogError(e.Message, this);
        //        Growl.Error("An error occurred while trying to InitData in flight");
        //    }
        //    finally
        //    {
        //        ShowingClients = false;
        //    }

        //}

        public void Dispose()
        {
           
        }
        #endregion
    }
}
