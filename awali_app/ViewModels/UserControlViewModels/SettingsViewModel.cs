using Airfare.Commands;
using Airfare.DataContext;
using Airfare.Models;
using Airfare.Servies;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Airfare.ViewModels.UserControlViewModels
{
    public class SettingsViewModel : BaseViewModel, IDisposable
    {

        private EnvironmentModel _Environment;

        public EnvironmentModel Environment
        {
            get { return _Environment; }
            set { _Environment = value;
                OnPropertyChanged(nameof(Environment));
            }
        }

        private readonly EnvironmentServices environmentServices = new();

        public RelayCommand ImportHeaderCommand { get; set; }
        public RelayCommand ImportFooterCommand { get; set; }
        public RelayCommand InsertFlightReturnNameCommand { get; set; }
        public RelayCommand InsertFlightDepartNameCommand { get; set; }
        public RelayCommand InsertFlightReturnDateCommand { get; set; }
        public RelayCommand InsertFlightDepartDateCommand { get; set; }
        public RelayCommand InsertFlightDepartTimeCommand { get; set; }
        public RelayCommand InsertFlightReturnTimeCommand { get; set; }
        public RelayCommand InsertFlightReturnItineraryCommand { get; set; }
        public RelayCommand InsertFlightDepartItineraryCommand { get; set; }
        public RelayCommand InsertFlightCapacityCommand { get; set; }
        public RelayCommand InsertClientFirstNameCommand { get; set; }
        public RelayCommand InsertClientLastNameCommand { get; set; }
        public RelayCommand InsertClientBirthDateCommand { get; set; }
        public RelayCommand InsertClientGenderCommand { get; set; }
        public RelayCommand InsertClientPassportNumberCommand { get; set; }
        public RelayCommand InsertClientHealthStatusCommand { get; set; }
        public RelayCommand InsertHotelNameCommand { get; set; }
        public RelayCommand InsertHotelAddressCommand { get; set; }
        public RelayCommand InsertHotelDistanceCommand { get; set; }
        public RelayCommand InsertRoomTypeCommand { get; set; }
        public RelayCommand SaveSettingsCommand { get; set; }

        public SettingsViewModel()
        {
           InitData();
        }

        private  async void InitData()
        {
            try
            {
                Parallel.Invoke(
                    () => { ImportHeaderCommand = new RelayCommand(ImportHeader); },
                    () => { ImportFooterCommand = new RelayCommand(ImportFooter); },
                    () => { InsertFlightReturnNameCommand = new RelayCommand(InsertFlightReturnName); },
                    () => { InsertFlightDepartNameCommand = new RelayCommand(InsertFlightDepartName); },
                    () => { InsertFlightReturnDateCommand = new RelayCommand(InsertFlightReturnDate); },
                    () => { InsertFlightDepartDateCommand = new RelayCommand(InsertFlightDepartDate); },
                    () => { InsertFlightDepartTimeCommand = new RelayCommand(InsertFlightDepartTime); },
                    () => { InsertFlightReturnTimeCommand = new RelayCommand(InsertFlightReturnTime); },
                    () => { InsertFlightReturnItineraryCommand = new RelayCommand(InsertFlightReturnItinerary); },
                    () => { InsertFlightDepartItineraryCommand = new RelayCommand(InsertFlightDepartItinerary); },
                    () => { InsertFlightCapacityCommand = new RelayCommand(InsertFlightCapacity); },
                    () => { InsertClientFirstNameCommand = new RelayCommand(InsertClientFirstName); },
                    () => { InsertClientLastNameCommand = new RelayCommand(InsertClientLastName); },
                    () => { InsertClientBirthDateCommand = new RelayCommand(InsertClientBirthDate); },
                    () => { InsertClientGenderCommand = new RelayCommand(InsertClientGender); },
                    () => { InsertClientPassportNumberCommand = new RelayCommand(InsertClientPassportNumber); },
                    () => { InsertClientHealthStatusCommand = new RelayCommand(InsertClientHealthStatus); },
                    () => { InsertHotelNameCommand = new RelayCommand(InsertHotelName); },
                    () => { InsertHotelAddressCommand = new RelayCommand(InsertHotelAddress); },
                    () => { InsertHotelDistanceCommand = new RelayCommand(InsertHotelDistance); },
                    () => { InsertRoomTypeCommand = new RelayCommand(InsertRoomType); },
                    () => { SaveSettingsCommand = new RelayCommand(SaveSettings); },
                    async () => { Environment = await environmentServices.GetEnvironment(); }
                );


                
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while InitData in the settings");
            }
        }

        private async void SaveSettings()
        {
            try
            {
                await environmentServices.UpdateEnvironment(Environment);

                if (!environmentServices.Error)
                {
                    Growl.Success("تم حفض المعلومات بنجاح");
                    InitData();
                }
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while saving settings");
            }
        }

        private void InsertRoomType()
        {
            try
            {
                if (Environment != null && Environment.ClientContractContent != null)
                    Environment.ClientContractContent += "<RT>";
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while inserting room type");
            }
        }

        private void InsertHotelDistance()
        {
            try
            {
                if (Environment != null && Environment.ClientContractContent != null)
                    Environment.ClientContractContent += "<HD>";
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while inserting hotel distance");
            }
        }

        private void InsertHotelAddress()
        {
            try
            {
                if (Environment != null && Environment.ClientContractContent != null)
                    Environment.ClientContractContent += "<HA>";
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while inserting hotel address");
            }
        }

        private void InsertHotelName()
        {
            try
            {
                if (Environment != null && Environment.ClientContractContent != null)
                    Environment.ClientContractContent += "<HN>";
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while inserting hotel name");
            }
        }

        private void InsertClientHealthStatus()
        {
            try
            {
                if (Environment != null && Environment.ClientContractContent != null)
                    Environment.ClientContractContent += "<CHS>";
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while inserting client health status");
            }
        }

        private void InsertClientPassportNumber()
        {
            try
            {
                if (Environment != null && Environment.ClientContractContent != null)
                    Environment.ClientContractContent += "<CPN>";
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while inserting client passport number");
            }
        }

        private void InsertClientGender()
        {
            try
            {
                if (Environment != null && Environment.ClientContractContent != null)
                    Environment.ClientContractContent += "<CG>";
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while inserting client gender");
            }
        }

        private void InsertClientBirthDate()
        {
            try
            {
                if (Environment != null && Environment.ClientContractContent != null)
                    Environment.ClientContractContent += "<CBD>";
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while inserting client birth date");
            }
        }

        private void InsertClientLastName()
        {
            try
            {
                if (Environment != null && Environment.ClientContractContent != null)
                    Environment.ClientContractContent += "<CLN>";
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while inserting client last name");
            }
        }

        private void InsertClientFirstName()
        {
            try
            {
                if (Environment != null && Environment.ClientContractContent != null)
                    Environment.ClientContractContent += "<CFN>";
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while inserting client first name");
            }
        }

        private void InsertFlightCapacity()
        {
            try
            {
                if (Environment != null && Environment.ClientContractContent != null)
                    Environment.ClientContractContent += "<FC>";
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while inserting flight cpaciy");
            }
        }
        private void InsertFlightDepartItinerary()
        {
            try
            {
                if (Environment != null && Environment.ClientContractContent != null)
                    Environment.ClientContractContent += "<FDI>";
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while inserting flight depart itinerary");
            }
        }

        private void InsertFlightReturnItinerary()
        {
            try
            {
                if (Environment != null && Environment.ClientContractContent != null)
                    Environment.ClientContractContent += "<FRI>";
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while inserting flight return itinerary");
            }
        }

        private void InsertFlightReturnTime()
        {
            try
            {
                if (Environment != null && Environment.ClientContractContent != null)
                    Environment.ClientContractContent += "<FRT>";
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while inserting flight return time");
            }
        }

        private void InsertFlightDepartTime()
        {
            try
            {
                if (Environment != null && Environment.ClientContractContent != null)
                    Environment.ClientContractContent += "<FDT>";
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while inserting flight depart time");
            }
        }

        private void InsertFlightDepartDate()
        {
            try
            {   
                if(Environment != null && Environment.ClientContractContent != null)
                    Environment.ClientContractContent += "<FDD>";
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while inserting flight depart date");
            }
        }

        private void InsertFlightReturnDate()
        {
            try
            {
                if (Environment != null && Environment.ClientContractContent != null)
                    Environment.ClientContractContent += "<FRD>";
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while inserting flight return date");
            }
        }

        private void InsertFlightDepartName()
        {
            try
            {
                if (Environment != null && Environment.ClientContractContent != null)
                    Environment.ClientContractContent += "<FDN>";
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while inserting flight depart name");
            }
        }

        private void InsertFlightReturnName()
        {
            try
            {
                if (Environment != null && Environment.ClientContractContent != null)
                    Environment.ClientContractContent += "<FRN>";
            }
            catch (Exception)
            {
                Growl.Error("An error occurred while inserting flight return name");
            }
        }

        private void ImportFooter()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Import Image File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string targetPath = System.Windows.Forms.Application.StartupPath + "\\Resources\\Footer";
                if (!System.IO.Directory.Exists(targetPath))
                    System.IO.Directory.CreateDirectory(targetPath);
                targetPath += "\\" + openFileDialog.SafeFileName;
                System.IO.File.Copy(openFileDialog.FileName, targetPath, true);
                Environment.FooterSource = targetPath;
            }
        }

        private void ImportHeader()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Import Image File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            { 
                string targetPath = System.Windows.Forms.Application.StartupPath + "\\Resources\\Header" ;
                if (!System.IO.Directory.Exists(targetPath))
                    System.IO.Directory.CreateDirectory(targetPath);
                targetPath += "\\" + openFileDialog.SafeFileName;
                System.IO.File.Copy(openFileDialog.FileName, targetPath, true);
                Environment.HeaderSource = targetPath;
            }
        }

       
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
