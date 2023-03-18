using Airfare.Models;
using Airfare.ViewModels.UserControlViewModels;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Airfare.Views.UserControls
{
    /// <summary>
    /// Interaction logic for HotelView.xaml
    /// </summary>
    public partial class HotelView : UserControl
    {
        List<Border> borderList = new List<Border>();
        Border groupBorder = new Border();
        public HotelView()
        {
            InitializeComponent();
        }

        private void hostItem_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    DragDrop.DoDragDrop((sender as UIElement), new DataObject(DataFormats.Serializable, sender), DragDropEffects.Move);
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in hostItem_MouseMove");
            }
            
        }

        private void spotContainer_Drop(object sender, DragEventArgs e)
        {
            try
            {
                object data = e.Data.GetData(DataFormats.Serializable);
                if (data is Border element)
                {
                    var child = element.Child as StackPanel;
                    HostModel host = child.DataContext as HostModel;
                    var origin = sender as Border;
                    SpotModel spot = origin.DataContext as SpotModel;
                    ((HotelViewModel)DataContext).DropSelectedHost(spot);


                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in spotContainer_Drop");
            }
        }

        private void RemoveHostFromSpotButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as Button;
                HostModel host = button.DataContext as HostModel;
                var spot = ((HotelViewModel)DataContext).DisplayedSpots.Where(s => s.Hosts.Cast<HostModel>().Contains(host)).FirstOrDefault();

                ((HotelViewModel)DataContext).RemoveSelectedHostFromSpot(spot, host);
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in RemoveHostFromSpotButton_Click");
            }
        }

        private void roomtypeListboxItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ClickCount >= 2)
                {
                    RoomModel room = (e.OriginalSource as TextBlock).DataContext as RoomModel;
                    (DataContext as HotelViewModel).SelectedRoom = room;

                    (DataContext as HotelViewModel).AddSpot();
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in roomtypeListboxItem_MouseDown");
            }
           
        }

        

        private void LinkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var colors = (DataContext as HotelViewModel).DisplayedSpots.Where(s => s.Selected).Select(s => s.Color).ToArray();
                List<Color> colorsList = new();
                for (int i = 0; i < colors.Length; i++)
                {
                    colorsList.Add((Color)ColorConverter.ConvertFromString(colors[i]));
                }
                Color color = Blend(colorsList);
                (DataContext as HotelViewModel).AddGroup(color.ToString());
                borderList.Clear();
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in LinkButton_Click");
            }
          

        }

        private void BreakButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                (DataContext as HotelViewModel).BreakGroup();
                groupBorder = new Border();
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in BreakButton_Click");
            }
        }

        private void ExportButton_Click(object sender,RoutedEventArgs e)
        {
            (DataContext as HotelViewModel).ExportExcelData();
        }

        private Color Blend( List<Color> colors)
        {
            byte r = SumOfBytes(colors.Select(c=>c.R).ToArray());
            byte g = SumOfBytes(colors.Select(c => c.G).ToArray());
            byte b = SumOfBytes(colors.Select(c => c.B).ToArray());
            return Color.FromRgb(r, g, b);
        }

        private byte SumOfBytes(byte[] bytes)
        {
            byte? _byte=null;
            for(int i = 0; i<bytes.Length;i++) 
            {
                if(_byte == null)
                {
                    _byte = (byte)(bytes[i] * (1.0/ bytes.Length));
                }
                else
                {
                    _byte += (byte)(bytes[i] * (1.0 / bytes.Length));
                }
            }
            return _byte??0;
        }

       
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                borderList.Clear();
                (DataContext as HotelViewModel).SpotsAvailable = false;
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in TabControl_SelectionChanged");
            }
        }

        private void groupContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var border = sender as Border;
                if (e.LeftButton == MouseButtonState.Pressed && Keyboard.IsKeyUp(Key.LeftCtrl) && Keyboard.IsKeyUp(Key.RightCtrl))
                {
                    for (int i = 0; i < borderList.Count; i++)
                    {
                        borderList[i].BorderThickness = new Thickness(0);
                        (borderList[i].DataContext as SpotModel).Selected = false;
                    }
                    borderList.Clear();
                    if (groupBorder.DataContext != null)
                    {
                        (groupBorder.DataContext as GroupModel).Selected = false;
                        groupBorder.Background = Brushes.Transparent;
                        groupBorder = new Border();
                        (DataContext as HotelViewModel).GroupAvailable = false;
                    }
                    (DataContext as HotelViewModel).SpotsAvailable = false;
                    (DataContext as HotelViewModel).GroupAvailable = true;
                    (border.DataContext as GroupModel).Selected = true;
                    border.Background = border.BorderBrush;
                    groupBorder = border;
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in groupContainer_MouseDown");
            }
        }

        private void spotContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (groupBorder.DataContext != null && groupBorder.DataContext is GroupModel)
                {
                    (groupBorder.DataContext as GroupModel).Selected = false;
                    groupBorder.Background = Brushes.Transparent;
                    groupBorder = new Border();
                    (DataContext as HotelViewModel).GroupAvailable = false;
                }
                var border = sender as Border;
                if (e.LeftButton == MouseButtonState.Pressed && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                {
                    border.BorderThickness = new Thickness(2);
                    border.BorderBrush = Brushes.Black;
                    (border.DataContext as SpotModel).Selected = true;
                    var numberSpot = (DataContext as HotelViewModel).DisplayedSpots.Where(s => s.Selected).Count();
                    if (numberSpot >= 2)
                    {
                        (DataContext as HotelViewModel).SpotsAvailable = true;
                    }
                    else
                    {
                        (DataContext as HotelViewModel).SpotsAvailable = false;
                    }
                    borderList.Add(border);
                }
                if (e.LeftButton == MouseButtonState.Pressed && Keyboard.IsKeyUp(Key.LeftCtrl) && Keyboard.IsKeyUp(Key.RightCtrl))
                {
                    for (int i = 0; i < borderList.Count; i++)
                    {
                        borderList[i].BorderThickness = new Thickness(0);
                        if (borderList[i].DataContext is SpotModel)
                            (borderList[i].DataContext as SpotModel).Selected = false;
                    }
                    borderList.Clear();
                    border.BorderThickness = new Thickness(2);
                    border.BorderBrush = Brushes.Black;
                    (border.DataContext as SpotModel).Selected = true;
                    (DataContext as HotelViewModel).SpotsAvailable = false;
                    borderList.Add(border);
                }
            }
            catch (Exception)
            {

                Growl.Error("an error has occurred in spotContainer_MouseDown");
            }
        }
    }
}
