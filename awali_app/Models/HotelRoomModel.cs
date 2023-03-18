﻿using Airfare.ViewModels.UserControlViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Models
{
    [Table("HotelRooms")]
    public class HotelRoomModel:BaseViewModel
    {
        [Key]
        public int Id { get; set; }
        private float _Price;
        public float Price { get { return _Price; } set { _Price = value; OnPropertyChanged(nameof(Price)); } }
        public int RoomId { get; set; }
        public int FlightHotelId { get; set; }
        public RoomModel Room { get; set; }
        public FlightHotelModel FlightHotel { get; set; }

    }
}
