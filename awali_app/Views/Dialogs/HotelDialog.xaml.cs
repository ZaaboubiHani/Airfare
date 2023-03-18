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
    /// Interaction logic for AddHotelDialog.xaml
    /// </summary>
    public partial class HotelDialog
    {
        public HotelDialog()
        {
            InitializeComponent();
        }

        private void Rate_ValueChanged(object sender, HandyControl.Data.FunctionEventArgs<double> e)
        {
            try
            {
                if (((HotelDialogViewModel)DataContext).Hotel is not null)
                {
                    ((HotelDialogViewModel)DataContext).Hotel.Rate = (int)rate.Value;
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in Rate_ValueChanged");
            }
           
        }
    }
}
