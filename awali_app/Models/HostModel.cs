
using Airfare.ViewModels.UserControlViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airfare.Models
{
    [Table("Hosts")]
    public class HostModel : BaseViewModel
    {
        [Key]
        public int Id { get; set; }
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
            get
            {
                return _RemainingPrice;
            }
            set
            {
                _RemainingPrice = value;
                OnPropertyChanged(nameof(RemainingPrice));
            }
        }
        private float _Discount;

        public float Discount
        {
            get
            {
                return _Discount;
            }
            set
            {
                _Discount = value;
                OnPropertyChanged(nameof(Discount));
            }
        }
        private bool _IsPaid;
        public bool IsPaid
        {
            get { return _IsPaid; }
            set
            {
                _IsPaid = value;
                OnPropertyChanged(nameof(IsPaid));
            }
        }
        public int ClientId { get; set; }
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
        public int HotelRoomId { get; set; }
        private HotelRoomModel
            _HotelRoom;
        public HotelRoomModel HotelRoom
        {
            get
            {
                return _HotelRoom;
            }
            set
            {
                _HotelRoom = value;
                OnPropertyChanged(nameof(HotelRoom));
            }
        }
        public int? SpotId { get; set; }
        private SpotModel _Spot;

        public SpotModel Spot
        {
            get { return _Spot; }
            set
            {
                _Spot = value;
                OnPropertyChanged(nameof(Spot));
            }
        }

        public List<PaymentModel> Payments { get; set; }
        public int? CompanyId { get; set; }
        private CompanyModel? _Company;
        public CompanyModel? Company
        {
            get { return _Company; }
            set
            {
                _Company = value;
                OnPropertyChanged(nameof(Company));
            }
        }

    }
}
