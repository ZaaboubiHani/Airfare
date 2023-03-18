using Airfare.Commands;
using Airfare.Models;
using Airfare.Servies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Airfare.ViewModels.WindowsViewModels
{
    public class SignupViewModel
    {
        private readonly EnvironmentServices environmentServices = new();
        public RelayCommand ExitAppCommand { get; set; }
        public SignupViewModel()
        {
            InitData();
            ExitAppCommand = new RelayCommand(ExitApp);
        }
        private async void InitData()
        {
            try
            {
                
                EnvironmentModel environment = await environmentServices.GetEnvironment();
                if(environment == null)
                {
                    environment = new() { ClientContractContent = string.Empty };
                    await environmentServices.AddEnvironment(environment);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void ExitApp()
        {
            Application.Current.Shutdown();
        }
    }
}
