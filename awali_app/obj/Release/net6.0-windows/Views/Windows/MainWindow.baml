   M S B A M L   `   `   `  ���� B  >Airfare, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:Airfare.ViewModels.WindowsViewModels$Airfare.ViewModels.WindowsViewModels  T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls     Airfare.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      Bd2http://schemas.microsoft.com/expression/blend/2008      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Avm2clr-namespace:Airfare.ViewModels.WindowsViewModels     Kmah;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol     LightBlueColor#���   $��	Comfortaa)�  � Title$  Airfare��$	��600��$	��900�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=� � Icon$ /Icons/logo��#���    � DataContext 8   2Airfare.ViewModels.WindowsViewModels.MainViewModel  .��� $��RightToLeft=�#���    � ColumnDefinitions �� $
��Auto ��� .+���  �� CornerRadius$ 	0 10 10 0��  SecundaryBackgroundColor#���  ._�� -    �Name$ gridMenu��$��60�� � RowDefinitions �� $��40 ��� $��80 ��� $��* �.+��� $��1��$	��5,0q�$��Center=�$	��Top=�.+��� $��/Icons/logo��$��80��d� -   	 d�Name$	 btnMenu��$
��Left=�$��0q�
������ $��Center=�  MenuIconStyle#��[ $��15 0 0 0q�
 � Triggers
 :�  :� RoutedEvent$ ToggleButton.Unchecked��.[��� .���� -    ��Name$ HideStackPanel��.��W�  �� 
TargetName$ gridMenu�� �� TargetProperty$ Width� g� 	BeginTime	 0:0:0 W� From 200	 W� To 60 g� Duration$ 0:0:0.3D�W� $ hotel_poptip��$ Opacity� 0 1	 0:0:0$ 0:0:0.1D�W� $ room_poptip��$ Opacity� 0 1	 0:0:0$ 0:0:0.1D�W� $ flights_poptip��$ Opacity� 0 1	 0:0:0$ 0:0:0.1D�W� $ seasons_poptip��$ Opacity� 0 1	 0:0:0$ 0:0:0.1D�W� $ clients_poptip��$ Opacity� 0 1	 0:0:0$ 0:0:0.1D�W� $ statistics_poptip��$ Opacity� 0 1	 0:0:0$ 0:0:0.1D�:� $ ToggleButton.Checked��.[��� .���� -   $ ShowStackPanel��.��W� $ gridMenu��$ Width�	 0:0:0 60 200$ 0:0:0.3D�W� $ hotel_poptip��$ Opacity� 1 0	 0:0:0$ 0:0:0.1D�W� $ room_poptip��$ Opacity� 1 0	 0:0:0$ 0:0:0.1D�W� $ flights_poptip��$ Opacity� 1 0	 0:0:0$ 0:0:0.1D�W� $ seasons_poptip��$ Opacity� 1 0	 0:0:0$ 0:0:0.1D�W� $ clients_poptip��$ Opacity� 1 0	 0:0:0$ 0:0:0.1D�W� $ statistics_poptip��$ Opacity� 1 0	 0:0:0$ 0:0:0.1D�]� $��2��.+�	� -    	�Name$ Seasons_poptip�� d� 	IsChecked$
 TrueY�$��المواسم��$	��Top=�  MenuRadioButtonStyle#���   	 kabba
 � Tag# �  $��0 5 0 5q�*  $HandyControl.Controls.AdornerElement   Instance "  HandyControl.Controls.Poptip  -     Name$ seasons_poptip��
���   �
���   �   Content$ المواسم��   Delay$ 0Q� J� Opacity$ 1Q�$��15(�   PlacementType$ Right=�   HorizontalOffset$	 -57Q�	� -   $ Flights_button��$��الرحلات��$	��Top=�#���   	 plane# �  $��1 5 0 5q�{��� *EnableFeatures+   -   $ flights_poptip��
���   �
���   �$ الرحلات��$ 0Q�$ 1Q�$��15(�$ Right=�$	 -52Q�	� -	   $ Room_button��$��
الغرف��$	��Top=�#���    door# �  $��0 5 0 5q�{��� *EnableFeatures+   -
   $ room_poptip��
���   �
���   �$ 
الغرف��$
��auto��$ 0Q�$ 1Q�$��15(�$ Right=�$	 -46Q�	� -   $ Hotel_button��$��التسكين��$	��Top=�#���    bank# �  $��0 5 0 5q�{��� *EnableFeatures+   -   $ hotel_poptip��
���   �
���   �$ التسكين��$ 0Q�$ 1Q�$��15(�$ Right=�$	 -55Q�	� -   $ Clients_button��$��المتعاملين��$	��Top=�#���   	 users# �  $��0 5 0 5q�{��� *EnableFeatures+   -   $ clients_poptip��
���   �
���   �$ المتعاملين��$ 0Q�$ 1Q�$��15(�$ Right=�$	 -67Q�	� -   $ Statistics_button��$��الإحصائيات��$	��Top=�#���   		 chart# � 	 $��0 5 0 5q�{��� *EnableFeatures+   -   $ statistics_poptip��
���   �
���   �$ الإحصائيات��$ 0Q�$ 1Q�$��15(�$ Right=�$	 -67Q�	� -   $ Settings_button��$��الإعدادات��$��Bottom=�#���   
 settings# � 
 $��0 5 0 5q�{��� *EnableFeatures+   -   $ settings_poptip��
���   �
���   �$ الإعدادات��$ 0Q�$ 1Q�$��15(�$ Right=�$	 -62Q��� $��Bottom=��� $��1��$��10q�$ 15��#���  ._�� .+� � -     �Name$ PagesNavigation��  � NavigationUIVisibility$ Hidden=�
������ 