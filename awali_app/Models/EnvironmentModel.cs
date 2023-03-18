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
    [Table("Environments")]
    public class EnvironmentModel:BaseViewModel
    {
        [Key]
        public int Id { get; set; }

        private string? _UserName;

        public string? UserName
        {
            get { return _UserName; }
            set { _UserName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private string? _ClientContractContent;

        public string? ClientContractContent
        {
            get { return _ClientContractContent; }
            set { _ClientContractContent = value;
                OnPropertyChanged(nameof(ClientContractContent));
            }
        }

        private string? _HeaderSource;

        public string? HeaderSource
        {
            get { return _HeaderSource; }
            set { _HeaderSource = value;
                OnPropertyChanged(nameof(HeaderSource));
            }
        }

        private string? _FooterSource;

        public string? FooterSource
        {
            get { return _FooterSource; }
            set { _FooterSource = value;
                OnPropertyChanged(nameof(FooterSource));
            }
        }
        private bool _KeepSigned;

        public bool KeepSigned
        {
            get { return _KeepSigned; }
            set { _KeepSigned = value;
                OnPropertyChanged(nameof(KeepSigned));
            }
        }


    }
}
