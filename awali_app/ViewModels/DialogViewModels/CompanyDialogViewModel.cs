using Airfare.Commands;
using Airfare.Models;
using Airfare.ViewModels.UserControlViewModels;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Airfare.ViewModels.DialogViewModels
{
    public class CompanyDialogViewModel : BaseViewModel, IDialogResultable<CompanyModel>,IDisposable
    {
        #region Properties
        private CompanyModel _Company;

        public CompanyModel Company
        {
            get { return _Company; }
            set
            {
                _Company = value;
                OnPropertyChanged(nameof(Company));
                
                
            }
        }

        CompanyModel IDialogResultable<CompanyModel>.Result
        {
            get { return Company; }
            set { Company = value; }
        }

        Action close;

        Action IDialogResultable<CompanyModel>.CloseAction
        {
            get { return close; }
            set { close = value; }
        }
        #endregion

        #region Commands
        public RelayCommand CloseDialogCommand { get; set; }
        public RelayCommand AddCompanyCommand { get; set; }
        #endregion

        public CompanyDialogViewModel()
        {
            CloseDialogCommand = new RelayCommand(CloseDialog);
            AddCompanyCommand = new RelayCommand(AddCompany);
        }

        #region Methods
        private void AddCompany()
        {
            try
            {
                close.Invoke();
            }
            catch (Exception)
            {

                Growl.Error("An error has occurred while passing the client object");
            }
        }

        private void CloseDialog()
        {
            try
            {
                Company = null;
                close.Invoke();
            }
            catch (Exception)
            {
                Growl.Error("An error has occurred while closing client dialog");
            }
            
        }

        public void Dispose()
        {
            GC.Collect();
        }
        #endregion
    }
}
