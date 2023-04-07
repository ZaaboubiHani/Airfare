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

       
        public async Task<bool> InitData()
        {

            try
            {
                // Get the current available season if not already set in configuration
                Information = "getting current available season";
                if (Configuration.CurrentSeason is null)
                {
                    Configuration.CurrentSeason = await seasonServices.GetFirstActiveSeason();
                }

                // Set the environment template
                Information = "setting the environment template";
                var environment = await environmentServices.GetEnvironment() ?? new EnvironmentModel { ClientContractContent = string.Empty };
                await environmentServices.AddEnvironment(environment);

                // Set the user domain and security
                Information = "setting the user domain and security";
                var user = await userServices.GetUser() ?? new UserModel { Name = "Admin", Password = "Admin", IsAdmin = true };
                await userServices.AddUser(user);
                Configuration.CurrentUser = user;

                // Check device compatibility
                Information = "checking device compatibility";

                string processorId = DeviceManagement.GetProcessorId();
                string pcName = Environment.MachineName;
                var id = System.Configuration.ConfigurationManager.AppSettings["ID"];
                if (string.IsNullOrEmpty(id))
                {
                    id = await controlDeviceService.UploadDevice(new ControlDeviceModel { DeviceId = processorId, ExpirationDate = DateTime.UtcNow.AddMonths(1), MachineName = pcName });
                    if(string.IsNullOrEmpty(id))
                    {
                        var oConfig = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                        oConfig.AppSettings.Settings["ID"].Value = id;
                        oConfig.Save(System.Configuration.ConfigurationSaveMode.Full);
                        System.Configuration.ConfigurationManager.RefreshSection("appSettings");
                    }

                }

                var device = await controlDeviceService.GetDeviceById(id);
                if (device is null)
                {
                    await controlDeviceService.UploadDevice(new ControlDeviceModel { DeviceId = processorId, ExpirationDate = DateTime.UtcNow.AddMonths(1), MachineName = pcName });
                    id = await controlDeviceService.UploadDevice(new ControlDeviceModel { DeviceId = processorId, ExpirationDate = DateTime.UtcNow.AddMonths(1), MachineName = pcName });
                    if (string.IsNullOrEmpty(id))
                    {
                        var oConfig = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                        oConfig.AppSettings.Settings["ID"].Value = id;
                        oConfig.Save(System.Configuration.ConfigurationSaveMode.Full);
                        System.Configuration.ConfigurationManager.RefreshSection("appSettings");
                    }
                }

                // Update configuration settings based on device compatibility
                Information = "Updating configuration settings based on device compatibility";
                if (device != null)
                {
                    var oConfig = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                    var isCompatible = (processorId == "BFEBFBFF000806EA" || processorId == "BFEBFBFF000306C3") && device.ExpirationDate.Date.CompareTo(DateTime.UtcNow.Date) > 0;
                    oConfig.AppSettings.Settings["Accessibility"].Value = isCompatible ? "True" : "False";
                    oConfig.Save(System.Configuration.ConfigurationSaveMode.Full);
                    System.Configuration.ConfigurationManager.RefreshSection("appSettings");
                }

                // Get the accessibility setting from configuration and show appropriate window
                var value = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["Accessibility"]);
                switch (value)
                {
                    case false:
                        new ErrorWindow().Show();
                        break;
                    case true when Configuration.CurrentUser.KeepSigned:
                        new MainWindow().Show();
                        break;
                    case true:
                        new SignupWindow().Show();
                        break;
                }

               
            }
            catch (Exception e)
            {
                // Show error message if initialization fails
                LogService.LogError(e.Message, this);
                HandleInitializationError();
                Growl.ErrorGlobal(e.Message);
            }
            return true;

        }
        private void HandleInitializationError()
        {
            var value = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["Accessibility"]);
            switch (value)
            {
                case false:
                    new ErrorWindow().Show();
                    break;
                case true when Configuration.CurrentUser.KeepSigned:
                    new MainWindow().Show();
                    break;
                case true:
                    new SignupWindow().Show();
                    break;
            }
        }
    }
}
