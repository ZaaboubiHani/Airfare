   M S B A M L   `   `   `  ���� D  @awali_app, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:awali_app.Views.Dialogsawali_app.Views.Dialogs  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem ]3clr-namespace:awali_app.ViewModels.DialogViewModels%awali_app.ViewModels.DialogViewModels  T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls ;"clr-namespace:awali_app.Convertersawali_app.Converters  *    $awali_app.Views.Dialogs.ClientDialog   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     7local%clr-namespace:awali_app.Views.Dialogs     6sys&clr-namespace:System;assembly=mscorlib     Bvm3clr-namespace:awali_app.ViewModels.DialogViewModels     S	 OHandyControl, Version=3.3.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol 	 Emah;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro 	 +cv"clr-namespace:awali_app.Converters 	 -     ��Name$  clientDialogUI�� �� CornerRadius$ 10��
�������$��RightToLeft=�$	��900��$
��auto�� � DataContext A   ;awali_app.ViewModels.DialogViewModels.ClientDialogViewModel   � 	Resources 1   +awali_app.Converters.BoolToCollapsConverter   BoolToCollaps0  *awali_app.Converters.BoolToGenderConverter  BoolToGender8   2awali_app.Converters.ReverseBoolToCollapsConverter  ReverseBoolToCollaps%   awali_app.Converters.StartAtOne  
StartAtOne��%<   &	        &	      &	 
     &	              ._�' 	 !HandyControl.Controls.SimplePanel  .+��� $��22��$��22��  
ButtonIcon#��[   PrimaryBrush#���    ErrorGeometry' 	 !HandyControl.Controls.IconElement   Geometry# [ $��0q�$��Right=�$	��Top=�$��0,4,4,0q����� *CloseDialogCommand+�� $��16,18q�.+��  � RowDefinitions �� �� �� �� �� �� �� ��  � ColumnDefinitions �� �� �� .+��� $��0��$��0��$��5q�  TextBoxExtend#��[ ( 	 "HandyControl.Controls.TitleElement   TitlePlacement$	 Top=�   Title$ 
اللقب��$��Center=����� *Client.FirstName+�� $��0��$��1��$��5q�#��[ $	 Top=�$ 
الإسم��$��Center=����� *Client.LastName+(	  "System.Windows.Controls.DatePicker	  $��0��$��2��$��5q�  DatePickerExtend#��[ $	 Top=�$ تاريخ الميلاد��	 	  SelectedDate	 �� *Client.BirthDate+�� $��1��$��0��$��5q�#��[ $	 Top=�$  رقم جواز السفر��$��Center=����� *Client.PassportNumber+]� $	��260��$��1��$��1��$��5q�$
��Left=�.+��� #��[ $	 Top=�$ رقم الهاتف��$��Center=����� *PhoneNumber+
 �� UpdateSourceTrigger$
 PropertyChanged=� �� Mode$ TwoWay=��� $��Bottom=�$��5q�$��1��$��1��$��Right=�)
 	 #HandyControl.Controls.BorderElement 
  CornerRadius$ 15�� 	 plus# [	    Height$ 17Q����� *AddPhoneCommand+* 	 $HandyControl.Controls.AdornerElement   Instance " 	 HandyControl.Controls.Poptip     Content$  أضف رقم الهاتف��   Delay$ 0Q� J� Opacity$ 1Q�$��12(�   PlacementType$
 Left=�   HorizontalOffset$ 40Q��� $��1��$��2��$��5q�#��[ $	 Top=�$ الحالة الصحية��$��Center=����� *Client.HealthStatus+�� $��3��$��0��$��5q� 
 ComboBoxExtend#��[
 $	 Top=�$ 
الجنس��$��Center=� �� SelectedIndex �� *Client.Gender+.@��� $��ذكر���� $��أنثى���� $��3��$��1��$��5q�#��[
 $	 Top=�$ الفندق��$��Center=����� *FlightHotels+ �� *SelectedFlightHotelIndex+$
 PropertyChanged=�$ TwoWay=� �� SelectedItem �� *SelectedFlightHotel+�� $��3��$��2��$��5q�#��[
 $	 Top=�$ الغرفة��$��Center=����� *Rooms+ �� *SelectedRoomIndex+$
 PropertyChanged=�$ TwoWay=� �� *SelectedRoom+�� $��4��$��0��$��5q�#��[ $	 Top=�$ ملاحظة��$��Center=����� *Client.Description+�� $��5��$��0��$��5q�#��[ $	 Top=�$ 
تخفيض��$��Center=����� *
Discount+$
 PropertyChanged=�$ TwoWay=��� $	��260��$
��Left=�$��6��$��0��$��5q�#��[ $	 Top=�$ 
الدفع��$��Center=����� *Payment.Amount+�� $��6q�$��6��$��0��$��Right=�$��Bottom=�$ 15��# [	 $ 17Q����� *AddPaymentCommand+   $ أضف دفع��$ 0Q�$ 1Q�$��12(�$
 Left=�$ 40Q��� $��7��$��0��$��Center=�$��3�� �� Orientation$ 
Horizontal=�$��2,0,-2,0q�.+��� $
��10,0q�.+��� $��0,5,0,5q�$��الاعاشة���� $	��0,5q�$	��0.5q� d� 	IsChecked �� *Feed+ � LayoutTransform ��  �� ScaleX$	 1.2Q� �� ScaleY$	 1.2Q�# 	 HandyControl.Controls.Divider  $	 0.5Q�   Orientation$ Vertical=�$	��0,5q�$��Center=�   LineStrokeThickness$ 1Q�  DarkPrimaryBrush   
LineStroke# �  �� $#��المبلغ الاجمالي��$��10q�$��Center=��� $	��100��$	��0,5q�$��Center=����� *	FullPrice+$ TwoWay=��� $��المبلغ الدفوع��$��10q�$��Center=��� $	��100��$	��0,5q�$��Center=����� *	PaidPrice+$ TwoWay=��� $!��المبلغ المتبقي��$��10q�$��Center=��� $	��100��$	��0,5q�$��Center=����� *RemainingPrice+$ TwoWay=�&   System.Windows.Controls.DataGrid  $��5q�   	RowHeight$ 25Q�$
}�True��$��4��$��1��$��2��   
IsReadOnly$
 True��    HeadersVisibility$	  All=�!   SelectionMode$! Single=�"   AutoGenerateColumns$" False������ *
PhonesList+ �� *SelectedPhone+#   Columns# 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn$   Header$$ رقم الهاتف��1  +System.Windows.Controls.DataGridBoundColumn%   Binding% �� *Number+4  .System.Windows.Controls.DataGridTemplateColumn  &   CellTemplate& �� .R��� $ 15�� 
 delete# [ $ 13Q����� ' �� Path$$' DataContext.RemovePhoneCommand�( �� ElementName$( clientDialogUI��   $ 
إزالة��$ 0Q�$ 1Q�$��12(�$
 Left=�$	 -55Q��� $��6��$��1��$��10q�.+��� $��
اللون��$��5 0 0 5q�(  "MahApps.Metro.Controls.ColorPicker   )   IsColorPalettesTabVisible$) False��,  &MahApps.Metro.Controls.ColorPickerBase*   SelectedColor* �� *Client.Color+  $ 25Q�$
}�True��$��5q�$��4��$��2��$��2��$
 True��$	  All=�$! Single=�$" False������ *PaymentsList+ �� *SelectedPayment+#   $$ 
الدفع��% �� *Amount+  $$ التاريخ��% �� *Date+  & �� .R��� $ 15��# [ $ 13Q����� $&'  DataContext.RemovePaymentCommand�$( clientDialogUI��   $ 
إزالة��$ 0Q�$ 1Q�$��12(�$
 Left=�$	 -55Q�$ 	 HandyControl.Controls.ComboBox  +   ShowClearButton$
+ True��$��6��$��2��$��5q�$	 Top=�$ المتعامل��$��Center=����� *	Companies+ �� *SelectedCompanyIndex+$
 PropertyChanged=�$ TwoWay=� �� *SelectedCompany+$
 PropertyChanged=�$ TwoWay=��� $��0q�$��
إدخال��y��� *
UpdateMode+, �� 	Converter#, [ ���� *AddClientCommand+�� $��0q�$��
تحديث��y��� *
UpdateMode+#, [  ���� *UpdateClientCommand+