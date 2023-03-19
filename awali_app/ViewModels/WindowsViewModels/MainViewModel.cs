using System;
using System.Linq;
using Airfare.DataContext;
using Airfare.Servies;
using Airfare.ViewModels.UserControlViewModels;
using HandyControl.Controls;

namespace Airfare.ViewModels.WindowsViewModels
{
    public class MainViewModel:BaseViewModel
    {
        private bool _EnableFeatures;

        public bool EnableFeatures
        {
            get { return _EnableFeatures; }
            set { _EnableFeatures = value;
                OnPropertyChanged(nameof(EnableFeatures));
            }
        }

        readonly private SeasonServices seasonServices = new();

        public MainViewModel()
        {
            InitData();
        }
        private async void InitData()
        {
            try
            {
                if(Configuration.CurrentSeason is null)
                {
                    var seasons = await seasonServices.GetAllSeasons();
                    Configuration.CurrentSeason = seasons.Where(s => !s.HasEnded).ToList().FirstOrDefault();
                }
                EnableFeatures = true;
            }
            catch (Exception)
            {
                EnableFeatures = true;
                Growl.Error("An error occurred while trying to get all the seaons in the main window");
            }
            
           
        }
    }
}
