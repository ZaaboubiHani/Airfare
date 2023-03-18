using Airfare.Commands;
using Airfare.Models;
using Airfare.Servies;
using Airfare.ViewModels.UserControlViewModels;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Airfare.ViewModels.DialogViewModels
{
    public class RoomDialogViewModel : BaseViewModel, IDialogResultable<RoomModel>,IDisposable
    {
        private bool _UpdateMode;

        public bool UpdateMode
        {
            get { return _UpdateMode; }
            set { _UpdateMode = value;
                OnPropertyChanged(nameof(UpdateMode));
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

        public int Capacity
        {
            get { return Room.Capacity; }
            set { Room.Capacity = value;
                
                
                OnPropertyChanged(nameof(Capacity));
            }
        }

        public string Type
        {
            get { return Room.Type; }
            set { Room.Type = value;
                
                OnPropertyChanged(nameof(Type));
            }
        }

      

        public SolidColorBrush Color
        {
            get { return new SolidColorBrush((Color)ColorConverter.ConvertFromString(Room.Color)) ; }
            set { Room.Color = value.Color.ToString();
                OnPropertyChanged(nameof(Color));
            }
        }


        RoomModel IDialogResultable<RoomModel>.Result
        {
            get { return Room; }
            set { Room = value; }
        }

        Action close;

        Action IDialogResultable<RoomModel>.CloseAction
        {
            get { return close; }
            set { close = value; }
        }

        private List<FlightHotelModel> flightHotels { get; set; }

        #region Services
        private readonly RoomServices roomServices = new();
        private readonly FlightHotelServices flightHotelServices = new();
        private readonly HotelRoomServices hotelRoomServices = new();
        #endregion

        #region Commands
        public RelayCommand CloseDialogCommand { get; set; }
        public RelayCommand AddRoomCommand { get; set; }
        public RelayCommand UpdateRoomCommand { get; set; }
        #endregion

        public RoomDialogViewModel()
        {
            InitData();
        }

        private async void InitData()
        {
            Room = new RoomModel { Color = "#FFF" };
            CloseDialogCommand = new RelayCommand(CloseDialog);
            AddRoomCommand = new RelayCommand(AddRoom);
            UpdateRoomCommand = new RelayCommand(UpdateRoom);
            flightHotels = await flightHotelServices.GetAllFlightHotels();
        }

        private async void UpdateRoom()
        {
            await roomServices.UpdateRoom(Room);

            if (!hotelRoomServices.Error)
            {
               
                close.Invoke();
            }
            else
            {
                Growl.Error("\n:خطأ\n" + roomServices.ErrorMessage);
            }
        }

        private async void AddRoom()
        {
            await roomServices.AddRoom(Room);

            if (!roomServices.Error)
            {
                for(int i = 0;i< flightHotels.Count;i++)
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
                    Growl.Success("تمت إضافة الغرفة بنجاح");
                    close.Invoke();
                }
            }
            else
            {
                Growl.Error("\n:خطأ\n" + roomServices.ErrorMessage);
            }
        }

        private void CloseDialog()
        {
            Room = null;
            close.Invoke();
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
