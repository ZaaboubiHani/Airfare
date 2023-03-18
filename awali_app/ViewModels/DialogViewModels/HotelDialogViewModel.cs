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
    public class HotelDialogViewModel : BaseViewModel, IDialogResultable<HotelModel>,IDisposable
    {
        private HotelModel _Hotel;

        public HotelModel Hotel
        {
            get { return _Hotel; }
            set
            {
                _Hotel = value;
                OnPropertyChanged(nameof(Hotel));
            }
        }

        private bool _UpdateMode;

        public bool UpdateMode
        {
            get { return _UpdateMode; }
            set
            {
                _UpdateMode = value;
                OnPropertyChanged(nameof(UpdateMode));
            }
        }

        HotelModel IDialogResultable<HotelModel>.Result
        {
            get { return Hotel; }
            set { Hotel = value; }
        }

        Action close;

        Action IDialogResultable<HotelModel>.CloseAction
        {
            get { return close; }
            set { close = value; }
        }

        #region Services
        private readonly HotelServices hotelServices = new();
        #endregion

        #region Commands

        public RelayCommand CloseDialogCommand { get; set; }
        public RelayCommand AddHotelCommand { get; set; }
        public RelayCommand UpdateHotelCommand { get; set; }

        #endregion

        public HotelDialogViewModel()
        {
            CloseDialogCommand = new RelayCommand(CloseDialog);
            AddHotelCommand = new RelayCommand(AddHotel);
            UpdateHotelCommand = new RelayCommand(UpdateHotel);
            InitData();
        }

        private async void UpdateHotel()
        {
            await hotelServices.UpdateHotel(Hotel);

            if (!hotelServices.Error)
            {
                Growl.Success("تمت تحديث الفندق بنجاح");
                close.Invoke();
            }
            else
            {
                Growl.Error("\n:خطأ\n" + hotelServices.ErrorMessage);
            }
        }

        private void InitData()
        {
            Hotel = new HotelModel();
        }

        private async void AddHotel()
        {
            await hotelServices.AddHotel(Hotel);
            
            if (!hotelServices.Error)
            {
                Growl.Success("تمت إضافة الفندق بنجاح");
                close.Invoke();
            }
            else
            {
                Growl.Error("\n:خطأ\n" + hotelServices.ErrorMessage);
            }
        }

        private void CloseDialog()
        {
            Hotel = null;
            close.Invoke();
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
