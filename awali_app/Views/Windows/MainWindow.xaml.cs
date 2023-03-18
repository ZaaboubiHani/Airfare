
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

namespace Airfare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : HandyControl.Controls.Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PagesNavigation.Navigate(new System.Uri("Views/UserControls/SeasonsView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Dashboard_button_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Views/UserControls/DashboardView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Hotel_button_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Views/UserControls/HotelView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Clients_button_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Views/UserControls/CompaniesView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Room_button_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Views/UserControls/RoomView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Flights_button_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Views/UserControls/FlightView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Reservation_button_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Views/UserControls/groupbox.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Billing_button_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Views/UserControls/BillingView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Settings_button_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Views/UserControls/SettingsView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Seasons_poptip_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Views/UserControls/SeasonsView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Statistics_button_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Views/UserControls/StatisticsView.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
