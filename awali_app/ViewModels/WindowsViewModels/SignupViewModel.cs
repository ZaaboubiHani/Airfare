using Airfare.Commands;
using Airfare.DataContext;
using Airfare.Models;
using Airfare.Servies;
using Airfare.ViewModels.UserControlViewModels;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Airfare.ViewModels.WindowsViewModels
{
    public class SignupViewModel:BaseViewModel
    {
        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private string _UserPassword;

        public string UserPassword
        {
            get { return _UserPassword; }
            set { _UserPassword = value;
                OnPropertyChanged(nameof(UserPassword));
            }
        }

        private bool _KeepMeSigned;

        public bool KeepMeSigned
        {
            get { return _KeepMeSigned; }
            set { _KeepMeSigned = value;
                OnPropertyChanged(nameof(KeepMeSigned));
            }
        }

        private readonly UserServices userServices = new();


        public RelayCommand ExitAppCommand { get; set; }

        public SignupViewModel()
        {
            ExitAppCommand = new RelayCommand(ExitApp);
        }

        public async Task<bool> Signup()
        {
            try
            {
                if(Configuration.CurrentUser.Name == UserName)
                {
                    if (Configuration.CurrentUser.Password == UserPassword)
                    {
                        if(KeepMeSigned == true)
                        {
                            Configuration.CurrentUser.KeepSigned = true;
                            await userServices.UpdateUser(Configuration.CurrentUser);
                        }

                        return true;
                    }
                    else
                    {
                        Growl.Warning("كلمة المرور غير صحيحة");
                    }
                }
                else
                {
                    Growl.Warning("اسم المستخدم غير صحيح");
                }
            }
            catch (Exception)
            {

                Growl.Error("an error occurred while signing in");
            }
            return false;
        }

        private void ExitApp()
        {
            Application.Current.Shutdown();
        }
    }
}
