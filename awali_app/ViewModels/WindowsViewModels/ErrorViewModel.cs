using HandyControl.Controls;
using Airfare.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airfare.ViewModels.UserControlViewModels;

namespace Airfare.ViewModels.WindowsViewModels
{
    public class ErrorViewModel:BaseViewModel
    {

        private string _ErrorCode;

        public string ErrorCode
        {
            get { return _ErrorCode; }
            set
            {
                _ErrorCode = value;
                OnPropertyChanged(nameof(_ErrorCode));
            }
        }

        public ErrorViewModel()
        {
            InitData();
        }

        public async Task InitData()
        {
            try
            {
                string macAdress = DeviceManagement.GetMacAddress();
                string processorId = DeviceManagement.GetProcessorId();
                string serialNumber = DeviceManagement.GetSerialNumber();
                if (!string.IsNullOrEmpty(processorId) && !string.IsNullOrEmpty(macAdress) && !string.IsNullOrEmpty(serialNumber))
                {
                    var folder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\";
                    ErrorCode = "\n" + macAdress + "\n" + processorId + "\n" + serialNumber;
                    string fileName = "ErrorCode.txt";
                    string fullPath = folder + fileName;
                    string[] authors = { macAdress, processorId, serialNumber };
                    File.WriteAllLines(fullPath, authors);
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has ocurred while loading data");
            }
        }
    }
}
