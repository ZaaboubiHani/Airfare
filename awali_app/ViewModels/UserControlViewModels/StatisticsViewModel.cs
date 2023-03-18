using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Airfare.DataContext;
using Airfare.Models;
using Airfare.Servies;
using HandyControl.Controls;
using LiveCharts;
using LiveCharts.Charts;
using LiveCharts.Wpf;

namespace Airfare.ViewModels.UserControlViewModels
{
    public class StatisticsViewModel:BaseViewModel
    {
        #region Properties
        private SeriesCollection _Series;

        public SeriesCollection Series
        {
            get { return _Series; }
            set { _Series = value;
                OnPropertyChanged(nameof(Series));
            }
        }

        private Func<double,string> _YFormatter;

        public Func<double, string> YFormatter
        {
            get { return _YFormatter; }
            set { _YFormatter = value;
                OnPropertyChanged(nameof(YFormatter));
            }
        }
        private List<string> _Labels;

        public List<string> Labels
        {
            get { return _Labels; }
            set { _Labels = value;
                OnPropertyChanged(nameof(Labels));
            }
        }

        private ICollectionView _FlightsList;

        public ICollectionView FlightsList
        {
            get { return _FlightsList; }
            set { _FlightsList = value;
                OnPropertyChanged(nameof(FlightsList));
            }
        }

        private FlightModel _SelectedFlight;

        public FlightModel SelectedFlight
        {
            get { return _SelectedFlight; }
            set { _SelectedFlight = value;
                OnPropertyChanged(nameof(SelectedFlight));
                DisplayData();
            }
        }

        List<PaymentModel> PaymentsList { get; set; }

        #endregion

        #region Services
        readonly private PaymentServices paymentServices = new();
        readonly private FlightServices flightServices = new();
        #endregion

        #region Commands

        #endregion

        public StatisticsViewModel()
        {
            InitData();
        }

        #region Methods
        private async void InitData()
        {
            try
            {
                FlightsList = CollectionViewSource.GetDefaultView(await flightServices.GetAllFlightsOfSeason(Configuration.CurrentSeason.Id));
                PaymentsList = await paymentServices.GetAllPaymentsOfSeason(Configuration.CurrentSeason.Id);
                var groupedPayments = PaymentsList.GroupBy(x => x.Date.Month).ToList();
                Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "المدفوعات",
                        Values = new ChartValues<float>(),
                    },
                };
                for (int i = 0; i < groupedPayments.Count; i++)
                {
                    float value = groupedPayments[i].Select(gp => gp.Amount).Sum();
                    Series[0].Values.Add(value);
                }
                Labels = new List<string>();
                for (int i = 0; i < groupedPayments.Count; i++)
                {
                    string month = groupedPayments[i].Select(gp => gp.Date).FirstOrDefault().ToString("MMMM", new CultureInfo("ar-DZ")); ;
                    Labels.Add(month);
                }

                YFormatter = value => value.ToString("C", new CultureInfo("ar-DZ"));
            }
            catch (Exception)
            {

                Growl.Error("an error occurred while initData");
            }
           
        }


        private void DisplayData()
        {
            try
            {
                if(SelectedFlight != null)
                {
                    var groupedPayments = PaymentsList.Where(p => p.Host.HotelRoom.FlightHotel.FlightId == SelectedFlight.Id).GroupBy(x => x.Date.Month).ToList();
                    Series = new SeriesCollection
                    {
                        new LineSeries
                        {
                            Title = "المدفوعات",
                            Values = new ChartValues<float>(),
                        },
                    };
                    for (int i = 0; i < groupedPayments.Count; i++)
                    {
                        float value = groupedPayments[i].Select(gp => gp.Amount).Sum();
                        Series[0].Values.Add(value);
                    }
                    Labels = new List<string>();
                    for (int i = 0; i < groupedPayments.Count; i++)
                    {
                        string month = groupedPayments[i].Select(gp => gp.Date).FirstOrDefault().ToString("MMMM", new CultureInfo("ar-DZ")); ;
                        Labels.Add(month);
                    }
                }
                else
                {
                    var groupedPayments = PaymentsList.GroupBy(x => x.Date.Month).ToList();
                    Series = new SeriesCollection
                    {
                        new LineSeries
                        {
                            Title = "المدفوعات",
                            Values = new ChartValues<float>(),
                        },
                    };
                    for (int i = 0; i < groupedPayments.Count; i++)
                    {
                        float value = groupedPayments[i].Select(gp => gp.Amount).Sum();
                        Series[0].Values.Add(value);
                    }
                    Labels = new List<string>();
                    for (int i = 0; i < groupedPayments.Count; i++)
                    {
                        string month = groupedPayments[i].Select(gp => gp.Date).FirstOrDefault().ToString("MMMM", new CultureInfo("ar-DZ")); ;
                        Labels.Add(month);
                    }
                }
            }
            catch (Exception)
            {

                Growl.Error("an error occurred while displaying Data");
            }
        }
        #endregion
    }
}
