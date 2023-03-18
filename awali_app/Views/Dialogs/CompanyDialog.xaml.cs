using Airfare.ViewModels.DialogViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
    /// Interaction logic for AddCompanyDialog.xaml
    /// </summary>
    public partial class CompanyDialog 
    {
        public CompanyDialog()
        {
            InitializeComponent();
            
        }

        private void ImageSelector_ImageUnselected(object sender, RoutedEventArgs e)
        {

        }

        private void ImageSelector_ImageSelected(object sender, RoutedEventArgs e)
        {

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

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        private void ImageSelector_Loaded(object sender, RoutedEventArgs e)
        {
            var DC = (CompanyDialogViewModel)DataContext;
            var image = LoadImage(DC.Company.Logo);
        }
    }
}
