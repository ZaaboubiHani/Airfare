   M S B A M L   `   `   `  ���� D  @awali_app, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:awali_app.Views.UserControlsawali_app.Views.UserControls  g8clr-namespace:awali_app.ViewModels.UserControlViewModels*awali_app.ViewModels.UserControlViewModels  ;"clr-namespace:awali_app.Convertersawali_app.Converters  ,    &awali_app.Views.UserControls.HotelView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     <local*clr-namespace:awali_app.Views.UserControls     S OHandyControl, Version=3.3.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  ^ ZMahApps.Metro.IconPacks, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137am	 iMahApps.Metro.IconPacks.BootstrapIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag
 cMahApps.Metro.IconPacks.BoxIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Codicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.Coolicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Entypo, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.EvaIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ak gMahApps.Metro.IconPacks.FeatherIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.FileIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.Fontaudio, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.FontAwesome, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Fontisto, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.ForkAwesome, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Ionicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.JamIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137am iMahApps.Metro.IconPacks.MaterialDesign, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al hMahApps.Metro.IconPacks.MaterialLight, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137af bMahApps.Metro.IconPacks.Microns, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Modern, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Octicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai eMahApps.Metro.IconPacks.PicolIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al hMahApps.Metro.IconPacks.PixelartIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai  eMahApps.Metro.IconPacks.RadixIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah! dMahApps.Metro.IconPacks.RemixIcon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai" eMahApps.Metro.IconPacks.RPGAwesome, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj# fMahApps.Metro.IconPacks.SimpleIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag$ cMahApps.Metro.IconPacks.Typicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137af% bMahApps.Metro.IconPacks.Unicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj& fMahApps.Metro.IconPacks.VaadinIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ak' gMahApps.Metro.IconPacks.WeatherIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah( dMahApps.Metro.IconPacks.Zondicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a}	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks!  	 
                        ! " # $ % & ' ( �vm8clr-namespace:awali_app.ViewModels.UserControlViewModels!  	 
                        ! " # $ % & ' ( kcv"clr-namespace:awali_app.Converters!  	 
                        ! " # $ % & ' ( -     >�Name$  HotelUI�� � DataContext ?   9awali_app.ViewModels.UserControlViewModels.HotelViewModel   � 	Resources 1   +awali_app.Converters.BoolToCollapsConverter   BoolToCollaps0  *awali_app.Converters.BoolToGenderConverter  BoolToGender8   2awali_app.Converters.ReverseBoolToCollapsConverter  ReverseBoolToCollaps%   awali_app.Converters.StartAtOne  
StartAtOne��%<   &	        &	      &	 
     &	              .���  � ColumnDefinitions �� $��* ��� $	��.3* � � RowDefinitions �� $
��auto ��� $��* �.+��� $��0��$��2��$��10q�$��المعتمرين��$��10q�.����   RegionBrush#���   �� CornerRadius$ 4��._��� $��RightToLeft=�.+���   VerticalListBoxItemStyle#��[ 
������ $��30��  EffectShadow2 J� Effect# [ $��10q�$��Disabled=����� *Months+ �� SelectedItem �� *SelectedMonth+���� .R��  K� IsItemsHost$
 True������ .R��� 	 �� Padding$		 5,2q�._��� ���� �� #��[ 
������ $��30��# [ $
��10,5q�$��Disabled=����� *SelectedFlights+ �� *SelectedFlight+���� .R�� $
 True������ .R��� $		 5,2q�._��� ���� �� $��0��$��1��$��10q�$��الفنادق��$��10q�.���  �� $��* ��� $
��auto � �� $
��auto ��� .+���   ClickableListBoxItemStyle#��[ $��5q����� *	RoomsList+���� .R��� $ 5������ *Color+._��� 
 ��Name$
 roomtypeListboxItem��-    �� Padding$
 10,2q����� *Type+�� $��5q�$��1��$��0��$��RightToLeft=����� *SelectedHotels+ �� *SelectedHotel+���� .R��� .+��� ���� *Name+ �� ContentTemplate �� .R��� $��Disabled=�$
��Auto=�.���� ����  �� Path$  DataContext.DisplayedSpots� �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=� �� ElementName$ HotelUI������ .R���  ��Name$ spotContainer��-   $	 10q�$ 10��$��5q� J� 	AllowDrop �� *	IsEmpty+���� *Color+._��� .+��� 
���   �$��Center=����� *Number+ �� StringFormat$  رقم الغرفة {0}���� 
���   �$��Center=���`� $ {1}/{0}��.1��� $ Capacity��� $ Taken��� ���� *Hosts+$ TwoWay=�$ PropertyChanged=�����  �� 
TargetType" ��.���  �� Property � �� �� Value$ Stretch=���  � ��$ Stretch=���  � ��$ 0q���  � ��$ 2q����� .R��� 
�������._���  �� Orientation$ 
Horizontal=�.+��� -    	 close'  !HandyControl.Controls.IconElement   Geometry# [ $��0,0,5,0q�
������ $��20��$��0q�$��20��   Height$ 7Q�.  (MahApps.Metro.IconPacks.PackIconMaterial  $��Center=�   Kind$ 	FaceWoman=�$��2q�y��� *Client.Gender+ �� 	Converter# [    $��Center=�$ FaceMan=�$��2q�y��� *Client.Gender+# [ �� $��Center=�$ 5q�$��RightToLeft=��� ��� *Client.FirstName+ �� ��� *Client.LastName+���� .R�� ]� $��10q�$��1��$��2��.+��� $
��Left=�)  #HandyControl.Controls.BorderElement   CornerRadius$ 15�� 	 save# [	 $ 17Q����� *SaveSpotCommand+*	  $HandyControl.Controls.AdornerElement 	  Instance "
  HandyControl.Controls.Poptip
   
  Content$ حفظ�� 
  Delay$ 0Q�  J� Opacity$  1Q�$��12(�! 
  PlacementType$
! Left=�" 
  HorizontalOffset$" 50Q��� $��Visible=�.��� $��RightToLeft=�$
��10,0q� �� $
��auto ��� $
��auto ��� $
��auto � �� �� �� .+��� 
���   �
�������$��Center=�$		 5,2q�$��1q�$ 0��$��0q�$��0��$��0��._��� $��نوع الغرف���� 
���   �
�������$��Center=�$		 5,2q�$��1q�$ 0��$��0q�$��0��$��1��._��� $��عدد الغرف���� 
���   �
�������$��Center=�$		 5,2q�$��1q�$ 0��$��0q�$��0��$��2��._��� $��عدد المعتمرين���� $��0��$��1��$��RightToLeft=� 
 ListBoxContainerTableStyle#��[
 
������ $��Disabled=�$��Disabled=����� *	RoomsList+ ��%�   '��       �� " ��.���  �@��CornerRadius$ 10����  �@��Padding$ 0q���  � ��$ 0q���  � ��$ 0q���  � ��
 ���� ���� " ��.���  � ��$ 0q���  � ��$ 0q����� .R�� $
 True������ .R��� $
	 15,2q����� *Color+._��� ���� �� $��1��$��1��$��RightToLeft=�#��[
 
������ 
������ $��0q�$��Disabled=�$��Disabled=����� *RoomsNumbersList+ ��%�   '��       �� " ��.���  �@��CornerRadius$ 0����  �@��Padding$ 0q���  � ��$ 0q���  � ��$ 0q���  � ��
 ���� ���� " ��.���  � ��$ 0q���  � ��$ 0q����� .R�� $
 True������ .R��� 
�������$
	 15,2q�._��� ���� �� $��2��$��1��$��Stretch=�$��Stretch=�$��RightToLeft=�#��[
 
������ $��0q�$��Disabled=�$��Disabled=����� *RoomsClientsNumbersList+ ��%�   '��       �� " ��.���  �@��CornerRadius$ 0����  �@��Padding$ 0q���  � ��$ 0q���  � ��$ 0q���  � ��
 ���� ���� " ��.���  � ��$ 0q���  � ��$ 0q����� .R�� $
 True������ .R��� 
�������$
	 15,2q�._��� ���� �� 
���   �
�������$��Center=�$		 5,2q�$��1q�$ 0��$��0q�$��2��$��0��._��� $��المجموع���� 
���   �
�������$��Center=�$		 5,2q�$��1q�$ 0��$��0q�$��2��$��1��._��� ���� *
TotalSpots+�� 
���   �
�������$��Center=�$		 5,2q�$��1q�$ 0��$��0q�$��2��$��2��._��� ���� *
TotalHosts+�� $��1��$��1��$��10q�$��المعتمرين��$��10q�.���� -   # ��Name$# hotstsListBox��
����������� *SelectedHostsWithoutSpots+���� " ��.���  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ 0q���  � ��$ 2q����� .R��� $ hostItem��-   $		 5,7q�$��RightToLeft=�$ 5��$��Stretch=�$��Stretch=����� *HotelRoom.Room.Color+._��� .+��� �� $��DemiBold"�$�الاسم :�� �� ��� *Client.FirstName+ �� ��� *Client.LastName+�� �� $��DemiBold"�$�نوع الغرفة :�� �� ��� *HotelRoom.Room.Type+