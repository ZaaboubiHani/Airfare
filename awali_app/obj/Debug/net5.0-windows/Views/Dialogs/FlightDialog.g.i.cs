﻿#pragma checksum "..\..\..\..\..\Views\Dialogs\FlightDialog.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "90437AFD1F57795C260DB88265DAA5E553ECA690"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using awali_app.Converters;
using awali_app.ViewModels.DialogViewModels;
using awali_app.Views.Dialogs;


namespace awali_app.Views.Dialogs {
    
    
    /// <summary>
    /// FlightDialog
    /// </summary>
    public partial class FlightDialog : System.Windows.Controls.Border, System.Windows.Markup.IComponentConnector {
        
        
        #line 90 "..\..\..\..\..\Views\Dialogs\FlightDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listViewHotels;
        
        #line default
        #line hidden
        
        
        #line 146 "..\..\..\..\..\Views\Dialogs\FlightDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Left;
        
        #line default
        #line hidden
        
        
        #line 147 "..\..\..\..\..\Views\Dialogs\FlightDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Right;
        
        #line default
        #line hidden
        
        
        #line 150 "..\..\..\..\..\Views\Dialogs\FlightDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBoxHotels;
        
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
            System.Uri resourceLocater = new System.Uri("/awali_app;component/views/dialogs/flightdialog.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\Dialogs\FlightDialog.xaml"
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
            this.listViewHotels = ((System.Windows.Controls.ListView)(target));
            return;
            case 2:
            this.Left = ((System.Windows.Controls.Button)(target));
            
            #line 146 "..\..\..\..\..\Views\Dialogs\FlightDialog.xaml"
            this.Left.Click += new System.Windows.RoutedEventHandler(this.Left_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Right = ((System.Windows.Controls.Button)(target));
            
            #line 147 "..\..\..\..\..\Views\Dialogs\FlightDialog.xaml"
            this.Right.Click += new System.Windows.RoutedEventHandler(this.Right_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.listBoxHotels = ((System.Windows.Controls.ListBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

