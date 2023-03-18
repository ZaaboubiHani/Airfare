﻿#pragma checksum "..\..\..\..\..\Views\UserControls\HotelView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9C76ADABF1764918D533D56504083C5DDB0CADDC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Airfare.Converters;
using Airfare.Models;
using Airfare.ViewModels.UserControlViewModels;
using Airfare.Views.UserControls;
using CalcBinding;
using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Expression.Media;
using HandyControl.Expression.Shapes;
using HandyControl.Interactivity;
using HandyControl.Media.Animation;
using HandyControl.Media.Effects;
using HandyControl.Properties.Langs;
using HandyControl.Themes;
using HandyControl.Tools;
using HandyControl.Tools.Converter;
using HandyControl.Tools.Extension;
using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Airfare.Views.UserControls {
    
    
    /// <summary>
    /// HotelView
    /// </summary>
    public partial class HotelView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 14 "..\..\..\..\..\Views\UserControls\HotelView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Airfare.Views.UserControls.HotelView HotelUI;
        
        #line default
        #line hidden
        
        
        #line 383 "..\..\..\..\..\Views\UserControls\HotelView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox hotstsListBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.7.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Airfare;component/views/usercontrols/hotelview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\UserControls\HotelView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.7.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.HotelUI = ((Airfare.Views.UserControls.HotelView)(target));
            return;
            case 3:
            
            #line 101 "..\..\..\..\..\Views\UserControls\HotelView.xaml"
            ((System.Windows.Controls.TabControl)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TabControl_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 235 "..\..\..\..\..\Views\UserControls\HotelView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ExportButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 244 "..\..\..\..\..\Views\UserControls\HotelView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LinkButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 249 "..\..\..\..\..\Views\UserControls\HotelView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BreakButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.hotstsListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.7.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 2:
            
            #line 96 "..\..\..\..\..\Views\UserControls\HotelView.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.roomtypeListboxItem_MouseDown);
            
            #line default
            #line hidden
            break;
            case 4:
            
            #line 118 "..\..\..\..\..\Views\UserControls\HotelView.xaml"
            ((System.Windows.Controls.Border)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.spotContainer_MouseDown);
            
            #line default
            #line hidden
            
            #line 118 "..\..\..\..\..\Views\UserControls\HotelView.xaml"
            ((System.Windows.Controls.Border)(target)).Drop += new System.Windows.DragEventHandler(this.spotContainer_Drop);
            
            #line default
            #line hidden
            break;
            case 5:
            
            #line 142 "..\..\..\..\..\Views\UserControls\HotelView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveHostFromSpotButton_Click);
            
            #line default
            #line hidden
            break;
            case 6:
            
            #line 158 "..\..\..\..\..\Views\UserControls\HotelView.xaml"
            ((System.Windows.Controls.Border)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.groupContainer_MouseDown);
            
            #line default
            #line hidden
            break;
            case 7:
            
            #line 163 "..\..\..\..\..\Views\UserControls\HotelView.xaml"
            ((System.Windows.Controls.Border)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.spotContainer_MouseDown);
            
            #line default
            #line hidden
            
            #line 163 "..\..\..\..\..\Views\UserControls\HotelView.xaml"
            ((System.Windows.Controls.Border)(target)).Drop += new System.Windows.DragEventHandler(this.spotContainer_Drop);
            
            #line default
            #line hidden
            break;
            case 12:
            
            #line 394 "..\..\..\..\..\Views\UserControls\HotelView.xaml"
            ((System.Windows.Controls.Border)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.hostItem_MouseMove);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

