using Airfare.Commands;
using Airfare.Models;
using Airfare.Servies;
using Airfare.DataContext;
using HandyControl.Controls;
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
    public class HotelViewModel:BaseViewModel, IDisposable
    {
        #region Properties
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
                    for(int i = 0; i < FlightsList.Length;i ++)
                    {
                        string month = FlightsList[i].DepartDate.ToString("MMMM", new CultureInfo("ar-DZ"));
                        if (_SelectedMonth == month)
                        {
                            SelectedFlights.Add(FlightsList[i]);
                        }
                    }
                }
                else
                {
                    SelectedFlights.Clear();
                }
            }
        }

        private FlightModel _SelectedFlight;

        public FlightModel SelectedFlight
        {
            get { return _SelectedFlight; }
            set { _SelectedFlight = value;
                OnPropertyChanged(nameof(SelectedFlight));
                GetAllSpots();
            }
        }

        private HotelModel _SelectedHotel;

        public HotelModel SelectedHotel
        {
            get { return _SelectedHotel; }
            set { _SelectedHotel = value;
                OnPropertyChanged(nameof(SelectedHotel));
                if(SelectedHotel != null)
                {
                    SpotsAndGroupsCollectionContainer.Clear();
                    SelectedHostsWithoutSpots = new ObservableCollection<HostModel>(HostsList.Where(h => h.HotelRoom.FlightHotel.HotelId == SelectedHotel.Id && h.HotelRoom.FlightHotel.FlightId == SelectedFlight.Id && h.SpotId==null && h.Spot == null).OrderBy(h=>h.Id));
                    DisplayedSpots = new ObservableCollection<SpotModel>(HostsList.Where(h => (h.HotelRoom.FlightHotel.HotelId == SelectedHotel.Id && h.HotelRoom.FlightHotel.FlightId == SelectedFlight.Id && h.Spot != null && h.Spot.Group == null)).Select(h => h.Spot).Distinct());
                    DisplayedGroups = new ObservableCollection<GroupModel>(HostsList.Where(h => (h.HotelRoom.FlightHotel.HotelId == SelectedHotel.Id && h.HotelRoom.FlightHotel.FlightId == SelectedFlight.Id && h.Spot != null && h.Spot.Group != null)).Select(h => h.Spot.Group).Distinct());
                    SpotsAndGroupsCollectionContainer.Clear();
                    SpotsAndGroupsCollectionContainer.Add(new CollectionContainer() { Collection = DisplayedSpots });
                    SpotsAndGroupsCollectionContainer.Add(new CollectionContainer() { Collection = DisplayedGroups });
                    var allSpots = HostsList.Where(h => (h.HotelRoom.FlightHotel.HotelId == SelectedHotel.Id && h.HotelRoom.FlightHotel.FlightId == SelectedFlight.Id && h.Spot != null)).Select(h => h.Spot).Distinct().ToList();
                    RoomsClientsNumbersList = new(new int[RoomsList.Count]);
                    RoomsNumbersList = new(new int[RoomsList.Count]);
                    for (int i = 0; i < RoomsList.Count; i++)
                    {
                        RoomsNumbersList[i] = allSpots.Where(s => s.Capacity == RoomsList[i].Capacity).Count();
                        RoomsClientsNumbersList[i] = allSpots.Where(s => s.Capacity == RoomsList[i].Capacity).Select(s => s.Hosts.Count).Sum();
                    }
                    TotalSpots = RoomsNumbersList.Sum();
                    TotalHosts = RoomsClientsNumbersList.Sum();
                }
                else
                {
                    SelectedHostsWithoutSpots.Clear();
                    DisplayedSpots.Clear();
                }
                
            }
        }

        private ObservableCollection<HotelModel> _SelectedHotels;

        public ObservableCollection<HotelModel> SelectedHotels
        {
            get { return _SelectedHotels; }
            set { _SelectedHotels = value;
                OnPropertyChanged(nameof(SelectedHotels));
            }
        }

        private ObservableCollection<HostModel> _SelectedHostsWithoutSpots;

        public ObservableCollection<HostModel> SelectedHostsWithoutSpots
        {
            get { return _SelectedHostsWithoutSpots; }
            set
            {
                _SelectedHostsWithoutSpots = value;
                OnPropertyChanged(nameof(SelectedHostsWithoutSpots));
            }
        }

        private ObservableCollection<SpotModel> _DisplayedSpots;

        public ObservableCollection<SpotModel> DisplayedSpots
        {
            get { return _DisplayedSpots; }
            set
            {
                _DisplayedSpots = value;
                OnPropertyChanged(nameof(DisplayedSpots));
            }
        }

        private ObservableCollection<GroupModel> _DisplayedGroups;

        public ObservableCollection<GroupModel> DisplayedGroups
        {
            get { return _DisplayedGroups; }
            set
            {
                _DisplayedGroups = value;
                OnPropertyChanged(nameof(DisplayedGroups));
            }
        }

        private CompositeCollection _SpotsAndGroupsCollectionContainer;

        public CompositeCollection SpotsAndGroupsCollectionContainer
        {
            get { return _SpotsAndGroupsCollectionContainer; }
            set {
                _SpotsAndGroupsCollectionContainer = value;
                OnPropertyChanged(nameof(SpotsAndGroupsCollectionContainer));
            }
        }


        private List<RoomModel> _RoomsList;

        public List<RoomModel> RoomsList
        {
            get { return _RoomsList; }
            set { _RoomsList = value;
                OnPropertyChanged(nameof(RoomsList));
            }
        }

        private RoomModel _SelectedRoom;

        public RoomModel SelectedRoom
        {
            get { return _SelectedRoom; }
            set { _SelectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }

        private int _SpotNumber;

        public int SpotNumber
        {
            get { return _SpotNumber; }
            set { _SpotNumber = value;
                OnPropertyChanged(nameof(SpotNumber));
            }
        }

        private ObservableCollection<int> _RoomsNumbersList;

        public ObservableCollection<int> RoomsNumbersList
        {
            get { return _RoomsNumbersList; }
            set { _RoomsNumbersList = value;
                OnPropertyChanged(nameof(RoomsNumbersList));
            }
        }

        private ObservableCollection<int> _RoomsClientsNumbersList;

        public ObservableCollection<int> RoomsClientsNumbersList
        {
            get { return _RoomsClientsNumbersList; }
            set
            {
                _RoomsClientsNumbersList = value;
                OnPropertyChanged(nameof(RoomsClientsNumbersList));
            }
        }

        private int _TotalSpots;

        public int TotalSpots
        {
            get { return _TotalSpots; }
            set { _TotalSpots = value;
                OnPropertyChanged(nameof(TotalSpots));
            }
        }

        private int _TotalHosts;

        public int TotalHosts
        {
            get { return _TotalHosts; }
            set { _TotalHosts = value;
                OnPropertyChanged(nameof(TotalHosts));
            }
        }

        private bool _SpotsAvailable;

        public bool SpotsAvailable
        {
            get { return _SpotsAvailable; }
            set { _SpotsAvailable = value;
                OnPropertyChanged(nameof(SpotsAvailable));
            }
        }

        private bool _GroupAvailable;

        public bool GroupAvailable
        {
            get { return _GroupAvailable; }
            set
            {
                _GroupAvailable = value;
                OnPropertyChanged(nameof(GroupAvailable));
            }
        }

        private HostModel _SelectedHost;

        public HostModel SelectedHost
        {
            get { return _SelectedHost; }
            set { _SelectedHost = value;
                OnPropertyChanged(nameof(SelectedHost));
            }
        }

        private bool _LoadingData;

        public bool LoadingData
        {
            get { return _LoadingData; }
            set { _LoadingData = value;
                OnPropertyChanged(nameof(LoadingData));
            }
        }

        private bool _SavingData;

        public bool SavingData
        {
            get { return _SavingData; }
            set { _SavingData = value;
                OnPropertyChanged(nameof(SavingData));
            }
        }


        public bool changesSaved { get; set; }
        public List<HostModel> HostsList { get; set; }
        public List<HostModel> HostsListToRemove { get; set; }
        public List<GroupModel> GroupsListToRemove { get; set; }
        public List<SpotModel> SpotsListToRemove { get; set; }
        public FlightHotelModel[] FlightHotelsList { get; set; }
        public ObservableCollection<string> Months { get; set; }
        public FlightModel[] FlightsList { get; set; }
        private FlightModel oldSelectedFlight { get; set; }
        #endregion

        #region Services
        private readonly RoomServices roomServices = new();
        private readonly FlightServices flightServices = new();
        private readonly FlightHotelServices flightHotelServices = new();
        private readonly HostServices hostServices = new();
        private readonly SeasonServices seasonServices = new();
        private readonly GroupServices groupServices = new();
        private readonly SpotServices spotServices = new();
        private readonly ExportationServices exportationServices = new();

        #endregion

        #region Commands
        public RelayCommand SaveSpotCommand { get; set; }
        #endregion

        public HotelViewModel()
        {
            InitData();
            SaveSpotCommand = new RelayCommand(SaveSpot);
        }

        #region Methods
        public async void ExportExcelData()
        {
            try
            {
                SavingData = true;
                List<SpotModel> spots = new();
                List<GroupModel> groups = new();
               
                spots.AddRange(HostsList.Where(h => (h.HotelRoom.FlightHotel.HotelId == SelectedHotel.Id && h.HotelRoom.FlightHotel.FlightId == SelectedFlight.Id && h.Spot != null && h.Spot.Group == null)).Select(h => h.Spot).Distinct());
                groups.AddRange(HostsList.Where(h => (h.HotelRoom.FlightHotel.HotelId == SelectedHotel.Id && h.HotelRoom.FlightHotel.FlightId == SelectedFlight.Id && h.Spot != null && h.Spot.Group != null)).Select(h => h.Spot.Group).Distinct());
                
                await exportationServices.ExportReservationExcelData(spots, groups,RoomsList,RoomsNumbersList.ToList(),RoomsClientsNumbersList.ToList(),SelectedHotel,SelectedFlight);
                SavingData = false;
            }
            catch (Exception)
            {
                SavingData = false;
                Growl.Error("An error occurred while trying to export reservatin excel data");
            }
        }
        private void GetAllSpots()
        {

            //Growl.Ask("لم تقم بحفظ آخر تعديلات, هل تريد الاستمرار بدون حفظ؟", isConfirmed =>
            //{
            //    if (isConfirmed)
            //    {
            //        if (_SelectedFlight != null)
            //        {
            //            //oldSelectedFlight = _SelectedFlight;
            //            //notSaved = false;
            //            //var list = FlightHotelsList.Where(fh => fh.FlightId == _SelectedFlight.Id).ToList();
            //            //SelectedHotels.Clear();
            //            //Spots.Clear();
            //            //SelectedHostsWithoutSpots.Clear();
            //            //list.ForEach(item =>
            //            //{
            //            //    SelectedHotels.Add(item.Hotel);

            //            //});
            //            //var hostListOfFlight = HostsList.Where(h => h.HotelRoom.FlightHotel.FlightId == SelectedFlight.Id && h.Spot != null).ToList();
            //            //hostListOfFlight.ForEach(h =>
            //            //{
            //            //    SpotModel spot = new SpotModel
            //            //    {
            //            //        Number = (int)h.Spot,
            //            //        Capacity = h.HotelRoom.Room.Capacity,
            //            //        Color = h.HotelRoom.Room.Color,
            //            //        Taken = 1,
            //            //        IsEmpty = true,
            //            //        Hosts = CollectionViewSource.GetDefaultView(new List<HostModel> { h })
            //            //    };
            //            //    if (spot.Capacity == spot.Taken)
            //            //        spot.IsEmpty = false;
            //            //    if (Spots.Any(s => s.Number == spot.Number))
            //            //    {
            //            //        var spotmodel = Spots.Where(s => s.Number == spot.Number).ToList().FirstOrDefault();
            //            //        spotmodel.Taken++;
            //            //        if (spotmodel.Capacity == spotmodel.Taken)
            //            //            spotmodel.IsEmpty = false;
            //            //        var hostmodels = spotmodel.Hosts.Cast<HostModel>().ToList();
            //            //        hostmodels.Add(h);
            //            //        spotmodel.Hosts = CollectionViewSource.GetDefaultView(hostmodels);
            //            //    }
            //            //    else
            //            //    {
            //            //        Spots.Add(spot);
            //            //    }
            //            //});

            //        }
            //        else
            //        {
            //            SelectedHotels.Clear();
            //            DisplayedSpots.Clear();
            //            SelectedHostsWithoutSpots.Clear();
            //        }
            //    }
            //    else
            //    {
            //        _SelectedFlight = oldSelectedFlight;
            //        OnPropertyChanged(nameof(SelectedFlight));
            //    }
            //    return true;
            //});


            try
            {
                if (_SelectedFlight != null)
                {
                    oldSelectedFlight = _SelectedFlight;

                    SelectedHotels = new ObservableCollection<HotelModel>(FlightHotelsList.Where(fh => fh.FlightId == _SelectedFlight.Id).Select(fh => fh.Hotel));
                    SelectedHotel = null;
                    DisplayedSpots.Clear();
                    SelectedHostsWithoutSpots.Clear();

                }
                else
                {
                    SelectedHotel = null;
                    SelectedHotels.Clear();
                    DisplayedSpots.Clear();
                    SelectedHostsWithoutSpots.Clear();
                }
            }
            catch (Exception)
            {

                Growl.Error("An error occurred while trying to Get All Spots");
            }
            
        }
        private async void SaveSpot()
        {
            try
            {
                if(SelectedFlight != null && SelectedHotel != null)
                {
                    SavingData = true;
                    changesSaved = false;
                    List<SpotModel> spots = new();
                    List<GroupModel> groups = new();
                    for (int i = 0; i < SelectedHotels.Count; i++)
                    {
                        spots.AddRange(HostsList.Where(h => (h.HotelRoom.FlightHotel.HotelId == SelectedHotels[i].Id && h.HotelRoom.FlightHotel.FlightId == SelectedFlight.Id && h.Spot != null && h.Spot.Group == null)).Select(h => h.Spot).Distinct());
                        groups.AddRange(HostsList.Where(h => (h.HotelRoom.FlightHotel.HotelId == SelectedHotels[i].Id && h.HotelRoom.FlightHotel.FlightId == SelectedFlight.Id && h.Spot != null && h.Spot.Group != null)).Select(h => h.Spot.Group).Distinct());
                    }
                    for (int i = 0; i < spots.Count; i++)
                    {
                        if (spots[i].Id == 0)
                        {
                            var hosts = spots[i].Hosts;
                            spots[i].Hosts = null;
                            spots[i] = await spotServices.AddSpot(spots[i]);
                            for (int j = 0; j < hosts.Count; j++)
                            {
                                hosts[j].SpotId = spots[i].Id;
                                hosts[j].Client = null;
                                hosts[j].Company = null;
                                hosts[j].Payments = null;
                                hosts[j].HotelRoom = null;
                                hosts[j].Spot = null;
                                await hostServices.UpdateHost(hosts[j]);
                            }
                        }
                        else
                        {
                            var hosts = spots[i].Hosts;
                            spots[i].Hosts = null;
                            await spotServices.UpdateSpot(spots[i]);
                            if (hosts.Count > 0)
                            {
                                for (int j = 0; j < hosts.Count; j++)
                                {
                                    hosts[j].SpotId = spots[i].Id;
                                    hosts[j].Client = null;
                                    hosts[j].Company = null;
                                    hosts[j].Payments = null;
                                    hosts[j].HotelRoom = null;
                                    hosts[j].Spot = null;
                                    await hostServices.UpdateHost(hosts[j]);
                                }
                            }
                            else
                            {
                                await spotServices.RemoveSpot(spots[i]);
                            }

                        }
                    }
                    for (int i = 0; i < groups.Count; i++)
                    {
                        if (groups[i].Id == 0)
                        {
                            var spotsList = groups[i].Spots;
                            groups[i].Spots = null;
                            await groupServices.AddGroup(groups[i]);
                            for (int j = 0; j < spotsList.Count; j++)
                            {
                                spotsList[j].GroupId = groups[i].Id;
                                if (spotsList[j].Id == 0)
                                {
                                    var hosts = spotsList[j].Hosts;
                                    spotsList[j].Hosts = null;
                                    spotsList[j].Group = null;
                                    spotsList[j] = await spotServices.AddSpot(spotsList[j]);
                                    for (int z = 0; z < hosts.Count; z++)
                                    {
                                        hosts[z].SpotId = spotsList[j].Id;
                                        hosts[z].Client = null;
                                        hosts[z].Company = null;
                                        hosts[z].Payments = null;
                                        hosts[z].HotelRoom = null;
                                        hosts[z].Spot = null;
                                        await hostServices.UpdateHost(hosts[z]);
                                    }
                                }
                                else
                                {
                                    var hosts = spotsList[j].Hosts;
                                    spotsList[j].Hosts = null;
                                    spotsList[j].Group = null;
                                    await spotServices.UpdateSpot(spotsList[j]);
                                    for (int z = 0; z < hosts.Count; z++)
                                    {
                                        hosts[z].SpotId = spotsList[j].Id;
                                        hosts[z].Client = null;
                                        hosts[z].Company = null;
                                        hosts[z].Payments = null;
                                        hosts[z].HotelRoom = null;
                                        hosts[z].Spot = null;
                                        await hostServices.UpdateHost(hosts[z]);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (groups[i].Spots.Count < 2)
                            {
                                await groupServices.RemoveGroup(groups[i]);
                            }
                        }

                    }
                    for (int i = 0; i < HostsListToRemove.Count; i++)
                    {
                        HostsListToRemove[i].SpotId = null;
                        HostsListToRemove[i].Client = null;
                        HostsListToRemove[i].Company = null;
                        HostsListToRemove[i].Payments = null;
                        HostsListToRemove[i].HotelRoom = null;
                        HostsListToRemove[i].Spot = null;
                        await hostServices.UpdateHost(HostsListToRemove[i]);
                    }
                    for (int i = 0; i < GroupsListToRemove.Count; i++)
                    {
                        GroupsListToRemove[i].Spots = null;
                        await groupServices.RemoveGroup(GroupsListToRemove[i]);
                    }
                    for (int i = 0; i < SpotsListToRemove.Count; i++)
                    {
                        SpotsListToRemove[i].Group = null;
                        SpotsListToRemove[i].GroupId = null;
                        SpotsListToRemove[i].Hosts = null;
                        await spotServices.RemoveSpot(SpotsListToRemove[i]);
                    }
                    HostsListToRemove.Clear();
                    InitData();
                    SelectedMonth = null;
                    SavingData = false;
                    Growl.Success("تم الحفض");
                }
            }
            catch (Exception)
            {
                SavingData = false;
                Growl.Error("An error occurred while trying to save the spots");
            }
            
        }

        public void DropSelectedHost(SpotModel spot)
        {
            try
            {
                if (spot.Capacity == SelectedHost.HotelRoom.Room.Capacity)
                {
                    var spotHosts = spot.Hosts.Cast<HostModel>().ToList();
                    SelectedHost.Spot = spot;
                    SelectedHost.SpotId = spot.Id;
                    if (HostsListToRemove.Contains(SelectedHost))
                    {
                        HostsListToRemove.Remove(SelectedHost);
                    }

                    spotHosts.Add(SelectedHost);
                    spot.Hosts = spotHosts;
                    spot.Taken++;
                    if (spot.Capacity == spot.Taken)
                        spot.IsEmpty = false;
                    if (SpotsListToRemove.Contains(spot))
                    {
                        SpotsListToRemove.Remove(spot);
                    }
                    changesSaved = true;


                    SelectedHostsWithoutSpots.Remove(SelectedHost);
                    var allSpots = HostsList.Where(h => (h.HotelRoom.FlightHotel.HotelId == SelectedHotel.Id && h.HotelRoom.FlightHotel.FlightId == SelectedFlight.Id && h.Spot != null)).Select(h => h.Spot).Distinct().ToList();
                    RoomsClientsNumbersList = new(new int[RoomsList.Count]);
                    RoomsNumbersList = new(new int[RoomsList.Count]);
                    for (int i = 0; i < RoomsList.Count; i++)
                    {
                        RoomsNumbersList[i] = allSpots.Where(s => s.Capacity == RoomsList[i].Capacity).Count();
                        RoomsClientsNumbersList[i] = allSpots.Where(s => s.Capacity == RoomsList[i].Capacity).Select(s => s.Hosts.Count).Sum();
                    }
                    TotalSpots = RoomsNumbersList.Sum();
                    TotalHosts = RoomsClientsNumbersList.Sum();
                }
                else
                {
                    Growl.Error("يختلف نوع الغرفة الافتراضية عن نوع غرفة المعتمر");
                }
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while trying to drop the selected host");
            }

        }

        public void RemoveSelectedHostFromSpot(SpotModel spot,HostModel host)
        {
            try
            {
                if (host != null)
                {
                    HostsListToRemove.Add(host);
                    var tempHosts = spot.Hosts.Cast<HostModel>().ToList();
                    tempHosts.Remove(host);
                    spot.Hosts = tempHosts;
                    host.Spot = null;
                    host.SpotId = null;
                    spot.Taken--;
                    spot.IsEmpty = true;
                    if(spot.Taken == 0)
                    {
                        SpotsListToRemove.Add(spot);
                    }
                    SelectedHostsWithoutSpots.Add(host);
                    SelectedHostsWithoutSpots = new ObservableCollection<HostModel>(SelectedHostsWithoutSpots.OrderBy(h => h.Id));

                    var allSpots = HostsList.Where(h => (h.HotelRoom.FlightHotel.HotelId == SelectedHotel.Id && h.HotelRoom.FlightHotel.FlightId == SelectedFlight.Id && h.Spot != null)).Select(h => h.Spot).Distinct().ToList();
                    RoomsClientsNumbersList = new(new int[RoomsList.Count]);
                    RoomsNumbersList = new(new int[RoomsList.Count]);
                    for (int i = 0; i < RoomsList.Count; i++)
                    {
                        RoomsNumbersList[i] = allSpots.Where(s => s.Capacity == RoomsList[i].Capacity).Count();
                        RoomsClientsNumbersList[i] = allSpots.Where(s => s.Capacity == RoomsList[i].Capacity).Select(s => s.Hosts.Count).Sum();
                    }
                    TotalSpots = RoomsNumbersList.Sum();
                    TotalHosts = RoomsClientsNumbersList.Sum();
                }
                else
                {
                    Growl.Error("failed to intiate the selected host for this spot");
                }
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while trying to remove the host");
            }

        }

        public void AddSpot()
        {
            try
            {
                if (SelectedRoom != null)
                {
                    SpotNumber = 1;
                    while (HostsList.Where(h => (h.HotelRoom.FlightHotel.HotelId == SelectedHotel.Id && h.HotelRoom.FlightHotel.FlightId == SelectedFlight.Id && h.Spot != null)).Select(h => h.Spot.Number).Distinct().Any(n=>n==SpotNumber) || DisplayedSpots.Any(s=>s.Number == SpotNumber))
                    {
                        SpotNumber++;
                    }
                    DisplayedSpots.Add(new SpotModel { Capacity = SelectedRoom.Capacity, Color = SelectedRoom.Color, Number = SpotNumber, Taken = 0, IsEmpty = true, Hosts = new List<HostModel>() });
                    SpotsAndGroupsCollectionContainer.Clear();
                    SpotsAndGroupsCollectionContainer.Add(new CollectionContainer() { Collection = DisplayedSpots });
                    SpotsAndGroupsCollectionContainer.Add(new CollectionContainer() { Collection = DisplayedGroups }) ;

                    var allSpots = HostsList.Where(h => (h.HotelRoom.FlightHotel.HotelId == SelectedHotel.Id && h.HotelRoom.FlightHotel.FlightId == SelectedFlight.Id && h.Spot != null)).Select(h => h.Spot).Distinct().ToList();
                    RoomsClientsNumbersList = new(new int[RoomsList.Count]);
                    RoomsNumbersList = new(new int[RoomsList.Count]);
                    for (int i = 0; i < RoomsList.Count; i++)
                    {
                        RoomsNumbersList[i] = allSpots.Where(s => s.Capacity == RoomsList[i].Capacity).Count();
                        RoomsClientsNumbersList[i] = allSpots.Where(s => s.Capacity == RoomsList[i].Capacity).Select(s => s.Hosts.Count).Sum();
                    }
                    TotalSpots = RoomsNumbersList.Sum();
                    TotalHosts = RoomsClientsNumbersList.Sum();

                }
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while adding the spot");
            }   
            
        }

        public void AddGroup(string color)
        {
            try
            {
                var spots = DisplayedSpots.Where(s => s.Selected).ToArray();
                var newGroup = new GroupModel { Id = 0, Color = color, Spots = spots.ToList() };
                DisplayedGroups.Add(newGroup);
                for(int i = 0; i < spots.Length; i++)
                {
                    spots[i].Group = newGroup;
                    DisplayedSpots.Remove(spots[i]);
                }
                SpotsAvailable = false;
                SpotsAndGroupsCollectionContainer.Clear();
                SpotsAndGroupsCollectionContainer.Add(new CollectionContainer() { Collection = DisplayedSpots });
                SpotsAndGroupsCollectionContainer.Add(new CollectionContainer() { Collection = DisplayedGroups });
            }
            catch (Exception)
            {

                Growl.Error("An error occurred while adding the group");
            }

        }

        public void BreakGroup()
        {
            try
            {
                var group = DisplayedGroups.Where(s => s.Selected).FirstOrDefault();
                var spots = group.Spots.Cast<SpotModel>().ToArray();
                for (int i = 0; i < spots.Length ; i++)
                {
                    spots[i].Selected = false;
                    spots[i].Group = null;
                    spots[i].GroupId = null;
                    DisplayedSpots.Add(spots[i]);
                }
                GroupsListToRemove.Add(group);
                DisplayedGroups.Remove(group);
                GroupAvailable = false;
            }
            catch (Exception)
            {

                Growl.Error("An error occurred while break the group");
            }

        }

       

        public async Task InitData()
        {
            try
            {
                LoadingData = true;
                var seasons = await seasonServices.GetAllSeasons();
                Configuration.CurrentSeason = seasons.Where(s => !s.HasEnded).ToList().FirstOrDefault();
                FlightHotelsList = (await flightHotelServices.GetAllFlightHotels()).ToArray();
                FlightsList = (await flightServices.GetAllFlightsOfSeason(Configuration.CurrentSeason.Id)).ToArray();
                HostsList = await hostServices.GetAllHosts();
                HostsListToRemove = new List<HostModel>();
                RoomsList = await roomServices.GetAllRooms();
                RoomsClientsNumbersList = new(new int[RoomsList.Count]);
                RoomsNumbersList = new(new int[RoomsList.Count]);
                SelectedHotels = new ObservableCollection<HotelModel>();
                SelectedHostsWithoutSpots = new ObservableCollection<HostModel>();
                SelectedFlights = new();
                SpotsListToRemove = new();
                GroupsListToRemove = new List<GroupModel>();
                DisplayedSpots = new ObservableCollection<SpotModel>();
                DisplayedGroups = new ObservableCollection<GroupModel>();
                SpotsAndGroupsCollectionContainer = new();
                
                Months = new();
                for (int i = 0; i < FlightsList.Length; i++)
                {
                    string month = FlightsList[i].DepartDate.ToString("MMMM", new CultureInfo("ar-DZ"));
                    if (!Months.Contains(month))
                        Months.Add(month);
                }
                OnPropertyChanged(nameof(Months));
                LoadingData = false;
            }
            catch (Exception)
            {
                LoadingData = false;
                Growl.Error("An error occurred while trying to InitData in hotel");
            }
            
        }
        public void Dispose()
        {
            GC.Collect();
        }
        #endregion
    }
}
