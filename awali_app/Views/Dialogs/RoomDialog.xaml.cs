using Airfare.ViewModels.DialogViewModels;
using System.Windows.Controls;

namespace Airfare.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for AddRoomDialog.xaml
    /// </summary>
    public partial class RoomDialog
    {
        public RoomDialog()
        {
            InitializeComponent();
        }


        private void ColorPicker_Confirmed(object sender, HandyControl.Data.FunctionEventArgs<System.Windows.Media.Color> e)
        {
            var colorPicker = e.OriginalSource as HandyControl.Controls.ColorPicker;
            (DataContext as RoomDialogViewModel).Color = colorPicker.SelectedBrush;
         
        }

        private void ColorPicker_Canceled(object sender, System.EventArgs e)
        {
         
        }
    }
}
