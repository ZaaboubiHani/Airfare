using Airfare.Commands;
using Airfare.Models;
using Airfare.Servies;
using Airfare.ViewModels.UserControlViewModels;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.ViewModels.DialogViewModels
{
    public class SeasonDialogViewModel : BaseViewModel, IDialogResultable<SeasonModel>,IDisposable
    {
        private SeasonModel _Season;

        public SeasonModel Season
        {
            get { return _Season; }
            set { _Season = value;
                OnPropertyChanged(nameof(Season));
            }
        }


        SeasonModel IDialogResultable<SeasonModel>.Result
        {
            get { return Season; }
            set { Season = value; }
        }

        Action close;

        Action IDialogResultable<SeasonModel>.CloseAction
        {
            get { return close; }
            set { close = value; }
        }


        private bool _UpdateMode;

        public bool UpdateMode
        {
            get { return _UpdateMode; }
            set
            {
                _UpdateMode = value;
                OnPropertyChanged(nameof(UpdateMode));
            }
        }

        #region Services
        private readonly SeasonServices seasonServices = new();
        #endregion


        #region Commands
        public RelayCommand CloseDialogCommand { get; set; }
        public RelayCommand UpdateSeasonCommand { get; set; }
        public RelayCommand AddSeasonCommand { get; set; }
        #endregion


        public SeasonDialogViewModel()
        {
            InitData();
            InitCommands();
        }

        private void InitCommands()
        {
            CloseDialogCommand = new RelayCommand(CloseDialog);
            UpdateSeasonCommand = new RelayCommand(UpdateSeason);
            AddSeasonCommand = new RelayCommand(AddSeason);
        }

        private async void AddSeason()
        {
            Season.StartDate = DateTime.Now;
            await seasonServices.AddSeason(Season);

            if (!seasonServices.Error)
            {
                Growl.Success("تمت إضافة الموسم بنجاح");
                close.Invoke();
            }
            else
            {
                Growl.Error("\n:خطأ\n" + seasonServices.ErrorMessage);
            }
        }

        private void InitData()
        {
           
        }

        private async void UpdateSeason()
        {
            await seasonServices.UpdateSeason(Season);

            if (!seasonServices.Error)
            {
                Growl.Success("تمت تعديل الموسم بنجاح");
                close.Invoke();
            }
            else
            {
                Growl.Error("\n:خطأ\n" + seasonServices.ErrorMessage);
            }
        }

        private void CloseDialog()
        {
            Season = null;
            close.Invoke();
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
