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
    public class CompanyContractModel: BaseViewModel
    {
        [Key]
        public int Id { get; set; }
        public int RoomsNumber { get; set; }
        public int PaidNumber { get; set; }
        private float _Price;
        public float Price { get { return _Price; } set { _Price = value; OnPropertyChanged(nameof(Price)); } }
        private HotelRoomModel _HotelRoom;
        public HotelRoomModel HotelRoom { get { return _HotelRoom; } set { _HotelRoom = value; OnPropertyChanged(nameof(HotelRoom)); } }
        public int HotelRoomId { get; set; }
        public CompanyModel Company { get; set; }
        public int CompanyId { get; set; }
    }
}
