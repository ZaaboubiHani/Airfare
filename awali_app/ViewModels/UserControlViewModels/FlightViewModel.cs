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

        private bool _FlightClientsLoadingLine;

        public bool FlightClientsLoadingLine
        {
            get { return _FlightClientsLoadingLine; }
            set
            {
                _FlightClientsLoadingLine = value;
                OnPropertyChanged(nameof(FlightClientsLoadingLine));
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
                if (SelectedFlight != null)
                {
                    ShowClients();
                }
         

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

      
        private ICollectionView _SelectedHostsList;

        public ICollectionView SelectedHostsList
        {
            get { return _SelectedHostsList; }
            set
            {
                _SelectedHostsList = value;
                OnPropertyChanged(nameof(SelectedHostsList));
            }
        }

        private ObservableCollection<FlightModel> _SelectedFlights;

        public ObservableCollection<FlightModel> SelectedFlights
        {
            get { return _SelectedFlights; }
            set
            {
                _SelectedFlights = value;
                OnPropertyChanged(nameof(SelectedFlights));
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
                if (_SelectedMonth != null)
                {
                    SelectedFlights.Clear();
                    SelectedFlights = new(FlightsList.Where(f => f.DepartDate.ToString("MMMM", new CultureInfo("ar-DZ")) == SelectedMonth));
                }
                else
                {
                    SelectedFlights.Clear();
                }
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
                if (SelectedFlight != null)
                {
                    FlightClientsLoadingLine = true;
                    switch (_OrderMethod)
                    {
                        case 0:
                            SelectedHostsList = CollectionViewSource.GetDefaultView(SelectedHostsList.Cast<HostModel>().OrderBy(host => host.Client.LastName).OrderBy(h => h.Client.IsGuide));
                           
                            break;
                        case 1:
                            SelectedHostsList = CollectionViewSource.GetDefaultView(SelectedHostsList.Cast<HostModel>().OrderBy(host => host.Client.FirstName).OrderBy(h => h.Client.IsGuide));
                          
                            break;
                        case 2:
                            SelectedHostsList = CollectionViewSource.GetDefaultView(SelectedHostsList.Cast<HostModel>().OrderBy(host => host.Client.Gender).OrderBy(h => h.Client.IsGuide));
                          
                            break;
                        case 3:
                            SelectedHostsList = CollectionViewSource.GetDefaultView(SelectedHostsList.Cast<HostModel>().OrderBy(host => host.Client.Feed).OrderBy(h => h.Client.IsGuide));
                          
                            break;
                        case 4:
                            SelectedHostsList = CollectionViewSource.GetDefaultView(SelectedHostsList.Cast<HostModel>().OrderBy(host => host.FullPrice).OrderBy(h => h.Client.IsGuide));
                          
                            break;
                        case 5:
                            SelectedHostsList = CollectionViewSource.GetDefaultView(SelectedHostsList.Cast<HostModel>().OrderBy(host => host.PaidPrice).OrderBy(h => h.Client.IsGuide));
                           
                            break;
                        case 6:
                            SelectedHostsList = CollectionViewSource.GetDefaultView(SelectedHostsList.Cast<HostModel>().OrderBy(host => host.RemainingPrice).OrderBy(h => h.Client.IsGuide));
                         
                            break;
                        case 7:
                            SelectedHostsList = CollectionViewSource.GetDefaultView(SelectedHostsList.Cast<HostModel>().OrderBy(host => host.HotelRoom.Room.Type).OrderBy(h => h.Client.IsGuide));
                          
                            break;
                        case 8:
                            SelectedHostsList = CollectionViewSource.GetDefaultView(SelectedHostsList.Cast<HostModel>().OrderBy(host => host.HotelRoom.FlightHotel.Hotel.Name).OrderBy(h => h.Client.IsGuide));
                          
                           
                            break;
                        default:
                            
                            SelectedHostsList = CollectionViewSource.GetDefaultView(SelectedHostsList.Cast<HostModel>().OrderBy(host => host.Id).OrderBy(h => h.Client.IsGuide));
                           
                            
                            break;
                    }
                    FlightClientsLoadingLine = false;
                }
                else
                {
                    Growl.Warning("لم تقم باختيار رحلة بعد");
                }

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
                if (!loadinState)
                    if (SelectedFlight != null)
                    {
                        ShowClients();
                    }
                    else
                    {
                        Growl.Warning("لم تقم باختيار رحلة بعد");
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
        public FlightModel[] FlightsList { get; set; }
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
        public RelayCommand ShowClientDialogCommand { get; set; }
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
            InitData();
        }

        #region Methods
        private void InitCommands()
        {
            try
            {
                ShowHotelDialogCommand = new RelayCommand(ShowHotelDialog);
                ShowClientDialogCommand = new RelayCommand(ShowClientDialog);
                ShowRoomDialogCommand = new RelayCommand(ShowRoomDialog);
                AddFlightCommand = new RelayCommand(AddFlight);
                ShowFlightDialogCommand = new RelayCommand(ShowFlightDialog);
                ExportExcelDataCommand = new RelayCommand(ExportExcelData);
                ExportWordDataCommand = new RelayCommand(ExportWordData);
                RemoveHostCommand = new RelayCommand(RemoveHost);
                RemoveFlightCommand = new RelayCommand(RemoveFlight);
                RemoveHotelCommand = new RelayCommand(RemoveHotel);
                UpdateHotelCommand = new RelayCommand(UpdateHotel);
                ImportExcelDataCommand = new RelayCommand(ImportExcelData);
                ExportClientPaymentCommand = new RelayCommand(ExportClientPayment);
            }
            catch (Exception)
            {
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
            catch (Exception)
            {
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
            catch (Exception)
            {
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
                        vm.Hotel = SelectedHotel.Copy();
                       

                    }).GetResultAsync<HotelModel>();
                    if (res != null)
                    {
                        SelectedHotel = res;
                    }
                }
                UpdateHotelLoadingCircle = false;
            }
            catch (Exception)
            {
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
                        await hotelServices.RemoveHotel(SelectedHotel);
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
            catch (Exception)
            {
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
                    await flightServices.RemoveFlight(SelectedFlight);
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
            catch (Exception)
            {
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
                    await hostServices.RemoveHost(SelectedHost);
                    if (!hostServices.Error)
                    {
                        ShowClients();
                        Growl.Success("تم حذف المعتمر بنجاح");
                    }
                    else
                    {
                        Growl.Error("\n:خطأ\n" + hostServices.ErrorMessage);
                    }
                }
            }
            catch (Exception)
            {

                Growl.Error("An error occurred while trying to remove this client");
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
            catch (Exception)
            {
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


                    await exportationServices.ExportFlightExcelData(SelectedHostsList.Cast<HostModel>().ToList(), SelectedFlight, HostsTotal, AdultHostsTotal, INFHostsTotal, CHDHostsTotal);
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
            
                var flighthotels = await flightHotelServices.GetFlightHotelsOfFlight(SelectedFlight.Id);
                var hotelsRooms = await hotelRoomServices.GetHotelsRoomsOfFlight(SelectedFlight.Id);
                var companies = await companyServices.GetAllCompanies();
                var res = await Dialog.Show<ClientDialog>().Initialize<ClientDialogViewModel>(vm =>
                {
                    vm.UpdateMode = true;
                    vm.Client = SelectedHost.Client;
                    vm.Host = SelectedHost;
                    vm.PhonesList = new ObservableCollection<PhoneModel>(SelectedHost.Client.Phones ?? new());
                    vm.PaymentsList = new ObservableCollection<PaymentModel>(SelectedHost.Payments ?? new());
                    vm.Feed = SelectedHost.Client.Feed;
                    vm.IsGuide = SelectedHost.Client.IsGuide;
                    vm.RemainingPrice = SelectedHost.RemainingPrice;
                    vm.Discount = SelectedHost.Discount;
                    vm.PaidPrice = SelectedHost.PaidPrice;
                    vm.Rooms = Rooms.ToList();
                    vm.Companies = companies;
                    vm.SelectedCompany = SelectedHost.Company;
                    vm.hiddenCompanySelector = SelectedHost.Company;
                    vm.SelectedRoom = SelectedHost.HotelRoom.Room;
                    vm.HotelsRooms = hotelsRooms;
                    vm.FlightHotels = flighthotels;
                    vm.SelectedFlightHotel = SelectedHost.HotelRoom.FlightHotel;
                }).GetResultAsync<HostModel>();
                if (res != null)
                {
                    SelectedHost = res;
                    if (string.IsNullOrEmpty(SelectedHostsList.Cast<HostModel>().Where(h => h.Id == res.Id).FirstOrDefault().Client.Color))
                        SelectedHostsList.Cast<HostModel>().Where(h => h.Id == res.Id).FirstOrDefault().Client.Color = SelectedHostsList.Cast<HostModel>().Where(h => h.Id == res.Id).FirstOrDefault().HotelRoom.Room.Color;

                    ShowClients();
                    OnPropertyChanged(nameof(SelectedHostsList));
                }
            
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while trying to update the client");
                
            }
            
        }

        private async void ShowClients()
        {
            try
            {
                await Task.Run( async() =>
                {
                    ShowingClients = true;
                    if (SelectedFlight != null)
                    {
                        FlightClientsLoadingLine = true;
                        FlightFuntionsButonsVisibility = true;

                        FullPriceTotal = 0;
                        DiscountTotal = 0;
                        PaidPriceTotal = 0;
                        RemainingPriceTotal = 0;
                        HostsTotal = 0;
                        HostsTotal = 0;
                        INFHostsTotal = 0;
                        CHDHostsTotal = 0;
                        AdultHostsTotal = 0;

                        var hotelRoomModels = SelectedFlightHotelRooms.Where(sfhr => sfhr.FlightHotel.FlightId == SelectedFlight.Id).ToList();
                        var hostsList = await hostServices.GetAllHostsOfFlight(SelectedFlight.Id);

                        SelectedHostsList = CollectionViewSource.GetDefaultView(hostsList.OrderBy(h=>h.Id).OrderBy(h=>h.Client.IsGuide));
                        if (Filters.Count > 0)
                        {
                            SelectedHostsList.Filter = SelectedHostsListFilter;
                            SelectedHostsList.Refresh();

                        }
                        
                        for (int i = 0; i < SelectedHostsList.Cast<HostModel>().ToList().Count; i++)
                        {

                            if (string.IsNullOrEmpty(SelectedHostsList.Cast<HostModel>().ToList()[i].Client.Color))
                            {
                                SelectedHostsList.Cast<HostModel>().ToList()[i].Client.Color = SelectedHostsList.Cast<HostModel>().ToList()[i].HotelRoom.Room.Color;
                            }

                            if (!SelectedHostsList.Cast<HostModel>().ToList()[i].Client.IsGuide)
                            {
                                FullPriceTotal += SelectedHostsList.Cast<HostModel>().ToList()[i].FullPrice;
                                DiscountTotal += SelectedHostsList.Cast<HostModel>().ToList()[i].Discount;
                                PaidPriceTotal += SelectedHostsList.Cast<HostModel>().ToList()[i].PaidPrice;
                                RemainingPriceTotal += SelectedHostsList.Cast<HostModel>().ToList()[i].RemainingPrice;
                            }
                            HostsTotal++;
                            try
                            {

                                DateTime zeroTime = new DateTime(1, 1, 1);
                                TimeSpan span = SelectedFlight.DepartDate - SelectedHostsList.Cast<HostModel>().ToList()[i].Client.BirthDate;
                                int years = (zeroTime + span).Year - 1;


                                if (years > 12)
                                {
                                    AdultHostsTotal++;
                                }
                                else if (years > 2)
                                {
                                    CHDHostsTotal++;
                                }
                                else
                                {
                                    INFHostsTotal++;
                                }
                            }
                            catch (Exception e)
                            {
                                Growl.Error("تاريخ ميلاد المعتمر رقم "+(i+1)+" غير معتاد");
                            }

                        }

                        FlightClientsLoadingLine = false;
                    }
                    else
                    {
                        SelectedHostsList = CollectionViewSource.GetDefaultView(new List<HostModel>());

                        FlightFuntionsButonsVisibility = false;
                        FullPriceTotal = 0;
                        DiscountTotal = 0;
                        PaidPriceTotal = 0;
                        RemainingPriceTotal = 0;
                        HostsTotal = 0;
                        HostsTotal = 0;
                        INFHostsTotal = 0;
                        CHDHostsTotal = 0;
                        AdultHostsTotal = 0;
                    }
                    ShowingClients = false;
                });
               
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while trying to execute ShowClients Function");
                
            }
           
        }

        private bool RoomFilter(HostModel host)
        {
            var roomsTypes = Rooms.Select(r => r.Type).ToArray();
            bool roomFilterExist = roomsTypes.Any(type => Filters.Contains("نوع الغرفة : " + type));


            if (roomFilterExist)
            {
                for (int i = 0; i < Rooms.Length;i++)
                {
                    if (Filters.Contains("نوع الغرفة : " + Rooms[i].Type))
                    {
                        if (host.HotelRoom.RoomId == Rooms[i].Id)
                            return true;
                    }
                }
            }
            else
            {
                return true;
            }
            return false;
        }

        private bool FeedFilter(HostModel host)
        {
            bool feedFilterExist = false;

            if (Filters.Contains("الاعاشة : موجودة") || Filters.Contains("الاعاشة : غير موجودة"))
                feedFilterExist = true;
            if (feedFilterExist)
            {
                if (Filters.Contains("الاعاشة : موجودة"))
                {
                    if (host.Client.Feed)
                        return true;
                }
                else
                {
                    if (!host.Client.Feed)
                        return true;
                }
            }
            else
            {
                return true;
            }
            return false;
        }

        private bool PaidFilter(HostModel host)
        {
            bool paidFilterExist = false;

            if (Filters.Contains("الدفع : مكتمل") || Filters.Contains("الدفع : غير مكتمل"))
                paidFilterExist = true;
            if (paidFilterExist)
            {
                if (Filters.Contains("الدفع : مكتمل"))
                {
                    if (host.IsPaid)
                        return true;
                }
                else
                {
                    if (!host.IsPaid)
                        return true;
                }
            }
            else
            {
                return true;
            }
            return false;
        }

        private bool GenderFilter(HostModel host)
        {
            bool genderFilterExist = false;

            if (Filters.Contains("الجنس : ذكر") || Filters.Contains("الجنس : أنثى"))
                genderFilterExist = true;

            if (genderFilterExist)
            {
                if (Filters.Contains("الجنس : ذكر"))
                {
                    if (!host.Client.Gender)
                        return true;
                }
                else
                {
                    if (host.Client.Gender)
                        return true;
                }
            }
            else
            {
                return true;
            }
            return false;
        }

        private bool HotelFilter(HostModel host)
        {
            var hotelsNames = HotelsList.Select(h => h.Name).ToArray();
            bool hotelFilterExist = hotelsNames.Any(name=> Filters.Contains("نوع الفندق : " + name));

            if (hotelFilterExist)
            {
                for (int i = 0; i < HotelsList.Count; i++)
                {
                    if (Filters.Contains("نوع الفندق : " + HotelsList[i].Name))
                    {
                        if (host.HotelRoom.FlightHotel.HotelId == HotelsList[i].Id)
                            return true;
                    }
                }
            }
            else
            {
                return true;
            }
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
                UpdateFlightLoadingCircle = false;
            }
            catch (Exception)
            {
                UpdateFlightLoadingCircle = false;
                Growl.Error("An error occurred while trying to display the flight dialog");
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

                AddFlightLoadingCircle = false;
            }
            catch (Exception)
            {
                AddFlightLoadingCircle = false;
                Growl.Error("An error occurred while trying to add a flight");
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
                AddRoomLoadingCircle = false;
            }
            catch (Exception)
            {
                AddRoomLoadingCircle = false;
                Growl.Error("An error occurred while trying to display the room dialog");
            }
           
        }

        private async void ShowClientDialog()
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
                            var hostslist = SelectedHostsList.Cast<HostModel>().ToList();
                            hostslist.Add(res);
                            ShowClients();
                            if (string.IsNullOrEmpty(hostslist.Where(h => h.Id == res.Id).FirstOrDefault().Client.Color))
                                hostslist.Where(h => h.Id == res.Id).FirstOrDefault().Client.Color = hostslist.Where(h => h.Id == res.Id).FirstOrDefault().HotelRoom.Room.Color;

                            Filters.Clear();
                            SelectedHostsList = CollectionViewSource.GetDefaultView(hostslist);
                            OnPropertyChanged(nameof(SelectedHostsList));
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
                AddClientLoadingCircle = false;
            }
            catch (Exception)
            {
                AddClientLoadingCircle = false;
                Growl.Error("An error occurred while trying to display the client dialog");
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
                AddHotelLoadingCircle = false;
            }
            catch (Exception)
            {
                AddHotelLoadingCircle = false;
                Growl.Error("An error occurred while tring to display the hotel dialog");
            }
            
        }

        private async void InitData()
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
                    var seasons = await seasonServices.GetAllSeasons();
                    Configuration.CurrentSeason = seasons.Where(s => !s.HasEnded).ToList().FirstOrDefault();
                }
                // init flight
                Flight = new() { SeasonId = Configuration.CurrentSeason.Id, DepartDate = DateTime.Now, ReturntDate = DateTime.Now, DepartTime = DateTime.Now.TimeOfDay, ReturnTime = DateTime.Now.TimeOfDay };

                // loading all the available hotels
                HotelsList = new ObservableCollection<HotelModel>(await hotelServices.GetAllHotels());

                // loading all the flights
                FlightsList = (await flightServices.GetAllFlightsOfSeason(Configuration.CurrentSeason.Id)).ToArray();

                // loading all the months
                Months = new(FlightsList.Select(f => f.DepartDate.ToString("MMMM", new CultureInfo("ar-DZ"))).Distinct());
              
                // loading rooms
                Rooms = (await roomServices.GetAllRooms()).ToArray();

                // loading the available filters from hotels and rooms
                AvailableFilters.AddRange(HotelsList.Select(h => "نوع الفندق : " + h.Name));
                AvailableFilters.AddRange(Rooms.Select(r => "نوع الغرفة : " + r.Type));
                // loading the hotel rooms
                SelectedFlightHotelRooms = await hotelRoomServices.GetAllHotelsRooms();

                // assgining the rest of the properties
                SelectedFlights = new ObservableCollection<FlightModel>();
                SelectedFlightHotelsList = new ObservableCollection<FlightHotelModel>();
                HotelsRooms = new List<HotelRoomModel>();
                DisplayedHotelsRooms = new ObservableCollection<HotelRoomModel>();
                SelectedHostsList = CollectionViewSource.GetDefaultView(new List<HostModel>());
                SelectedHostsList.Filter = SelectedHostsListFilter;
                Filters = new();
                
                HotelsLoadingLine = false;
                loadinState = false;
                MonthsLoadingLine = false;
            }
            catch (Exception)
            {
                HotelsLoadingLine = false;
                loadinState = false;
                MonthsLoadingLine = false;
                Growl.Error("An error occurred while trying to InitData in flight"); 
            }
    
        }

        private bool SelectedHostsListFilter(object obj)
        {
            if(obj is HostModel host)
            {
                return (GenderFilter(host) && FeedFilter(host) && RoomFilter(host) && HotelFilter(host) && PaidFilter(host)); 
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
