   M S B A M L   `   `   `  ���� B  >Airfare, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Airfare.Views.DialogsAirfare.Views.Dialogs  Y1clr-namespace:Airfare.ViewModels.DialogViewModels#Airfare.ViewModels.DialogViewModels  T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls R Nnetstandard, Version=2.1.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd514)clr-namespace:System;assembly=netstandardSystem 7 clr-namespace:Airfare.ConvertersAirfare.Converters  &     Airfare.Views.Dialogs.RoomDialog   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     5local#clr-namespace:Airfare.Views.Dialogs     @vm1clr-namespace:Airfare.ViewModels.DialogViewModels     S	 OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol 	 Emah;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro 	 3sys)clr-namespace:System;assembly=netstandard 	 )cv clr-namespace:Airfare.Converters 	   �� CornerRadius$  10��
�������$��RightToLeft=�$	��400��$
��auto�� � DataContext =   7Airfare.ViewModels.DialogViewModels.RoomDialogViewModel   � 	Resources /   )Airfare.Converters.BoolToCollapsConverter   BoolToCollaps.  (Airfare.Converters.BoolToGenderConverter  BoolToGender6   0Airfare.Converters.ReverseBoolToCollapsConverter  ReverseBoolToCollaps#   Airfare.Converters.StartAtOne  
StartAtOne��%<   &	        &	      &	 
     &	              ._�' 	 !HandyControl.Controls.SimplePanel  .+���  � Cursor$
 Hand��$��22��$��22��  
ButtonIcon#��[   PrimaryBrush#���    ErrorGeometry' 	 !HandyControl.Controls.IconElement   Geometry# [ $��0q�$��Right=�$	��Top=�$��0,4,4,0q����� *CloseDialogCommand+�� $��16,18q�.+��� $��75��$��20(�$��0,0,0,7q�  TextBoxExtend#��[ ( 	 "HandyControl.Controls.TitleElement   TitlePlacement$	 top=�   Title$ نوع الغرفة��$��Center=����� *Type+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=��� $��75��$��20(�$��0,0,0,7q�#��[ $	 top=�$ سعة الغرفة��$��Center=����� *
Capacity+$ PropertyChanged=�$ TwoWay=�.���� $
��0,10q�.+��� $��20(�$��
اللون��$��5 0 0 5q�(	  "MahApps.Metro.Controls.ColorPicker	  
���   �$��40�� 	 	  IsColorPalettesTabVisible$	 False��,
  &MahApps.Metro.Controls.ColorPickerBase
 
  SelectedColor
 �� *
Room.Color+�� $
 Hand��$��18(�$��40��
���   �$��10q�$
��Left=�$��
إدخال��y��� *
UpdateMode+ �� 	Converter# [ ���� *AddRoomCommand+�� $
 Hand��$��18(�$��40��
���   �$��10q�$
��Left=�$��
تحديث��y��� *
UpdateMode+# [  ���� *UpdateRoomCommand+