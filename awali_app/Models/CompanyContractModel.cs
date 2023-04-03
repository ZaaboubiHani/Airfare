using Airfare.ViewModels.UserControlViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Models
{
    [Table("CompanyContracts")]
    public class CompanyContractModel : BaseModel
    {
        [Key]
        private int _id;
        public int Id { get { return _id; } set { SetProperty(ref _id, value); } }

        private int _roomsNumber;
        public int RoomsNumber { get { return _roomsNumber; } set { SetProperty(ref _roomsNumber, value); } }

        private int _paidNumber;
        public int PaidNumber { get { return _paidNumber; } set { SetProperty(ref _paidNumber, value); } }

        private float _price;
        public float Price { get { return _price; } set { SetProperty(ref _price, value); } }

        private HotelRoomModel _hotelRoom;
        public HotelRoomModel HotelRoom { get { return _hotelRoom; } set { SetProperty(ref _hotelRoom, value); } }

        public int HotelRoomId { get; set; }

        private CompanyModel _company;
        public CompanyModel Company { get { return _company; } set { SetProperty(ref _company, value); } }

        public int CompanyId { get; set; }
    }
}
