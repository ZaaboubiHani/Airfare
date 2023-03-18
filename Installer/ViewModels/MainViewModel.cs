using Installer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using HandyControl.Controls;
using Installer.ViewModels.UserControlViewModels;
using System.Windows;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using Installer.Models;
using System.Reflection;

namespace Installer.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        private string _DiscoveryMessage;

        public string DiscoveryMessage
        {
            get { return _DiscoveryMessage; }
            set {
                _DiscoveryMessage = value;
                OnPropertyChanged(nameof(DiscoveryMessage));
            }
        }

        private bool _Checkingliability;

        public bool Checkingliability
        {
            get { return _Checkingliability; }
            set { _Checkingliability = value;
                OnPropertyChanged(nameof(Checkingliability));
            }
        }

        private bool _Installable;

        public bool Installable
        {
            get { return _Installable; }
            set { _Installable = value;
                OnPropertyChanged(nameof(Installable));
            }
        }

        public RelayCommand InstallCommand { get; set; }
        public RelayCommand UninstallCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }

        public MainViewModel()
        {
            InstallCommand = new RelayCommand(Install);
            UninstallCommand = new RelayCommand(Uninstall);
            UpdateCommand = new RelayCommand(Update);
            InitData();
        }

        private async void Update()
        {
            await UninstallAxiaAirfare();
            await InstallAxiaAirfare();
        }

        private async void Uninstall()
        {
            await UninstallAxiaAirfare();
            await UninstallSQLServer();
            InitData();
            DiscoveryMessage = "Uninstallation complete";
        }

        private async void Install()
        {
           
            //await InstallSQLServer();
            
            await InstallAxiaAirfare();
            InitData();
            DiscoveryMessage = "Installation complete";
        }

        private async void InitData()
        {
            await Task.Run(() =>
            {
                
                Checkingliability = true;
                var apps = GetFullListInstalledApplication();
                var airfare = apps.Where(app => app.DisplayName.ToLower().Contains("airfare")).FirstOrDefault();
                
                if (airfare != null && SQLExist())
                {
                    Installable = false;
                }
                else
                {
                    Installable = true;
                }
                Checkingliability = false;
            });

        }

        private async Task UninstallSQLServer()
        {
            await Task.Run(() =>
            {
                var startupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                System.Diagnostics.Process processObj = new System.Diagnostics.Process();
                processObj.StartInfo.FileName = startupPath + "\\SQLEXPR_x64_ENU.exe";
                processObj.StartInfo.Arguments = @" /ACTION=Uninstall /FEATURES=SQLENGINE /INSTANCENAME=""SQLEXPRESS2022"" /q";

                processObj.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processObj.StartInfo.UseShellExecute = true;
                processObj.EnableRaisingEvents = true;
                processObj.Start();
                //Loop until the process has exited

                DiscoveryMessage = "Uninstalling sql server \\";
                do
                {
                    if (DiscoveryMessage == "Uninstalling sql server \\")
                    {
                        DiscoveryMessage = "Uninstalling sql server |";
                    }
                    else if (DiscoveryMessage == "Uninstalling sql server |")
                    {
                        DiscoveryMessage = "Uninstalling sql server /";
                    }
                    else if (DiscoveryMessage == "Uninstalling sql server /")
                    {
                        DiscoveryMessage = "Uninstalling sql server -";
                    }
                    else if (DiscoveryMessage == "Uninstalling sql server -")
                    {
                        DiscoveryMessage = "Uninstalling sql server \\";
                    }
                    else
                    {
                        DiscoveryMessage = "Uninstalling sql server \\";
                    }
                   
                    processObj.Refresh();

                } while (!processObj.WaitForExit(100));

                if (processObj.ExitCode == 0)
                {
                    DiscoveryMessage = "sql server app uninstalled successfuly";
                }
                else
                {
                    DiscoveryMessage = "sql server uninstallation failed" + processObj.ExitCode;
                }
                // Release resources.
                processObj.Close();
                processObj.Dispose();
            });
        }

        private async Task UninstallAxiaAirfare()
        {
            await Task.Run(() =>
            {
                // Create the uninstall process.
                Process processObj = new Process();

                // Define the parameters for the process
                processObj.StartInfo.FileName = "msiexec.exe";

                // This is the Product Code from your Basic MSI InstallShield Project
                string strProdGUID = "{6F86DE4B-66B1-48FA-925F-92E3E651CE02}";

                processObj.StartInfo.Arguments = "/uninstall" + " " + strProdGUID + " " + "/q";

                // Start the process.
                processObj.Start();
                // Wait for the uninstall process to end.
                DiscoveryMessage = "Uninstalling Axia Airfare app \\";
                do
                {
                    if (DiscoveryMessage == "Uninstalling Axia Airfare app \\")
                    {
                        DiscoveryMessage = "Uninstalling Axia Airfare app |";
                    }
                    else if (DiscoveryMessage == "Uninstalling Axia Airfare app |")
                    {
                        DiscoveryMessage = "Uninstalling Axia Airfare app /";
                    }
                    else if (DiscoveryMessage == "Uninstalling Axia Airfare app /")
                    {
                        DiscoveryMessage = "Uninstalling Axia Airfare app -";
                    }
                    else if (DiscoveryMessage == "Uninstalling Axia Airfare app -")
                    {
                        DiscoveryMessage = "Uninstalling Axia Airfare app \\";
                    }
                    else
                    {
                        DiscoveryMessage = "Uninstalling Axia Airfare app \\";
                    }
                    //refresh the process
                    processObj.Refresh();
                } while (!processObj.WaitForExit(100));

                if (processObj.ExitCode == 0)
                {
                    DiscoveryMessage = "airfare app uninstalled successfuly";
                }
                else
                {
                    DiscoveryMessage = "airfare uninstallation failed" + processObj.ExitCode;
                }
                // Release resources.
                processObj.Close();
                processObj.Dispose();

            });
        }

        private async Task InstallAxiaAirfare()
        {
            await Task.Run(() =>
            {
                var startupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                System.Diagnostics.Process processObj = new System.Diagnostics.Process();
                processObj.StartInfo.FileName = startupPath + "\\setup.exe";
                processObj.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processObj.EnableRaisingEvents = true;
                processObj.StartInfo.Arguments = @"TARGETDIR=""C:\Program Files\Axia\Aifare\"" /qb";
                processObj.Start();
               
                //Loop until the process has exited
                DiscoveryMessage = "Installing Axia Airfare app \\";
                do
                {
                    if (DiscoveryMessage == "Installing Axia Airfare app \\")
                    {
                        DiscoveryMessage = "Installing Axia Airfare app |";
                    }
                    else if (DiscoveryMessage == "Installing Axia Airfare app |")
                    {
                        DiscoveryMessage = "Installing Axia Airfare app /";
                    }
                    else if (DiscoveryMessage == "Installing Axia Airfare app /")
                    {
                        DiscoveryMessage = "Installing Axia Airfare app -";
                    }
                    else if (DiscoveryMessage == "Installing Axia Airfare app -")
                    {
                        DiscoveryMessage = "Installing Axia Airfare app \\";
                    }
                    else
                    {
                        DiscoveryMessage = "Installing Axia Airfare app \\";
                    }
                    processObj.Refresh();
                } while (!processObj.WaitForExit(100));
                
                if (processObj.ExitCode == 0)
                {
                    DiscoveryMessage = "airfare app installed successfuly";
                }
                else
                {
                    DiscoveryMessage = "airfare installation failed" + processObj.ExitCode;
                }
                // Release resources.
                processObj.Close();
                processObj.Dispose();
            });
        }
      
        private async Task InstallSQLServer()
        {
            await Task.Run(() =>
            {
                var startupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                System.Diagnostics.Process processObj = new System.Diagnostics.Process();
                processObj.StartInfo.FileName = startupPath + "\\SQLEXPR_x64_ENU.exe";
                processObj.StartInfo.Arguments = @" /ACTION=Install /IACCEPTSQLSERVERLICENSETERMS=true /INSTANCENAME=SQLEXPRESS2022 /q";

                processObj.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processObj.StartInfo.UseShellExecute = true;
                processObj.EnableRaisingEvents = true;
                processObj.Start();
                //Loop until the process has exited

                DiscoveryMessage = "installing sql server express on this device \\";
                do
                {
                    if(DiscoveryMessage == "installing sql server express on this device \\")
                    {
                        DiscoveryMessage = "installing sql server express on this device |";
                    }
                    else if (DiscoveryMessage == "installing sql server express on this device |")
                    {
                        DiscoveryMessage = "installing sql server express on this device /";
                    }
                    else if (DiscoveryMessage == "installing sql server express on this device /")
                    {
                        DiscoveryMessage = "installing sql server express on this device -";
                    }
                    else if (DiscoveryMessage == "installing sql server express on this device -")
                    {
                        DiscoveryMessage = "installing sql server express on this device \\";
                    }
                    else
                    {
                        DiscoveryMessage = "installing sql server express on this device \\";
                    }
                    //refresh the process
                    processObj.Refresh();
                    
                } while (!processObj.WaitForExit(100));

                if (processObj.ExitCode == 0)
                {
                    DiscoveryMessage = "sql sever installed successfuly";
                }
                else
                {
                    DiscoveryMessage = "sql sever installation failed" + processObj.ExitCode;
                }
                // Release resources.
                processObj.Close();
                processObj.Dispose();
            });
        }


       
        

        private static List<AppModel> GetInstalledApplication(RegistryKey regKey, string registryKey)
        {
            List<AppModel> list = new();
            using (RegistryKey key = regKey.OpenSubKey(registryKey))
            {
                if (key != null)
                {
                    foreach (string name in key.GetSubKeyNames())
                    {
                        using (RegistryKey subkey = key.OpenSubKey(name))
                        {
                            string displayName = (string)subkey.GetValue("DisplayName");
                            string installLocation = (string)subkey.GetValue("InstallLocation");
                            string appVersion = (string)subkey.GetValue("DisplayVersion");
                            string guid = (string)subkey.GetValue("ProductId");

                            if (!string.IsNullOrEmpty(displayName)) // && !string.IsNullOrEmpty(installLocation)
                            {
                                list.Add(new AppModel()
                                {
                                    DisplayName = displayName.Trim(),
                                    InstallationLocation = installLocation,
                                    AppVersion = appVersion,
                                    Id = guid
                                });
                            }
                        }
                    }
                }
            }

            return list;
        }

        private static List<AppModel> GetFullListInstalledApplication()
        {
            IEnumerable<AppModel> finalList = new List<AppModel>();

            string registry_key_32 = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            string registry_key_64 = @"SOFTWARE\WoW6432Node\Microsoft\Windows\CurrentVersion\Uninstall";

            List<AppModel> win32AppsCU = GetInstalledApplication(Registry.CurrentUser, registry_key_32);
            List<AppModel> win32AppsLM = GetInstalledApplication(Registry.LocalMachine, registry_key_32);
            List<AppModel> win64AppsCU = GetInstalledApplication(Registry.CurrentUser, registry_key_64);
            List<AppModel> win64AppsLM = GetInstalledApplication(Registry.LocalMachine, registry_key_64);

            finalList = win32AppsCU.Concat(win32AppsLM).Concat(win64AppsCU).Concat(win64AppsLM);

            finalList = finalList.GroupBy(d => d.DisplayName).Select(d => d.First());

            return finalList.ToList();
        }

        private bool SQLExist()
        {
            var exist = false;
            RegistryView registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
            {
                RegistryKey instanceKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL", false);
                if (instanceKey != null)
                {
                    foreach (var instanceName in instanceKey.GetValueNames())
                    {
                        if(instanceName == "SQLEXPRESS")
                        {
                            exist = true;
                            break;
                        }
                    }
                    
                }
            }
            return exist;
        }


    }
}
