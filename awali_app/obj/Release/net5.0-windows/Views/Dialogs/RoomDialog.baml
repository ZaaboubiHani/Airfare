   M S B A M L   `   `   `  ���� D  @awali_app, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:awali_app.Views.Dialogsawali_app.Views.Dialogs  ]3clr-namespace:awali_app.ViewModels.DialogViewModels%awali_app.ViewModels.DialogViewModels  T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls E'clr-namespace:awali_app.ValidationRulesawali_app.ValidationRules  R Nnetstandard, Version=2.1.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd514)clr-namespace:System;assembly=netstandardSystem ;"clr-namespace:awali_app.Convertersawali_app.Converters  (    "awali_app.Views.Dialogs.RoomDialog   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     7local%clr-namespace:awali_app.Views.Dialogs     Bvm3clr-namespace:awali_app.ViewModels.DialogViewModels     S	 OHandyControl, Version=3.3.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol 	 Emah;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro 	 0vr'clr-namespace:awali_app.ValidationRules 	 3sys)clr-namespace:System;assembly=netstandard 	 +cv"clr-namespace:awali_app.Converters 	   �� CornerRadius$  10��
�������$��RightToLeft=�$	��400��$
��auto�� � DataContext ?   9awali_app.ViewModels.DialogViewModels.RoomDialogViewModel   � 	Resources 1   +awali_app.Converters.BoolToCollapsConverter   BoolToCollaps0  *awali_app.Converters.BoolToGenderConverter  BoolToGender8   2awali_app.Converters.ReverseBoolToCollapsConverter  ReverseBoolToCollaps%   awali_app.Converters.StartAtOne  
StartAtOne��%<   &	        &	      &	 
     &	              ._�' 	 !HandyControl.Controls.SimplePanel  .+��� $��22��$��22��  
ButtonIcon#��[   PrimaryBrush#���    ErrorGeometry' 	 !HandyControl.Controls.IconElement   Geometry# [ $��0q�$��Right=�$	��Top=�$��0,4,4,0q����� *CloseDialogCommand+�� $��16,18q�.+��� 
���   �$��0,0,0,7q�  TextBoxExtend#��[ ( 	 "HandyControl.Controls.TitleElement   TitlePlacement$	 top=�   Title$ نوع الغرفة��$��Center=����� *Type+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=��� 
���   �$��0,0,0,7q�#��[ $	 top=�$ سعة الغرفة��$��Center=�.������  �� Path$ Capacity�$ PropertyChanged=�$ TwoWay=�	 �� ValidationRules	 =	   7awali_app.ValidationRules.NegativeNumbersValidationRule	  .���� $
��0,10q�.+��� $��
اللون��$��5 0 0 5q�(
  "MahApps.Metro.Controls.ColorPicker
   
 
  IsColorPalettesTabVisible$
 False��,  &MahApps.Metro.Controls.ColorPickerBase   SelectedColor �� *
Room.Color+�� 
���   �$��10q�$
��Left=�$��
إدخال��y��� *
UpdateMode+ �� 	Converter# [ ���� *AddRoomCommand+�� 
���   �$��10q�$
��Left=�$��
تحديث��y��� *
UpdateMode+# [  ���� *UpdateRoomCommand+