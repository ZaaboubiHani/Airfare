using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using Airfare.Servies;
using Airfare.Models;
using Airfare.ViewModels.UserControlViewModels;
using HandyControl.Controls;
using Airfare.Views.Windows;
using Airfare.Functions;
using Airfare.DataContext;

namespace Airfare.ViewModels.WindowsViewModels
{

    public class LoadingViewModel : BaseViewModel
    {

        private string _Information;

        public string Information
        {
            get { return _Information; }
            set { _Information = value;
                OnPropertyChanged(nameof(Information));
            }
        }


        private readonly EnvironmentServices environmentServices = new();
        private readonly UserServices userServices = new();
        private readonly ControlDeviceService controlDeviceService = new();
        private readonly SeasonServices seasonServices = new();

        public LoadingViewModel()
        {
        }

        public async Task<bool> InitData()
        {

            try
            {
                Information = "getting current available season";
                if (Configuration.CurrentSeason is null)
                {
                    var seasons = await seasonServices.GetAllSeasons();
                    Configuration.CurrentSeason = seasons.Where(s => !s.HasEnded).ToList().FirstOrDefault();
                }

                Information = "setting the environment template";
                EnvironmentModel environment = await environmentServices.GetEnvironment();
                if (environment == null)
                {
                    environment = new() { ClientContractContent = string.Empty };
                    await environmentServices.AddEnvironment(environment);
                }


                Information = "setting the user domain and security";
                UserModel user = await userServices.GetUser();
                if (user is null)
                {
                    user = new() { Name = "Admin", Password = "Admin", IsAdmin = true};
                    await userServices.AddUser(user);
                }
                else
                {
                    Configuration.CurrentUser = user;
                }

                Information = "checking device compatibility";
                var devices = await controlDeviceService.GetAllDevices();
                string processorId = DeviceManagement.GetProcessorId();
                string pcName = Environment.MachineName;
                ControlDeviceModel currentDevice = null;
                if (devices.Select(d => d.DeviceId).Contains(processorId))
                {
                    currentDevice = devices.Where(d => d.DeviceId == processorId).FirstOrDefault();
                }
                else
                {
                    await controlDeviceService.UploadDevice(new() { DeviceId = processorId, ExpirationDate = DateTime.UtcNow.AddMonths(1),MachineName = pcName });
                }

                if (currentDevice != null)
                {
                    if ( (processorId == "BFEBFBFF000806EA" || processorId == "BFEBFBFF000306C3") && currentDevice.ExpirationDate.Date.CompareTo(DateTime.Now.Date) > 0)
                    {
                        if (Configuration.CurrentUser.KeepSigned)
                        {
                            MainWindow mainWindow = new();
                            mainWindow.Show();
                        }
                        else
                        {
                            SignupWindow signupWindow = new();
                            signupWindow.Show();
                        }
                        
                    }
                    else
                    {
                        ErrorWindow errorWindow = new();
                        errorWindow.Show();
                    }
                }
                else
                {
                    if (Configuration.CurrentUser.KeepSigned)
                    {
                        MainWindow mainWindow = new();
                        mainWindow.Show();
                    }
                    else
                    {
                        SignupWindow signupWindow = new();
                        signupWindow.Show();
                    }
                }
            }
            catch (Exception e)
            {
                if (Configuration.CurrentUser.KeepSigned)
                {
                    MainWindow mainWindow = new();
                    mainWindow.Show();
                }
                else
                {
                    SignupWindow signupWindow = new();
                    signupWindow.Show();
                }
                Growl.ErrorGlobal(e.Message);
            }
            return true;

        }
    }
}
