using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Airfare.Models
{
    [Table("Seasons")]
    public class SeasonModel : BaseModel,ICloneable
    {
        [Key]
        private int _id;
        public int Id { get => _id; set => SetProperty(ref _id, value); }

        private string _name;
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        private DateTime _startDate;
        public DateTime StartDate { get => _startDate; set => SetProperty(ref _startDate, value); }

        private DateTime? _endDate;
        public DateTime? EndDate { get => _endDate; set => SetProperty(ref _endDate, value); }

        private bool _hasEnded;
        public bool HasEnded { get => _hasEnded; set => SetProperty(ref _hasEnded, value); }

        public override string ToString()
        {
            return Name;
        }

        public object Clone()
        {
            return new SeasonModel
            {
                Id = this.Id,
                Name = this.Name,
                StartDate = this.StartDate,
                EndDate = this.EndDate,
                HasEnded = this.HasEnded
            };
        }

    }
}

