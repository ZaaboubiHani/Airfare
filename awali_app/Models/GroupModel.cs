using Airfare.ViewModels.UserControlViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Models
{
    [Table("Groups")]
    public class GroupModel : BaseViewModel,ICloneable
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        private bool _Selected;
        [NotMapped]
        public bool Selected
        {
            get { return _Selected; }
            set
            {
                _Selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }

        private string _Color;
        public string Color { get { return _Color; } set { _Color = value; OnPropertyChanged(nameof(Color)); } }

        private List<SpotModel> _Spots;
        public List<SpotModel> Spots
        {
            get { return _Spots; }
            set
            {
                _Spots = value;
                OnPropertyChanged(nameof(Spots));
            }
        }

        public object Clone()
        {
            return new GroupModel
            {
                Id = this.Id,
                Color = this.Color,
                Selected = this.Selected,
                Spots = this.Spots?.Select(s => (SpotModel)s.Clone()).ToList(),
            };
        }

    }
}
