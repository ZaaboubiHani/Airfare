   M S B A M L   `   `   `  ���� D  @awali_app, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:awali_app.Views.Dialogsawali_app.Views.Dialogs  ]3clr-namespace:awali_app.ViewModels.DialogViewModels%awali_app.ViewModels.DialogViewModels  ;"clr-namespace:awali_app.Convertersawali_app.Converters  *    $awali_app.Views.Dialogs.FlightDialog   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     7local%clr-namespace:awali_app.Views.Dialogs     S OHandyControl, Version=3.3.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  <vm3clr-namespace:awali_app.ViewModels.DialogViewModels  +cv"clr-namespace:awali_app.Converters    �� CornerRadius$  10��
�������$��RightToLeft=�$
��1000��$	��500�� � DataContext A   ;awali_app.ViewModels.DialogViewModels.FlightDialogViewModel   � 	Resources 1   +awali_app.Converters.BoolToCollapsConverter   BoolToCollaps��%   &	          ._�'  !HandyControl.Controls.SimplePanel  .+��� $��22��$��22��  
ButtonIcon#��[   PrimaryBrush#���    ErrorGeometry'  !HandyControl.Controls.IconElement   Geometry# [ $��0q�$��Right=�$	��Top=�$��0,4,4,0q����� *CloseDialogCommand+� $��16,18q� � RowDefinitions �� $��* ��� $
��auto �.+��� $��0��
������ .@��� $!��معلومات الرحلة��.��� $��10q� �� �� �� �� ��  � ColumnDefinitions �� �� $
��auto ��� .+��� $��0�� �� TextAlignment$ Center=�$��رحلة الذهاب���� $��60��  EffectShadow2 J� Effect# [ 
�������$��0��$��0��  TextBoxExtend#��[ (  "HandyControl.Controls.TitleElement   Title$ رقم الرحلة������ *Flight.DepartName+&   HandyControl.Controls.DatePicker  $��60��# [ $��1��$��0��	   ShowClearButton$
	 True��
�������$��1q�$ تاريخ الرحلة��(  "System.Windows.Controls.DatePicker
   SelectedDate
 �� *Flight.DepartDate+&   HandyControl.Controls.TimePicker  $��60��# [ $��2��$��0��   ShowClearButton$
 True��  TimePickerExtend#��[ 
�������$ وقت الرحلة��   SelectedTime �� *Flight.DepartDate+�� $��60��# [ 
�������$��3��$��0��#��[ $ مسار الرحلة������ *Flight.DepartItinerary+#	  HandyControl.Controls.Divider	   	  Orientation$ Vertical=�$��1��$��4��$��Center=� 	  LineStrokeThickness$ 2Q� J� Opacity$	 0.5Q� 	  
LineStroke
 �   ��� $��2��$ Center=�$��رحلة العودة���� $��60��# [ 
�������$��0��$��2��#��[ $ رقم الرحلة������ *Flight.ReturnName+  $��60��# [ 
�������$��1��$��2��$
	 True��  DatePickerExtend#��[ $ تاريخ الرحلة��
 �� *Flight.ReturntDate+  $��60��# [ 
�������$��2��$��2��$
 True��#��[ $ وقت الرحلة�� �� *Flight.ReturntDate+�� $��60��# [ 
�������$��3��$��2��#��[ $ مسار الرحلة������ *Flight.ReturnItinerary+�� $��60��# [ 
�������$��4��$��0��$��2��#��[ $ 
السعة������ *Flight.Capacity+��  �� Orientation$ 
Horizontal=�$��4��$��2��$
��10,0q�.+��� $��Center=�$��10q�$��AH���� $	��0,5q�$	��0.5q� d� 	IsChecked �� *	Flight.AH+ � LayoutTransform ��  �� ScaleX$	 1.5Q� �� ScaleY$	 1.5Q��� $��فنادق الرحلة��.���  �� �� .+��� 
������ 
�������  EffectShadow3# [ $��1��$��فنادق الرحلة��$��0q�$��10q� 	 GroupBoxTab#��[	 .��� $��10q� �� $��* ��� $
��auto �.+��� 
������ $	��0.1q�$��0��$	��Top=�$  10��._��  �� �� $
��auto ���  �� $��* ��� $��* �.+��� $#��الفنادق المتاحة��$ Center=�$��0��$��0,5,0,0q�$��0���� -    ��Name$ listViewHotels��
�������
������ $��1��$��0�� 
 ListView.Small#��[
 ���� *
HotelsList+����  �� 
TargetType" ��.���  �� Property � �� �� Value
 ���� ��  � �� ��  �� 
TargetType" ��.R���  ��Name$ _Border��
������ $��0q�$��5q�$  5�� �� Padding$ 10q� J� SnapsToDevicePixels$ False��
�������._��� $��_Cont�� �� Triggers P�  P� Property �@J�IsMouseOver  P� Value$
  True��. ��� ! �� 
TargetName$! _Border��  VividOrangeColor# �   � ��P�  �@��
IsSelected$
  True��. ��� $! _Border��  VividBlueColor# �   � ��" �� View" �� !# �� ColumnHeaderContainerStyle# �� " ��.���  � ��$ 0��.P��� $ �� CellTemplate$ �� .R��� $��Center=�$��Center=����� *Name+'
  !HandyControl.Controls.LoadingLine
  $��1��$��0��$
��0,16q�y��� *HotelsLoadingLine+% �� 	Converter#% [  �� $��Center=�$��1��$	��5,5q�$��1��.+��� -   & ��Name$
& Left��$	��0,5q�
������ # [ )  #HandyControl.Controls.BorderElement'   CornerRadius$' 15��  	arrowLeft# [ (   Height$( 17Q��� -   $& Right��$	��0,5q�
������ # [ $' 15��  
arrowRight# [ $( 17Q��� $%��الفنادق المختارة��$ Center=�$��0,5,0,0q�$��2��$��0���� -   ) ��Name$) listBoxHotels��
������ 
������ $��1��$��2������ *SelectedFlightHotelsList+* �� SelectedItem* �� *SelectedFlightHotel+���� " ��.���  � ��$ 50����  � ��
 �������  � ��$ Stretch=����� .R��� $��Stretch=�$��40��$��Stretch=�
�������$ 1Q�$  5��$}�False��$��0,4,0,0q�._��  �� $��* ��� $
��auto ��� $��* �.+��� $��0��$��Center=�$
��Left=�$��	10,0,20,0q����� *
Hotel.Name+�� $��الاعاشة��$��1��$��Center=�$��Center=�$	��5,0q��� $��Center=�
������ # [ $��2��$��80��$��25��$
��10,0q�$��0 0 10 0q����� *Feed+�� 
������ 
�������# [ $��1��$��1��$��
الغرف��$��10q�#��[	 .���  �� $��* ��� $
��auto �.+�&   System.Windows.Controls.DataGrid  $��0��
�������$	��Top=�
�������$	��0.3q�+   
IsReadOnly$
+ True��,   HeadersVisibility$	, All=�-   SelectionMode$- Single=�.   AutoGenerateColumns$. False������ *DisplayedHotelsRooms+/   Columns/ 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn0   Width5  /System.Windows.Controls.DataGridLengthConverter$
0 auto 1   Header$1 نوع الغرفة��1  +System.Windows.Controls.DataGridBoundColumn2   Binding2 �� *Room+4  .System.Windows.Controls.DataGridTemplateColumn  $	0 120 $1 
تسعير��3   CellTemplate3 �� .R��� 
�������$��1q�# [ ���� *Price+4 �� UpdateSourceTrigger$4 PropertyChanged=�5 �� Mode$5 TwoWay=��� $��1��$��10q�$��
تحديث������ *AddFlightCommand+