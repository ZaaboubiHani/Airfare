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
                    Configuration.CurrentSeason = await seasonServices.GetFirstActiveSeason();
                }
                EnableFeatures = true;
            }
            catch (Exception e)
            {

                LogService.LogError(e.Message, this);
                EnableFeatures = true;
                Growl.Error("An error occurred while trying to get all the seaons in the main window");
            }
            
           
        }
    }
}
