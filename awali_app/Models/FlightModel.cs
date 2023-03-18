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
    [Table("Flights")]
    public class FlightModel: BaseViewModel
    {
        [Key]
        public int Id { get; set; }
        private string? _DepartName;
        public string? DepartName { get { return _DepartName; } set { _DepartName = value; OnPropertyChanged(DepartName); } }
        public string? ReturnName { get; set; }
        public int? Capacity { get; set; }
        public string? DepartItinerary { get; set; }
        public string? ReturnItinerary { get; set; }
        public DateTime DepartDate { get; set; }
        private TimeSpan _DepartTime { get; set; }
        public TimeSpan DepartTime {
            get { return _DepartTime; }
            set {
                _DepartTime = value; 
                OnPropertyChanged(nameof(DepartTime));
            } 
        }
        public DateTime ReturntDate { get; set; }
        private TimeSpan _ReturnTime { get; set; }
        public TimeSpan ReturnTime
        {
            get { return _ReturnTime; }
            set
            {
                _ReturnTime = value;
                OnPropertyChanged(nameof(ReturnTime));
            }
        }
        public int Category { get; set; }
        public int SeasonId { get; set; }
        public SeasonModel Season { get; set; }

        public override string ToString()
        {
            return DepartDate.Date.ToString("yyyy-MM-dd") + "/" + DepartName;
        }

    }
}
