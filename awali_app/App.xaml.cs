using Airfare.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Airfare
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {


            //string macAdress = DeviceManagement.GetMacAddress();
            string processorId = DeviceManagement.GetProcessorId();
            string serialNumber = DeviceManagement.GetSerialNumber();
            DateTime dateTime = new DateTime(2023,3,23);
            if ((!string.IsNullOrEmpty(processorId) && processorId == "BFEBFBFF000806EA" && !string.IsNullOrEmpty(serialNumber) && serialNumber == "/66LFKR2/CNCMC0088D0299/" && dateTime.Date.CompareTo(DateTime.Now.Date) > 0) || (!string.IsNullOrEmpty(processorId) && processorId == "BFEBFBFF000306C3" && dateTime.Date.CompareTo(DateTime.Now.Date) > 0))
            {
                base.OnStartup(e);
            }
            else
            {
                StartupUri = new Uri(@"Views/Windows/ErrorWindow.xaml", UriKind.RelativeOrAbsolute);
                base.OnStartup(e);

            }
        }
    }
}
