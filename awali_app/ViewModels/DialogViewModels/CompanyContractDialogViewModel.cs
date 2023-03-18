using Airfare.Commands;
using Airfare.DataContext;
using Airfare.Models;
using Airfare.Servies;
using Airfare.ViewModels.UserControlViewModels;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Airfare.ViewModels.DialogViewModels
{
    public class CompanyContractDialogViewModel : BaseViewModel, IDialogResultable<CompanyContractModel>, IDisposable
    {

        private CompanyContractModel _CompanyContract;

        public CompanyContractModel CompanyContract
        {
            get { return _CompanyContract; }
            set { _CompanyContract = value;
                OnPropertyChanged(nameof(CompanyContract));
            }
        }

        private bool _UpdateMode;

        public bool UpdateMode
        {
            get { return _UpdateMode; }
            set { _UpdateMode = value;
                OnPropertyChanged(nameof(UpdateMode));
            }
        }

        private ICollectionView _FlightHotelsList;

        public ICollectionView FlightHotelsList
        {
            get { return _FlightHotelsList; }
            set { _FlightHotelsList = value;
                OnPropertyChanged(nameof(FlightHotelsList));
            }
        }

        private FlightModel _SelectedFlight;

        public FlightModel SelectedFlight
        {
            get { return _SelectedFlight; }
            set { _SelectedFlight = value;
                OnPropertyChanged(nameof(SelectedFlight));
                if(SelectedFlight != null)
                {
                    FlightHotelsList.Filter = FilterFlightHotels;
                    FlightHotelsList.Refresh();
                    CalculateAmount();
                }
            }
        }

        private FlightHotelModel _SelectedFlightHotel;

        public FlightHotelModel SelectedFlightHotel
        {
            get { return _SelectedFlightHotel; }
            set { _SelectedFlightHotel = value;
                OnPropertyChanged(nameof(SelectedFlightHotel));
                CalculateAmount();
            }
        }

        private RoomModel _SelectedRoom;

        public RoomModel SelectedRoom
        {
            get { return _SelectedRoom; }
            set { _SelectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
                CalculateAmount();
            }
        }

        private int _RoomsNumber;

        public int RoomsNumber
        {
            get { return _RoomsNumber; }
            set {
                _RoomsNumber = value;
                OnPropertyChanged(nameof(RoomsNumber));
                CalculateAmount();
            }
        }

        private float _FullPrice;

        public float FullPrice
        {
            get { return _FullPrice; }
            set { _FullPrice = value;
                OnPropertyChanged(nameof(FullPrice));
            }
        }


        CompanyContractModel IDialogResultable<CompanyContractModel>.Result
        {
            get { return CompanyContract; }
            set { CompanyContract = value; }
        }

        Action close;

        Action IDialogResultable<CompanyContractModel>.CloseAction
        {
            get { return close; }
            set { close = value; }
        }

        private ObservableCollection<FlightModel> _FlightsList;

        public ObservableCollection<FlightModel> FlightsList
        {
            get { return _FlightsList; }
            set { _FlightsList = value;
                OnPropertyChanged(nameof(FlightsList));
            }
        }

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
       
        public HotelRoomModel[] HotelRoomsList;
        #region Services
        private readonly CompanyContractServices companyContractServices = new();
        private readonly HotelRoomServices hotelRoomServices = new();
        #endregion

        #region Commands
        public RelayCommand CloseDialogCommand { get; set; }
        public RelayCommand AddCompanyContractCommand { get; set; }
        public RelayCommand UpdateCompanyContractCommand { get; set; }
        #endregion

        public CompanyContractDialogViewModel()
        {
            InitCommands();
            InitData();
        }

        private void CalculateAmount()
        {
            if(SelectedFlight != null && SelectedFlightHotel != null && SelectedRoom != null)
            {
                var hotelroom = HotelRoomsList.Where(hr => hr.RoomId == SelectedRoom.Id).FirstOrDefault();
                CompanyContract.HotelRoom = hotelroom;
                CompanyContract.HotelRoomId = hotelroom.Id;
                FullPrice = hotelroom.Price * RoomsNumber;
            }
            else
            {
                CompanyContract.HotelRoom = null;
                CompanyContract.HotelRoomId = 0;
                FullPrice = 0;
            }
        }

        private async void InitCommands()
        {
                Parallel.Invoke(
                () => { AddCompanyContractCommand = new RelayCommand(AddCompanyContract); },
                () => { UpdateCompanyContractCommand = new RelayCommand(UpdateCompanyContract); },
                () => { CloseDialogCommand = new RelayCommand(CloseDialog); }
                );
        }

        private async void UpdateCompanyContract()
        {
            await companyContractServices.UpdateCompanyContract(CompanyContract);

            if (!companyContractServices.Error)
            {
                Growl.Success("تم تحديث العقد بنجاح");
                close.Invoke();
            }
            else
            {
                Growl.Error("\n:خطأ\n" + companyContractServices.ErrorMessage);
            }
        }

        private async void AddCompanyContract()
        {
            CompanyContract.Price = FullPrice;
            CompanyContract.RoomsNumber = RoomsNumber;
            CompanyContract.HotelRoom = null;
            await companyContractServices.AddCompanyContract(CompanyContract);

            if (!companyContractServices.Error)
            {
                Growl.Success("تمت إضافة العقد بنجاح");
                CompanyContract.HotelRoom = await hotelRoomServices.GetHotelRoom(CompanyContract.HotelRoomId);
                close.Invoke();
            }
            else
            {
                Growl.Error("\n:خطأ\n" + companyContractServices.ErrorMessage);
            }
        }

        private async void InitData()
        {
        }

        private bool FilterFlightHotels(object obj)
        {
            if(obj is FlightHotelModel flightHotel)
            {
                return flightHotel.FlightId == SelectedFlight.Id;
            }
            return false;
        }

        private void CloseDialog()
        {
            CompanyContract = null;
            close.Invoke();
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
