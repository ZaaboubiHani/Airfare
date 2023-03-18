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
using System.Text;
using System.Threading.Tasks;

namespace Airfare.ViewModels.UserControlViewModels
{
    public class RoomViewModel:BaseViewModel, IDisposable
    {

        #region Stylize
        private bool _LoadingCircle;

        public bool LoadingCircle
        {
            get { return _LoadingCircle; }
            set
            {
                _LoadingCircle = value;
                OnPropertyChanged(nameof(LoadingCircle));
            }
        }

        private bool _LoadingLine;

        public bool LoadingLine
        {
            get { return _LoadingLine; }
            set
            {
                _LoadingLine = value;
                OnPropertyChanged(nameof(LoadingLine));
            }
        }

        #endregion

        #region Properties
        private ObservableCollection<RoomModel> _RoomsList;

        public ObservableCollection<RoomModel> RoomsList
        {
            get { return _RoomsList; }
            set
            {
                _RoomsList = value;
                OnPropertyChanged(nameof(RoomsList));
            }
        }

        private RoomModel _SelectedRoom;

        public RoomModel SelectedRoom
        {
            get { return _SelectedRoom; }
            set
            {
                _SelectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }

        private RoomModel _Room;

        public RoomModel Room
        {
            get { return _Room; }
            set
            {
                _Room = value;
                OnPropertyChanged(nameof(Room));
            }
        }

        private FlightHotelModel[] flightHotels { get; set; }
        public bool UpdateMode { get; set; }
        #endregion

        #region Services
        private readonly RoomServices roomServices = new();
        private readonly FlightHotelServices flightHotelServices = new();
        private readonly HotelRoomServices hotelRoomServices = new();
        private readonly SeasonServices seasonServices = new();
        #endregion

        #region Commands
        public RelayCommand AddRoomCommand { get; set; }
        public RelayCommand RemoveRoomCommand { get; set; }
        public RelayCommand UpdateRoomCommand { get; set; }
        #endregion

        public RoomViewModel()
        {
            InitData();
            AddRoomCommand = new RelayCommand(AddRoom);
            RemoveRoomCommand = new RelayCommand(RemoveRoom);
            UpdateRoomCommand = new RelayCommand(UpdateRoom);
        }

        #region Methods
        private async void UpdateRoom()
        {
            try
            {
                LoadingLine = true;
                var res = await Dialog.Show<RoomDialog>().Initialize<RoomDialogViewModel>(vm => { vm.UpdateMode = true; vm.Type = SelectedRoom.Type;vm.Capacity = SelectedRoom.Capacity; vm.Room = SelectedRoom; }).GetResultAsync<RoomModel>();
                if (res is not null)
                {
                    await roomServices.UpdateRoom(res);
                    if (!roomServices.Error)
                    {
                        InitData();
                        Growl.Success("تمت تحديث الغرفة بنجاح");
                    }
                    else
                    {
                        Growl.Error("\n:خطأ\n" + roomServices.ErrorMessage);
                    }
                }
                LoadingLine = false;
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while updating room");
            }
            
        }

        private async void RemoveRoom()
        {
            try
            {
                LoadingLine = true;
                bool res = await Dialog.Show<YesNoDialog>().Initialize<YesNoDialogViewModel>(vm => vm.Description = "هل أنت متأكد أنك تريد حذف هذه الغرفة؟").GetResultAsync<bool>();
                if (res)
                {
                    await roomServices.RemoveRoom(SelectedRoom);
                    if (!roomServices.Error)
                    {
                        InitData();
                        Growl.Success("تم حذف الغرفة بنجاح");
                    }
                    else
                    {
                        Growl.Error("\n:خطأ\n" + roomServices.ErrorMessage);
                    }
                }
                LoadingLine = false;
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while removing room");
            }
           
        }

        private async void AddRoom()
        {
            try
            {
                LoadingCircle = true;
                await roomServices.AddRoom(Room);
                if (!roomServices.Error)
                {
                    RoomsList.Add(Room);
                    for (int i = 0; i < flightHotels.Length; i++)
                    {
                        HotelRoomModel hotelRoom = new HotelRoomModel
                        {
                            Price = 0,
                            RoomId = Room.Id,
                            FlightHotelId = flightHotels[i].Id,
                        };
                        await hotelRoomServices.AddHotelRoom(hotelRoom);
                    }
                    if (!hotelRoomServices.Error)
                    {
                        InitData();
                        Growl.Success("تمت إضافة الغرفة بنجاح");
                    }
                }
                else
                {
                    Growl.Error("\n:خطأ\n" + roomServices.ErrorMessage);
                }
                LoadingCircle = false;
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while adding room");
            }
            
        }

        private async void InitData()
        {
            try
            {
                LoadingLine = true;
                Room = new RoomModel();
                var seasons = await seasonServices.GetAllSeasons();
                Configuration.CurrentSeason = seasons.Where(s => !s.HasEnded).ToList().FirstOrDefault();
                flightHotels = (await flightHotelServices.GetAllFlightHotels()).ToArray();

                RoomsList = new ObservableCollection<RoomModel>(await roomServices.GetAllRooms());
                LoadingLine = false;
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while InitData in room");
            }
           
        }
        public void Dispose()
        {
            GC.Collect();
        }
        #endregion
    }
}
