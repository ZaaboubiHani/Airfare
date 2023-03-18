using Airfare.ViewModels.UserControlViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Models
{
    [Table("Rooms")]
    public class RoomModel:BaseViewModel
    {
        [Key]
        private int _Id;
        public int Id { get { return _Id; } set { _Id = value; OnPropertyChanged(nameof(Id)); } }
        private string _Type;
        public string Type { get { return _Type; } set { _Type = value; OnPropertyChanged(nameof(Type)); } }
        private int _Capacity;
        public int Capacity { get { return _Capacity; } set { _Capacity = value; OnPropertyChanged(nameof(Capacity)); } }
        private string _Color;
        public string Color { get { return _Color; } set { _Color = value; OnPropertyChanged(nameof(Color)); } }

        public override string ToString()
        {
            return Type;
        }

       

    }
}
