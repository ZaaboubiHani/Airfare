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
    [Table("Clients")]
    public class ClientModel:BaseViewModel
    {
        [Key]
        public int Id { get; set; }
        private string _FirstName;
        public string FirstName { get { return _FirstName; } set { _FirstName = value; OnPropertyChanged(nameof(FirstName)); } }
        private string _LastName;
        public string LastName { get { return _LastName; } set { _LastName = value; OnPropertyChanged(nameof(LastName)); } }
        private DateTime _BirthDate;
        public DateTime BirthDate { get { return _BirthDate; } set { _BirthDate = value; OnPropertyChanged(nameof(BirthDate)); } }
        private string? _PassportNumber;
        public string? PassportNumber { get { return _PassportNumber; } set { _PassportNumber = value; OnPropertyChanged(nameof(PassportNumber)); } }
        private string? _HealthStatus;
        public string? HealthStatus { get { return _HealthStatus; } set { _HealthStatus = value; OnPropertyChanged(nameof(HealthStatus)); } }
        private bool _Gender;
        public bool Gender { get { return _Gender; } set { _Gender = value; OnPropertyChanged(nameof(Gender)); } }
        public bool IsMinor { get; set; }
        private bool _Feed;
        public bool Feed { get { return _Feed; } set { _Feed = value; OnPropertyChanged(nameof(Feed)); } }
        private bool _IsGuide;
        public bool IsGuide { get { return _IsGuide; } set { _IsGuide = value; OnPropertyChanged(nameof(IsGuide)); } }
        private string? _Description;
        public string? Description { get { return _Description; } set { _Description = value; OnPropertyChanged(nameof(Description)); } }
        private string? _Color;
        public string? Color { get { return _Color; } set { _Color = value; OnPropertyChanged(nameof(Color)); } }
        private List<PhoneModel> _Phones;
        public List<PhoneModel> Phones { get { return _Phones; } set { _Phones = value; OnPropertyChanged(nameof(Phones)); } }
       
        public override string ToString()
        {
            return FirstName.ToString()+" "+LastName.ToString();
        }

        public ClientModel()
        {

        }
    }
}
