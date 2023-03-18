using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Airfare.ViewModels.UserControlViewModels;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Airfare.Views.UserControls
{
  
    public partial class CompaniesView : UserControl
    {
        public CompaniesView()
        {
            InitializeComponent();
        }

        private void ImageSelector_ImageSelected(object sender, RoutedEventArgs e)
        {
            try
            {
                var imageSelector = sender as ImageSelector;

                BitmapImage Image = new BitmapImage(imageSelector.Uri);
                (DataContext as CompaniesViewModel).Company.Logo = BitmapImagetoByteArray(Image);
            }
            catch (Exception)
            {
                Growl.Error("an error has occurred in ImageSelector_ImageSelected");
            }
           
        }

        private byte[] BitmapImagetoByteArray(BitmapImage bitmapImage)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        private void ImageSelector_ImageUnselected(object sender, RoutedEventArgs e)
        {
            try
            {
                (DataContext as CompaniesViewModel).Company.Logo = null;
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in ImageSelector_ImageUnselected");
            }
            
        }

        private void AddPaymentTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!AddPaymentButton.IsEnabled && e.ClickCount == 1)
            {
                Growl.Warning("عليك اختيار رحلة لتمكين هذه العملية");
            }
        }
    }


}
