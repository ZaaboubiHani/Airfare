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
using Airfare.Models;
using Airfare.ViewModels;
using Airfare.ViewModels.DialogViewModels;
using HandyControl.Controls;
using HandyControl.Tools.Extension;

namespace Airfare.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for AddFlightDialog.xaml
    /// </summary>
    public partial class FlightDialog
    {
        public FlightDialog()
        {
            InitializeComponent();
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HotelModel hotel = listViewHotels.SelectedItems.Cast<HotelModel>().ToList().FirstOrDefault();
                if (hotel is not null)
                {

                    var DC = (FlightDialogViewModel)DataContext;

                    DC.HotelsList.Remove(hotel);
                    var flighthotel = new FlightHotelModel { Hotel = hotel, HotelId = hotel.Id };
                    DC.SelectedFlightHotelsList.Add(flighthotel);
                    for (int i = 0; i < DC.Rooms.Count; i++)
                    {
                        DC.HotelRooms.Add(new HotelRoomModel() { FlightHotel = flighthotel, Room = DC.Rooms[i], RoomId = DC.Rooms[i].Id });
                    }
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred, while trying to switch the hotel");
            }
           
            
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var flightHotel = listBoxHotels.SelectedItems.Cast<FlightHotelModel>().ToList().FirstOrDefault();
                if (flightHotel is not null)
                {

                    var DC = (FlightDialogViewModel)DataContext;

                    FlightHotelModel selectedflightHotelModel = flightHotel;
                    DC.DisplayedHotelsRooms.Clear();
                    DC.HotelRooms.RemoveAll(hr => hr.FlightHotelId == selectedflightHotelModel.Id);
                    DC.HotelsList.Add(selectedflightHotelModel.Hotel);
                    DC.SelectedFlightHotelsList.Remove(selectedflightHotelModel);
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred, while trying to switch the hotel");
            }
            
            
        }

        private void DepartTimePicker_SelectedDateTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            var DC = (FlightDialogViewModel)DataContext;
            DC.Flight.DepartTime = DepartTimePicker.SelectedDateTime.Value.TimeOfDay;
        }

        private void ReturnTimePicker_SelectedDateTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            var DC = (FlightDialogViewModel)DataContext;
            DC.Flight.ReturnTime = ReturnTimePicker.SelectedDateTime.Value.TimeOfDay;
        }

        private void DepartTimePicker_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var DC = (FlightDialogViewModel)DataContext;
                DepartTimePicker.SelectedDateTime = new DateTime() + DC.Flight.DepartTime;

            }
            catch (Exception)
            {
                Growl.Error("an error has occurred, while trying to load the DepartTime");
            }
        }

        private void ReturnTimePicker_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var DC = (FlightDialogViewModel)DataContext;
                ReturnTimePicker.SelectedDateTime = new DateTime() + DC.Flight.ReturnTime;

            }
            catch (Exception)
            {
                Growl.Error("an error has occurred, while trying to load the ReturnTime");
            }
        }

        private void CategoryPanel_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var DC = (FlightDialogViewModel)DataContext;
                if(DC.Flight.Category != 0)
                {
                    if (DC.Flight.Category == 1)
                    {
                        AHOptionalRadioButton.IsChecked = true;
                    }
                    else
                    {
                        TKOptionalRadioButton.IsChecked = true;
                    }
                }

            }
            catch (Exception)
            {
                Growl.Error("an error has occurred, while trying to load the ReturnTime");
            }
        }

        private void AHOptionalRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var DC = DataContext as FlightDialogViewModel;
            DC.Flight.Category = 1;
        }

        private void TKOptionalRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var DC = DataContext as FlightDialogViewModel;
            DC.Flight.Category = 2;
        }

        private void TKOptionalRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            var DC = DataContext as FlightDialogViewModel;
            DC.Flight.Category = 0;
        }

        private void AHOptionalRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            var DC = DataContext as FlightDialogViewModel;
            DC.Flight.Category = 0;
        }
    }
}
