using Airfare.ViewModels.DialogViewModels;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Airfare.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for AddClientDialog.xaml
    /// </summary>
    public partial class ClientDialog
    {
        public ClientDialog()
        {
            InitializeComponent();
        }

        private void CompanyComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((DataContext as ClientDialogViewModel).UpdateMode)
                {
                    (DataContext as ClientDialogViewModel).SelectedCompany = (DataContext as ClientDialogViewModel).hiddenCompanySelector;
                }
            }
            catch (Exception)
            {
                Growl.Error("an error has occurred in CompanyComboBox_Loaded");
            }
            
        }

        private void clientDialogUI_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Escape)
                {
                    (DataContext as ClientDialogViewModel).CloseDialogCommand.Execute(null);
                    e.Handled = true;
                }
                else if (e.Key == Key.S && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                {
                    if ((DataContext as ClientDialogViewModel).UpdateMode)
                    {
                        (DataContext as ClientDialogViewModel).UpdateClientCommand.Execute(null);
                    }
                    else
                    {
                        (DataContext as ClientDialogViewModel).AddClientCommand.Execute(null);
                    }
                    e.Handled = true;
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in clientDialogUI_PreviewKeyDown");
            }
           
        }

        private void PhoneTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    (DataContext as ClientDialogViewModel).AddPhoneCommand.Execute(null);
                    PhoneTextBox.Focus();
                    e.Handled = true;
                }
                else if (e.Key == Key.Tab)
                {
                    PaymentTextBox.Focus();
                    e.Handled = true;
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in PhoneTextBox_PreviewKeyDown");
            }
           
        }

        private void PaymentTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    (DataContext as ClientDialogViewModel).AddPaymentCommand.Execute(null);
                    PaymentTextBox.Focus();
                    e.Handled = true;
                }
                else if (e.Key == Key.Tab)
                {
                    CompanyComboBox.Focus();
                    e.Handled = true;
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in PaymentTextBox_PreviewKeyDown");
            }
           
        }


        private void CompanyComboBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Tab)
                {
                    ColorPicker.Focus();
                    e.Handled = true;
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in CompanyComboBox_PreviewKeyDown");
            }
          
        }

        private void ColorPicker_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Tab)
                {
                    FirstNameTextBox.Focus();
                    e.Handled = true;
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in ColorPicker_PreviewKeyDown");
            }
           
        }

        private void FirstNameTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Tab)
                {
                    LastNameTextBox.Focus();
                    e.Handled = true;
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in FirstNameTextBox_PreviewKeyDown");
            }
           
        }

        private void LastNameTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Tab)
                {
                    DatePicker.Focus();
                    e.Handled = true;
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in LastNameTextBox_PreviewKeyDown");
            }
           
        }

        private void DatePicker_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Tab)
                {
                    PassportNumberTextBox.Focus();
                    e.Handled = true;
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in DatePicker_PreviewKeyDown");
            }
           
        }

        private void PassportNumberTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Tab)
                {
                    HotelsComboBox.Focus();
                    e.Handled = true;
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in PassportNumberTextBox_PreviewKeyDown");
            }
           
        }

        private void HotelsComboBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Tab)
                {
                    RoomsComboBox.Focus();
                    e.Handled = true;
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in HotelsComboBox_PreviewKeyDown");
            }
           
        }

        private void RoomsComboBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Tab)
                {
                    GenderComboBox.Focus();
                    e.Handled = true;
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in RoomsComboBox_PreviewKeyDown");
            }
           
        }

        private void GenderComboBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Tab)
                {
                    HealthStatusTextBox.Focus();
                    e.Handled = true;
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in GenderComboBox_PreviewKeyDown");
            }
           
        }

        private void HealthStatusTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Tab)
                {
                    DiscountTextBox.Focus();
                    e.Handled = true;
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in HealthStatusTextBox_PreviewKeyDown");
            }
            
        }

        private void DiscountTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Tab)
                {
                    DescriptionTextBox.Focus();
                    e.Handled = true;
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in DiscountTextBox_PreviewKeyDown");
            }
            
        }

        private void DescriptionTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Tab)
                {
                    PhoneTextBox.Focus();
                    e.Handled = true;
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in DescriptionTextBox_PreviewKeyDown");
            }
           
        }

        private void clientDialogUI_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                FirstNameTextBox.Focus();
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in clientDialogUI_Loaded");
            }
            
            
        }
    }
}