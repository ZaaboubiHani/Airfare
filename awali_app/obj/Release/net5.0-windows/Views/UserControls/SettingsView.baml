   M S B A M L   `   `   `  ���� D  @awali_app, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:awali_app.Views.UserControlsawali_app.Views.UserControls  g8clr-namespace:awali_app.ViewModels.UserControlViewModels*awali_app.ViewModels.UserControlViewModels  ;"clr-namespace:awali_app.Convertersawali_app.Converters  /    )awali_app.Views.UserControls.SettingsView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     <local*clr-namespace:awali_app.Views.UserControls     S OHandyControl, Version=3.3.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  Avm8clr-namespace:awali_app.ViewModels.UserControlViewModels  +cv"clr-namespace:awali_app.Converters  $��RightToLeft=�  � DataContext  B   <awali_app.ViewModels.UserControlViewModels.SettingsViewModel   � 	Resources 1   +awali_app.Converters.BoolToCollapsConverter   BoolToCollaps0  *awali_app.Converters.BoolToGenderConverter  BoolToGender8   2awali_app.Converters.ReverseBoolToCollapsConverter  ReverseBoolToCollaps%   awali_app.Converters.StartAtOne  
StartAtOne0  *awali_app.Converters.BytesToImageConverter  BytesToImage0  *awali_app.Converters.ObjToCollapsConverter  ObjToCollaps��%Z   &	        &	      &	 
     &	      &	      &	                  .��+  %HandyControl.Controls.DialogContainer  .u�7	  1HandyControl.Controls.TransitioningContentControl	  .��'
  !HandyControl.Controls.SimplePanel
  .+��� .@��� $!��معلومات الرحلة��.���  � RowDefinitions �� $��* ��� $
��auto �.+��� 
������ 
�������  EffectShadow3 J� Effect# [ $��10q�$��2��$��0��$!��معلومات الرحلة��$��5q�  GroupBoxTab#��[ .��� $��10q� �� �� �� �� ��  � ColumnDefinitions �� �� $
��auto ��� .+��� $��0 0 0 0q�$��0�� �� TextAlignment$ Center=�$��رحلة الذهاب���� $��60��  EffectShadow2# [ 
������ $��0��$��0�� 	 TextBoxExtend#��[	 (  "HandyControl.Controls.TitleElement   Title$ رقم الرحلة������ *Flight.DepartName+&   HandyControl.Controls.DatePicker  $��60��# [ $��1��$��0��   ShowClearButton$
 True��
������ $��1q�$ تاريخ الرحلة��(  "System.Windows.Controls.DatePicker   SelectedDate �� *Flight.DepartDate+&   HandyControl.Controls.TimePicker  $��60��# [ $��2��$��0��	   ShowClearButton$
	 True�� 
 TimePickerExtend#��[
 
������ $ وقت الرحلة��
   SelectedTime
 �� *Flight.DepartDate+�� $��60��# [ 
������ $��3��$��0��#��[	 $ مسار الرحلة������ *Flight.DepartItinerary+#  HandyControl.Controls.Divider     Orientation$ Vertical=�$
��20,0q�$��1��$��4��$��Center=�   LineStrokeThickness$ 2Q� J� Opacity$	 0.5Q�   
LineStroke
 �   ��� $��2��$ Center=�$��رحلة العودة���� $��60��# [ 
������ $��0��$��2��#��[	 $ رقم الرحلة������ *Flight.ReturnName+  $��60��# [ $��1��$��2��$
 True��  DatePickerExtend#��[ $ تاريخ الرحلة�� �� *Flight.ReturntDate+  $��60��# [ $��2��$��2��$
	 True��#��[
 $ وقت الرحلة��
 �� *Flight.ReturntDate+�� $��60��# [ 
������ $��3��$��2��#��[	 $ مسار الرحلة������ *Flight.ReturnItinerary+�� $��60��# [ 
������ $��4��$��0��$��2��#��[	 $ 
السعة������ *Flight.Capacity+��  �� Orientation$ 
Horizontal=�$��4��$��2��$
��10,0q�.+��� $��Center=�$��10q�$��AH���� $	��0,5q�$	��0.5q� d� 	IsChecked �� *	Flight.AH+ � LayoutTransform ��  �� ScaleX$	 1.5Q� �� ScaleY$	 1.5Q��� # [ $��LeftToRight=�$��1��$ 
Horizontal=�$��
10 0 10 10q�.+��� 
������ )  #HandyControl.Controls.BorderElement   CornerRadius$ 15��  	addFlight'  !HandyControl.Controls.IconElement   Geometry# [    Height$ 17Q����� *AddFlightCommand+*  $HandyControl.Controls.AdornerElement   Instance "  HandyControl.Controls.Poptip  
������    Content$ أضف رحلة��   Delay$ 0Q�$ 1Q�$��12(�   PlacementType$
 Left=�   HorizontalOffset$ 50Q�)  #HandyControl.Controls.LoadingCircle  $��30��$
��10,0q�y��� *AddFlightLoadingCircle+ �� 	Converter# [  