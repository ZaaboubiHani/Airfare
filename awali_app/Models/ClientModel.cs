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
    public class ClientModel : BaseModel
    {
        [Key]
        private int _id;
        public int Id { get => _id; set => SetProperty(ref _id, value); }

        private string _firstName;
        public string FirstName { get => _firstName; set => SetProperty(ref _firstName, value); }

        private string _lastName;
        public string LastName { get => _lastName; set => SetProperty(ref _lastName, value); }

        private DateTime _birthDate;
        public DateTime BirthDate { get => _birthDate; set => SetProperty(ref _birthDate, value); }

        private string? _passportNumber;
        public string? PassportNumber { get => _passportNumber; set => SetProperty(ref _passportNumber, value); }

        private string? _healthStatus;
        public string? HealthStatus { get => _healthStatus; set => SetProperty(ref _healthStatus, value); }

        private bool _gender;
        public bool Gender { get => _gender; set => SetProperty(ref _gender, value); }

        private bool _isMinor;
        public bool IsMinor { get => _isMinor; set => SetProperty(ref _isMinor, value); }

        private bool _feed;
        public bool Feed { get => _feed; set => SetProperty(ref _feed, value); }

        private bool _isGuide;
        public bool IsGuide { get => _isGuide; set => SetProperty(ref _isGuide, value); }

        private string? _description;
        public string? Description { get => _description; set => SetProperty(ref _description, value); }

        private string? _color;
        public string? Color { get => _color; set => SetProperty(ref _color, value); }

        private List<PhoneModel> _phones = new List<PhoneModel>();
        public List<PhoneModel> Phones { get => _phones; set => SetProperty(ref _phones, value); }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
