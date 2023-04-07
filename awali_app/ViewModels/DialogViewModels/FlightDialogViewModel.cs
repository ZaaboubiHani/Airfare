using Airfare.Commands;
using Airfare.Models;
using Airfare.Servies;
using Airfare.ViewModels.UserControlViewModels;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.ViewModels.DialogViewModels
{
    public class FlightDialogViewModel : BaseViewModel, IDialogResultable<FlightModel>,IDisposable
    {
        #region Properties
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

        private bool _HotelsLoadingLine;

        public bool HotelsLoadingLine
        {
            get { return _HotelsLoadingLine; }
            set { _HotelsLoadingLine = value;
                OnPropertyChanged(nameof(HotelsLoadingLine));
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

        private List<HotelRoomModel> _HotelRooms;

        public List<HotelRoomModel> HotelRooms
        {
            get { return _HotelRooms; }
            set { _HotelRooms = value;
                OnPropertyChanged(nameof(HotelRooms));
                var hotels = HotelsList.ToList();
                for(int i = 0; i < HotelRooms.Count;i++)
                {
                    hotels.RemoveAll(h => h.Id == HotelRooms[i].FlightHotel.HotelId);
                }
                HotelsList = new ObservableCollection<HotelModel>(hotels);
            }
        }

        private ObservableCollection<HotelRoomModel> _DisplayedHotelsRooms;

        public ObservableCollection<HotelRoomModel> DisplayedHotelsRooms
        {
            get { return _DisplayedHotelsRooms; }
            set {
                _DisplayedHotelsRooms = value;
                OnPropertyChanged(nameof(DisplayedHotelsRooms));
            }
        }

        private ObservableCollection<FlightHotelModel> _SelectedFlightHotelsList;

        public ObservableCollection<FlightHotelModel> SelectedFlightHotelsList
        {
            get { return _SelectedFlightHotelsList; }
            set { _SelectedFlightHotelsList = value;
                OnPropertyChanged(nameof(SelectedFlightHotelsList));
            }
        }

        private FlightHotelModel _SelectedFlightHotel;

        public FlightHotelModel SelectedFlightHotel
        {
            get { return _SelectedFlightHotel; }
            set { _SelectedFlightHotel = value;
                OnPropertyChanged(nameof(SelectedFlightHotel));
                if (value != null)
                {
                    if (HotelRooms.Count > 0)
                    {
                        DisplayedHotelsRooms = new ObservableCollection<HotelRoomModel>(HotelRooms.Where(hr => hr.FlightHotelId == SelectedFlightHotel.Id).ToList());
                    }
                    else
                    {
                        DisplayedHotelsRooms.Clear();
                        for (int i = 0;i< Rooms.Count;i++)
                        {
                            DisplayedHotelsRooms.Add(new HotelRoomModel { Room = Rooms[i], RoomId = Rooms[i].Id });
                        }
                    }
                }
                else
                {
                    DisplayedHotelsRooms.Clear();
                }
            }
        }

      

        FlightModel IDialogResultable<FlightModel>.Result
        {
            get { return Flight; }
            set { Flight = value; }
        }

        Action close;

        Action IDialogResultable<FlightModel>.CloseAction
        {
            get { return close; }
            set { close = value; }
        }

        public List<RoomModel> Rooms { get; set; }
        public List<HotelRoomModel> hotelRoomsControlList { get; set; }
        public List<FlightHotelModel> flightHotelsControlList { get; set; }
        #endregion

        #region Services
        readonly private FlightServices flightServices = new();
        readonly private RoomServices roomServices = new();
        readonly private HotelRoomServices hotelRoomServices = new();
        readonly private FlightHotelServices flightHotelServices = new();
        #endregion

        #region Commands
        public RelayCommand CloseDialogCommand { get; set; }
        public RelayCommand UpdateFlightCommand { get; set; }
        #endregion

        public FlightDialogViewModel()
        {
            InitData();
        }

        #region Methods
        private async void InitData()
        {
            try
            {
                HotelsLoadingLine = true;
                Flight = new FlightModel();
                CloseDialogCommand = new RelayCommand(CloseDialog);
                UpdateFlightCommand = new RelayCommand(UpdateFlight);
                Rooms = await roomServices.GetAllRooms();
                DisplayedHotelsRooms = new();
                HotelsLoadingLine = false;
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while InitData in flight dialog");
            }
            
        }

        private async void UpdateFlight()
        {
            try
            {
                await flightServices.UpdateFlight(Flight);
                for (int i = 0; i < SelectedFlightHotelsList.Count; i++)
                {
                    if (!flightHotelsControlList.Contains(SelectedFlightHotelsList[i]))
                    {
                        SelectedFlightHotelsList[i].Hotel = null;
                        SelectedFlightHotelsList[i].Flight = null;
                        SelectedFlightHotelsList[i].FlightId = Flight.Id;
                        await flightHotelServices.AddFlightHotel(SelectedFlightHotelsList[i]);
                    }
                }
                for (int i = 0; i < HotelRooms.Count;i++)
                {
                    if (hotelRoomsControlList.Contains(HotelRooms[i]))
                    {
                        await hotelRoomServices.UpdateHotelRoom(HotelRooms[i]);
                    }
                    else
                    {
                        
                        HotelRooms[i].FlightHotel.Hotel = null;
                        HotelRooms[i].Room = null;
                        
                        HotelRooms[i].FlightHotelId = HotelRooms[i].FlightHotel.Id;
                        HotelRooms[i].FlightHotel = null;
                        await hotelRoomServices.AddHotelRoom(HotelRooms[i]);
                      
                    }
                }

                for (int i = 0; i < flightHotelsControlList.Count;i++)
                {
                    flightHotelsControlList[i].Flight = null;
                    flightHotelsControlList[i].Hotel = null;
                    if (!SelectedFlightHotelsList.Contains(flightHotelsControlList[i]))
                    {
                        await flightHotelServices.RemoveFlightHotel(flightHotelsControlList[i].Id);
                    }
                    else
                    {
                        await flightHotelServices.UpdateFlightHotel(flightHotelsControlList[i]);
                    }

                }
                close.Invoke();
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while updating flight");
            }
            
        }

        private void CloseDialog()
        {
            try
            {
                Flight = null;
                close.Invoke();
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while closing flight dialog");
            }
           
        }

        public void Dispose()
        {
            GC.Collect();
        }
        #endregion
    }

}
