   M S B A M L   `   `   `  ���� D  @awali_app, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:awali_app.Views.UserControlsawali_app.Views.UserControls  g8clr-namespace:awali_app.ViewModels.UserControlViewModels*awali_app.ViewModels.UserControlViewModels  ;"clr-namespace:awali_app.Convertersawali_app.Converters  2    ,awali_app.Views.UserControls.ReservationView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     <local*clr-namespace:awali_app.Views.UserControls     S OHandyControl, Version=3.3.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  Avm8clr-namespace:awali_app.ViewModels.UserControlViewModels  +cv"clr-namespace:awali_app.Converters  $��RightToLeft=�  � DataContext  E   ?awali_app.ViewModels.UserControlViewModels.ReservationViewModel   � 	Resources 1   +awali_app.Converters.BoolToCollapsConverter   BoolToCollaps��%   &	          .��+  %HandyControl.Controls.DialogContainer  .u�7  1HandyControl.Controls.TransitioningContentControl  .��'  !HandyControl.Controls.SimplePanel  .+��� ._��  � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $��* �.+��� $��0��$��
16,16,16,0q� �� Orientation$ 
Horizontal=�.+��� $!��الموسم الحالي: ���� ���� *CurrentSeason.Name+�� $�� من: ���� ���� *CurrentSeason.StartDate+ �� StringFormat$ 
dd-MM-yyyy���� $��	 إلى: ���� ���� *CurrentSeason.EndDate+$ 
dd-MM-yyyy��#  HandyControl.Controls.StepBar  $��1��$
��0,16q�   	StepIndex �� *	StepIndex+.@�'  !HandyControl.Controls.StepBarItem  $��رحلة طيران��  $��إدارة الفندق��  $��العملاء���� $��2��y��� *FlightSectionVisibility+ �� 	Converter# [  .+��� $��16q�$��رحلة الطيران��$��10q�  GroupBoxTab#��[ .����   RegionBrush#���   �� CornerRadius$ 4��._��  �� �� .+��� $��0��+  %HandyControl.Controls.DropDownElement   ConsistentWidth$ False��(	  "HandyControl.Controls.TitleElement	 	  Title$	 الرحلات��$��10q�  ComboBoxExtend#��[ ���� *FlightsList+
 �� SelectedItem
 �� *SelectedFlight+�� $��1��$
��Left=�.+��� $��10q�$��أضف رحلة������ *ShowAddFlightDialogCommand+�� $��2��y��� *HotelSectionVisibility+# [  .+��� $��16q�$��إدارة الفندق��$��10q�#��[ .���� #���  $ 4��._��  �� �� .+��� $��0��$ False�� �� SelectedIndex$ 0��$	 الفنادق��$��10q�#��[ ���� *
HotelsList+�� $��1��$
��Left=�.+��� $��10q�$��Stretch=�$��أضف فندق������ *ShowAddHotelDialogCommad+�� $��2��y��� *ClientSectionVisibility+# [  .+��� $��16q�$��العملاء��$��10q�#��[ .���� #���  $ 4��._��  �� �� �� .+�)
  #HandyControl.Controls.CheckComboBox
  -    
 Name$ CheckComboBox��$��0�� 
  ShowClearButton$
 True��  CheckComboBoxPlus#��[ $	 العملاء��$��10q� 
  ShowSelectAllButton$
 True������ *CompaniesList+�� $��4��$
��Left=�$ 
Horizontal=�.+��� $��10q�$��Stretch=�$��أضف عميل������ *ShowAddCompanyDialogCommad+�� $��3��$
��0,32q�$��Center=�$��Bottom=�$ 
Horizontal=�.+��� $	��180��$��16q�$��السابق��  ButtonPrimary#��[ ���� *PreviousStepCommand+����  �� ElementName$ 	PanelMain���� $	��180��$��16q�$��التالي��#��[ ���� *NextStepCommand+���� $ 	PanelMain��