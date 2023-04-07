using Airfare.Models;
using Airfare.ViewModels.UserControlViewModels;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ComboBox = HandyControl.Controls.ComboBox;

namespace Airfare.Views
{
    /// <summary>
    /// Interaction logic for FlightView.xaml
    /// </summary>
    public partial class FlightView : UserControl
    {
        
        public FlightView()
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
                    var DC = (FlightViewModel)DataContext;
                    DC.HotelsList.Remove(hotel);
                    var flighthotel = new FlightHotelModel { Hotel = hotel, HotelId = hotel.Id };
                    DC.SelectedFlightHotelsList.Add(flighthotel);
                    for (int i = 0; i < DC.Rooms.Count(); i++)
                    {
                        DC.HotelsRooms.Add(new() { Room = DC.Rooms[i], RoomId = DC.Rooms[i].Id, Price = 0.0f, FlightHotel = flighthotel });
                    }
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in Left_Click");
            }
            
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FlightHotelModel flightHotel = listBoxHotels.SelectedItems.Cast<FlightHotelModel>().ToList().FirstOrDefault();
                if (flightHotel is not null)
                {
                    var DC = (FlightViewModel)DataContext;
                    HotelRoomModel hotelRoom = DC.HotelsRooms.Where(hr => hr.FlightHotelId == DC.SelectedFlightHotel.Id).ToList().FirstOrDefault();
                    DC.HotelsRooms.Add(hotelRoom);

                    DC.DisplayedHotelsRooms.Clear();
                    DC.HotelsRooms.RemoveAll(hr => hr.FlightHotelId == flightHotel.Id);

                    DC.HotelsList.Add(flightHotel.Hotel);
                    DC.SelectedFlightHotelsList.Remove(flightHotel);
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in Right_Click");
            }
            
        }
      

        private void updateClientButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((FlightViewModel)DataContext).UpdateClient();

            }
            catch (Exception)
            {
                Growl.Error("an error has occurred, while trying to execute the UpdateClient");
            }
        }

        private void CheckComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                List<string> filters = ((FlightViewModel)DataContext).Filters.ToList();
                CheckComboBox origin = sender as CheckComboBox;
                for (int i = 0; i < e.AddedItems.Count; i++)
                {
                    filters.Add((string)e.AddedItems[i]);
                }
                for (int i = 0; i < e.RemovedItems.Count; i++)
                {
                    filters.Remove((string)e.RemovedItems[i]);
                }
                ((FlightViewModel)DataContext).Filters = filters;
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in CheckComboBox_SelectionChanged");
            }
            
        }

        private void exportClientButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((FlightViewModel)DataContext).ExportWordDataCommand.Execute(null);

            }
            catch (Exception)
            {
                Growl.Error("an error has occurred, while trying to execute the ExportWordDataCommand");
            }
        }

        private void removeClientButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((FlightViewModel)DataContext).RemoveHostCommand.Execute(null);

            }
            catch (Exception)
            {
                Growl.Error("an error has occurred, while trying to execute the RemoveHostCommand");

            }
        }

        private void clientsDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Delete && ((FlightViewModel)DataContext).SelectedHost != null)
                {
                    ((FlightViewModel)DataContext).RemoveHostCommand.Execute(null);
                }
                else if (e.Key == Key.M && ((FlightViewModel)DataContext).SelectedHost != null)
                {
                    ((FlightViewModel)DataContext).UpdateClient();
                }
                else if (e.Key == Key.E && ((FlightViewModel)DataContext).SelectedHost != null)
                {
                    ((FlightViewModel)DataContext).ExportWordDataCommand.Execute(null);
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in clientsDataGrid_PreviewKeyDown");
            }
          
        }

        private void FlightsGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.A && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && ((FlightViewModel)DataContext).SelectedFlight != null)
                {
                    ((FlightViewModel)DataContext).AddClientCommand.Execute(null);
                }
                else if (e.Key == Key.M && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && ((FlightViewModel)DataContext).SelectedFlight != null)
                {
                    ((FlightViewModel)DataContext).ShowFlightDialogCommand.Execute(null);
                }
                else if (e.Key == Key.E && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && ((FlightViewModel)DataContext).SelectedFlight != null)
                {
                    ((FlightViewModel)DataContext).ExportExcelDataCommand.Execute(null);
                }
                else if (e.Key == Key.I && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && ((FlightViewModel)DataContext).SelectedFlight != null)
                {
                    ((FlightViewModel)DataContext).ImportExcelDataCommand.Execute(null);
                }
                else if (e.Key == Key.Delete && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && ((FlightViewModel)DataContext).SelectedFlight != null)
                {
                    ((FlightViewModel)DataContext).RemoveFlightCommand.Execute(null);
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in FlightsGrid_PreviewKeyDown");
            }
            
        }

        private void DepartTimePicker_SelectedDateTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            var DC = DataContext as FlightViewModel;
            DC.Flight.DepartTime = DepartTimePicker.SelectedDateTime.Value.TimeOfDay;
        }

        private void ReturnTimePicker_SelectedDateTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            var DC = DataContext as FlightViewModel;
            DC.Flight.ReturnTime = ReturnTimePicker.SelectedDateTime.Value.TimeOfDay;
        }


        private void AHOptionalRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var DC = DataContext as FlightViewModel;
            DC.Flight.Category = 1;
        }

        private void TKOptionalRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var DC = DataContext as FlightViewModel;
            DC.Flight.Category = 2;
        }

        private void TKOptionalRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            var DC = DataContext as FlightViewModel;
            DC.Flight.Category = 0;
        }

        private void AHOptionalRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            var DC = DataContext as FlightViewModel;
            DC.Flight.Category = 0;
        }

       

        private void exportClientPaymentButton_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ((FlightViewModel)DataContext).ExportClientPaymentCommand.Execute(null);

            }
            catch (Exception)
            {
                Growl.Error("an error has occurred, while trying to export client payment");
            }
        }
    }
}
