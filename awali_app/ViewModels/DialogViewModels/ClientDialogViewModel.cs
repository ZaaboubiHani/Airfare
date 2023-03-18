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
    public class ClientDialogViewModel : BaseViewModel, IDialogResultable<HostModel>, IDisposable
    {
        private ClientModel _Client;

        public ClientModel Client
        {
            get { return _Client; }
            set
            {
                _Client = value;

                OnPropertyChanged(nameof(Client));
            }
        }

        private bool _Feed;

        public bool Feed
        {
            get { return _Feed; }
            set
            {
                _Feed = value;
                OnPropertyChanged(nameof(Feed));
                Client.Feed = value;
                GetFullPrice();
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

        private bool _IsGuide;

        public bool IsGuide
        {
            get { return _IsGuide; }
            set { _IsGuide = value;
                OnPropertyChanged(nameof(IsGuide));
                Client.IsGuide = value;
            }
        }


        private ObservableCollection<PaymentModel> _PaymentsList;

        public ObservableCollection<PaymentModel> PaymentsList
        {
            get { return _PaymentsList; }
            set
            {
                _PaymentsList = value;
                OnPropertyChanged(nameof(PaymentsList));
            }
        }

        private ObservableCollection<PhoneModel> _PhonesList;

        public ObservableCollection<PhoneModel> PhonesList
        {
            get { return _PhonesList; }
            set
            {
                _PhonesList = value;
                OnPropertyChanged(nameof(PhonesList));
            }
        }

        private PaymentModel _Payment;

        public PaymentModel Payment
        {
            get { return _Payment; }
            set
            {
                _Payment = value;
                OnPropertyChanged(nameof(Payment));
            }
        }

        private HostModel _Host;

        public HostModel Host
        {
            get { return _Host; }
            set
            {
                _Host = value;
                OnPropertyChanged(nameof(Host));
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
                if (SelectedFlightHotel != null)
                {
                    for (int i = 0; i < FlightHotels.Count; i++)
                    {
                        if (FlightHotels[i].Id == SelectedFlightHotel.Id)
                        {
                            SelectedFlightHotelIndex = i;
                            break;
                        }

                    }

                }
                GetFullPrice();
            }
        }

        private int _SelectedFlightHotelIndex = -1;

        public int SelectedFlightHotelIndex
        {
            get { return _SelectedFlightHotelIndex; }
            set
            {
                _SelectedFlightHotelIndex = value;
                OnPropertyChanged(nameof(SelectedFlightHotelIndex));

                GetFullPrice();
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
                if (SelectedRoom != null)
                {
                    for (int i = 0; i < Rooms.Count; i++)
                    {
                        if (Rooms[i].Id == SelectedRoom.Id)
                        {
                            SelectedRoomIndex = i;
                            break;
                        }

                    }

                }

                GetFullPrice();
            }
        }

        private int _SelectedRoomIndex = -1;

        public int SelectedRoomIndex
        {
            get { return _SelectedRoomIndex; }
            set
            {
                _SelectedRoomIndex = value;
                OnPropertyChanged(nameof(SelectedRoomIndex));
            }
        }

        private CompanyModel? _SelectedCompany;

        public CompanyModel? SelectedCompany
        {
            get { return _SelectedCompany; }
            set
            {
                _SelectedCompany = value;
                OnPropertyChanged(nameof(SelectedCompany));

                if (SelectedCompany != null)
                {
                    for (int i = 0; i < Companies.Count; i++)
                    {
                        if (Companies[i].Id == SelectedCompany.Id)
                        {
                            SelectedCompanyIndex = i;
                            break;
                        }
                    }
                }

                GetFullPrice();
            }
        }

        private int _SelectedCompanyIndex = -1;

        public int SelectedCompanyIndex
        {
            get { return _SelectedCompanyIndex; }
            set
            {
                _SelectedCompanyIndex = value;
                OnPropertyChanged(nameof(SelectedCompanyIndex));

                GetFullPrice();
            }
        }

        private List<FlightHotelModel> _FlightHotels;

        public List<FlightHotelModel> FlightHotels
        {
            get { return _FlightHotels; }
            set
            {
                _FlightHotels = value;
                OnPropertyChanged(nameof(FlightHotels));
            }
        }

        private List<RoomModel> _Rooms;

        public List<RoomModel> Rooms
        {
            get { return _Rooms; }
            set
            {
                _Rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }

        private List<CompanyModel> _Companies;

        public List<CompanyModel> Companies
        {
            get { return _Companies; }
            set
            {
                _Companies = value;
                OnPropertyChanged(nameof(Companies));
            }
        }

        private float _FullPrice;

        public float FullPrice
        {
            get { return _FullPrice; }
            set
            {
                _FullPrice = value;
                OnPropertyChanged(nameof(FullPrice));
            }
        }

        private float _PaidPrice;

        public float PaidPrice
        {
            get { return _PaidPrice; }
            set
            {
                _PaidPrice = value;
                OnPropertyChanged(nameof(PaidPrice));

            }
        }

        private float _RemainingPrice;

        public float RemainingPrice
        {
            get { return _RemainingPrice; }
            set
            {
                _RemainingPrice = value;
                OnPropertyChanged(nameof(RemainingPrice));

            }
        }

        private float _Discount;

        public float Discount
        {
            get { return _Discount; }
            set
            {
                _Discount = value;
                OnPropertyChanged(nameof(Discount));

                GetFullPrice();
            }
        }

        private string _PhoneNumber;

        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set
            {
                _PhoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        private PaymentModel _SelectedPayment;

        public PaymentModel SelectedPayment
        {
            get { return _SelectedPayment; }
            set
            {
                _SelectedPayment = value;
                OnPropertyChanged(nameof(SelectedPayment));
            }
        }

        private PhoneModel _SelectedPhone;

        public PhoneModel SelectedPhone
        {
            get { return _SelectedPhone; }
            set
            {
                _SelectedPhone = value;

                OnPropertyChanged(nameof(SelectedPhone));
            }
        }


        private List<HotelRoomModel> _HotelsRooms;

        public List<HotelRoomModel> HotelsRooms
        {
            get { return _HotelsRooms; }
            set
            {
                _HotelsRooms = value;
                OnPropertyChanged(nameof(HotelsRooms));
            }
        }

        HostModel IDialogResultable<HostModel>.Result
        {
            get { return Host; }
            set { Host = value; }
        }

        Action close;

        Action IDialogResultable<HostModel>.CloseAction
        {
            get { return close; }
            set { close = value; }
        }

        public CompanyModel hiddenCompanySelector { get; set;}

        #region Services
        private readonly HostServices hostServices = new();
        private readonly ClientServices clientServices = new();
        private readonly PaymentServices paymentServices = new();
        private readonly PhoneServices phoneServices = new();
        private readonly CompanyServices companyServices = new();
        #endregion

        #region Commands
        public RelayCommand CloseDialogCommand { get; set; }
        public RelayCommand AddClientCommand { get; set; }
        public RelayCommand UpdateClientCommand { get; set; }
        public RelayCommand AddPaymentCommand { get; set; }
        public RelayCommand RemovePaymentCommand { get; set; }
        public RelayCommand RemovePhoneCommand { get; set; }
        public RelayCommand AddPhoneCommand { get; set; }
        #endregion

        public ClientDialogViewModel()
        {
            InitData();
            InitCommands();
        }

        private async void RemovePayment()
        {
            await paymentServices.RemovePayment(SelectedPayment);
            PaymentsList.Remove(SelectedPayment);
            GetFullPrice();
        }

        private void AddPayment()
        {
            if(Payment.Amount > 0)
            {
                Payment.Date = DateTime.Now;
                if (PaidPrice + Payment.Amount > FullPrice)
                {
                    Payment.Amount = Host.RemainingPrice;
                }

                PaymentsList.Add(Payment);
                Payment = new PaymentModel();
                GetFullPrice();
            }
            else
            {
                Growl.Warning("لا يمكن ادخال دفع بهذ القيمة");
            }
        }

        private void InitCommands()
        {
            CloseDialogCommand = new RelayCommand(CloseDialog);
            AddClientCommand = new RelayCommand(AddClient);
            AddPaymentCommand = new RelayCommand(AddPayment);
            RemovePaymentCommand = new RelayCommand(RemovePayment);
            RemovePhoneCommand = new RelayCommand(RemovePhone);
            AddPhoneCommand = new RelayCommand(AddPhone);
            UpdateClientCommand = new RelayCommand(UpdateClient);
        }

        private async void UpdateClient()
        {
            Client.Phones = null;
            if (Client.Color == Host.HotelRoom.Room.Color)
                Client.Color = null;
            await clientServices.UpdateClient(Client);

            if (!clientServices.Error)
            {
                Host.HotelRoomId = HotelsRooms.Where(hr => (hr.RoomId == SelectedRoom.Id && hr.FlightHotelId == SelectedFlightHotel.Id)).FirstOrDefault().Id;
                Host.FullPrice = FullPrice;
                Host.ClientId = Client.Id;
                Host.Discount = Discount;
                Host.PaidPrice = PaidPrice;
                Host.RemainingPrice = RemainingPrice;
                Host.Payments = null;
                Host.Client = null;
                Host.HotelRoom = null;
                if (Host.PaidPrice == Host.FullPrice)
                {
                    Host.IsPaid = true;
                }
                else
                {
                    Host.IsPaid = false;
                }
                if (SelectedCompany is not null)
                {
                    Host.CompanyId = SelectedCompany.Id;
                }
                else
                {
                    Host.CompanyId = null;
                }
                Host.Company = null;
                await hostServices.UpdateHost(Host);
                Host.Client = Client;
                Host.Company = SelectedCompany;
                Host.HotelRoom = HotelsRooms.Where(hr => (hr.RoomId == SelectedRoom.Id && hr.FlightHotelId == SelectedFlightHotel.Id)).FirstOrDefault();
               
                if (!hostServices.Error)
                {

                    for (int i = 0; i< PaymentsList.Count;i++)
                    {
                        PaymentsList[i].HostId = Host.Id;
                        await paymentServices.AddPaymentIfNotExist(PaymentsList[i]);
                    }
                    for (int i = 0; i < PhonesList.Count;i++)
                    {
                        PhonesList[i].ClientId = Host.ClientId;
                        await phoneServices.AddPhoneIfNotExist(PhonesList[i]);
                    }

                    if (!paymentServices.Error && !phoneServices.Error)
                    {
                        Host.Payments = PaymentsList.ToList();
                        Host.Client.Phones = PhonesList.ToList();
                        Growl.Success("تم حفظ تعديلات المعتمر بنجاح");
                        close.Invoke();
                    }
                    else
                    {
                        for (int i = 0; i < PaymentsList.Count; i ++)
                        {
                            await paymentServices.RemovePayment(PaymentsList[i]);
                            PaymentsList[i].Id = 0;
                        }
                        for (int i = 0; i < PhonesList.Count; i ++)
                        {
                            await phoneServices.RemovePhone(PhonesList[i]);
                            PhonesList[i].Id = 0;
                        }
                        await hostServices.RemoveHost(Host);
                        await clientServices.RemoveClient(Client);
                        Host.HotelRoom = null;
                        Host.Id = 0;
                        Client.Id = 0;
                        Growl.Error("\n:خطأ\n" + paymentServices.ErrorMessage);
                    }
                }
                else
                {
                    await hostServices.RemoveHost(Host);
                    await clientServices.RemoveClient(Client);
                    Host.HotelRoom = null;
                    Host.Id = 0;
                    Client.Id = 0;
                    Growl.Error("\n:خطأ\n" + hostServices.ErrorMessage);
                }

            }
            else
            {
                Client.Id = 0;
                Growl.Error("\n:خطأ\n" + clientServices.ErrorMessage);
            }
        }

        private void AddPhone()
        {
            if (!string.IsNullOrEmpty(PhoneNumber))
            {


                if (PhoneNumber.Length == 10)
                {
                    PhoneModel phone = new() { Number = PhoneNumber };
                    PhonesList.Add(phone);
                    PhoneNumber = "";
                }
                else
                {
                    Growl.Warning("رقم الهاتف غير صحيح");
                }
            }
        }

        private async void RemovePhone()
        {
            await phoneServices.RemovePhone(SelectedPhone);
            PhonesList.Remove(SelectedPhone);
        }

        private async void InitData()
        {
            Client = new() { BirthDate = DateTime.Now };
            Host = new();
            PaymentsList = new();
            PhonesList = new();
            Payment = new();
            Companies = await companyServices.GetAllCompanies();
        }

        private void GetFullPrice()
        {
            if (SelectedRoom != null && SelectedFlightHotel != null)
            {
                for (int i = 0; i < HotelsRooms.Count; i++)
                {
                    if (HotelsRooms[i].FlightHotelId == SelectedFlightHotel.Id && HotelsRooms[i].RoomId == SelectedRoom.Id)
                    {
                        if (Client.Feed)
                        {
                            FullPrice = HotelsRooms[i].Price + SelectedFlightHotel.Feed - Discount;
                            PaidPrice = 0;
                            PaymentsList.ForEach(p => { PaidPrice += p.Amount; });
                            RemainingPrice = FullPrice - PaidPrice;
                            Host.HotelRoomId = HotelsRooms[i].Id;
                        }
                        else
                        {

                            FullPrice = HotelsRooms[i].Price - Discount;
                            PaidPrice = 0;
                            PaymentsList.ForEach(p => { PaidPrice += p.Amount; });
                            RemainingPrice = FullPrice - PaidPrice;
                            Host.HotelRoomId = HotelsRooms[i].Id;
                        }
                    }
                }
            }
        }

        private async void AddClient()
        {
            if(HotelsRooms.Count == 0)
            {
                Growl.Warning("لا تحتوي هذه الرحلة على فندق أو غرف ، يرجى إضافتها حتى تتمكن من إضافة المعتمر");
            }
            else
            {
                await clientServices.AddClient(Client);

                if (!clientServices.Error)
                {
                    Host.FullPrice = FullPrice;
                    Host.ClientId = Client.Id;
                    Host.Discount = Discount;
                    Host.PaidPrice = PaidPrice;
                    Host.RemainingPrice = RemainingPrice;

                    if (Host.PaidPrice == Host.FullPrice)
                    {
                        Host.IsPaid = true;
                    }
                    else
                    {
                        Host.IsPaid = false;
                    }
                    if (SelectedCompany != null && SelectedCompanyIndex != -1)
                    {
                        Host.CompanyId = SelectedCompany.Id;
                    }
                    if(Host.HotelRoomId == 0)
                    {
                        Host.HotelRoomId = HotelsRooms[0].Id;
                    }
                    await hostServices.AddHost(Host);
                    Host.Client = Client;
                    Host.Company = SelectedCompany;
                    for (int i = 0; i< HotelsRooms.Count; i ++)
                    {
                        if(SelectedRoom is null)
                        {
                            Host.HotelRoom = HotelsRooms[0];
                            break;
                        }
                        if (HotelsRooms[i].RoomId == SelectedRoom.Id )
                        {
                            Host.HotelRoom = HotelsRooms[i];
                            break;
                        }

                    }
                    if (!hostServices.Error)
                    {

                        for (int i = 0; i < PaymentsList.Count;i++)
                        {
                            PaymentsList[i].HostId = Host.Id;
                            await paymentServices.AddPayment(PaymentsList[i]);
                        }
                        for (int i = 0; i < PhonesList.Count;i++)
                        {
                            PhonesList[i].ClientId = Host.ClientId;
                            await phoneServices.AddPhone(PhonesList[i]);
                        }

                        if (!paymentServices.Error && !phoneServices.Error)
                        {
                            Growl.Success("تمت إضافة المعتمر بنجاح");
                            close.Invoke();
                        }
                        else
                        {
                            for (int i = 0; i < PaymentsList.Count; i++)
                            {
                                await paymentServices.RemovePayment(PaymentsList[i]);
                                PaymentsList[i].Id = 0;
                            }
                            for (int i = 0; i < PhonesList.Count; i++)
                            {
                                await phoneServices.RemovePhone(PhonesList[i]);
                                PhonesList[i].Id = 0;
                            }
                            await hostServices.RemoveHost(Host);
                            await clientServices.RemoveClient(Client);
                            Host.HotelRoom = null;
                            Host.Id = 0;
                            Client.Id = 0;
                            Growl.Error("\n:خطأ\n" + paymentServices.ErrorMessage);
                        }
                    }
                    else
                    {
                        await hostServices.RemoveHost(Host);
                        await clientServices.RemoveClient(Client);
                        Host.HotelRoom = null;
                        Host.Id = 0;
                        Client.Id = 0;
                        Growl.Error("\n:خطأ\n" + hostServices.ErrorMessage);
                    }

                }
                else
                {
                    Client.Id = 0;
                    Growl.Error("\n:خطأ\n" + clientServices.ErrorMessage);
                }
            }
        }

        private void CloseDialog()
        {
            Client = null;
            Host = null;
            close.Invoke();
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
