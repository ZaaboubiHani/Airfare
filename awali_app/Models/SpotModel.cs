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
    [Table("Spots")]
    public class SpotModel:BaseViewModel
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }

        private string _Color;

        public string Color
        {
            get { return _Color; }
            set { _Color = value;
                OnPropertyChanged(nameof(Color));
            }
        }


        private bool _IsEmpty;

        public bool IsEmpty
        {
            get { return _IsEmpty; }
            set { _IsEmpty = value;
                OnPropertyChanged(nameof(IsEmpty));
            }
        }


        private int _Taken;

        public int Taken
        {
            get { return _Taken; }
            set { _Taken = value;
                OnPropertyChanged(nameof(Taken));
            }
        }

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

        private int? _GroupId;

        public int? GroupId
        {
            get { return _GroupId; }
            set { _GroupId = value;
                OnPropertyChanged(nameof(GroupId));
            }
        }


        private GroupModel? _Group;

        public GroupModel? Group
        {
            get { return _Group; }
            set
            {
                _Group = value;
                OnPropertyChanged(nameof(Group));
            }
        }

        private List<HostModel> _Hosts;

        public List<HostModel> Hosts
        {
            get { return _Hosts; }
            set { _Hosts = value;
                OnPropertyChanged(nameof(Hosts));
            }
        }

    }
}
