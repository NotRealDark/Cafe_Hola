﻿#pragma checksum "..\..\..\..\UAdmin\AdminWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "62F532F00A2833A46E2E851BE9BE74F591BDB3F5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Cafe.UAdmin;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
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


namespace Cafe.UAdmin {
    
    
    /// <summary>
    /// AdminWindow
    /// </summary>
    public partial class AdminWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\UAdmin\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox lblTitle;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\..\UAdmin\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCoffeeManagement;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\UAdmin\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnStaffManagement;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\UAdmin\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOrderManagement;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\UAdmin\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblAdminName;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\UAdmin\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLogout;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\UAdmin\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame frAdmin;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Cafe;component/uadmin/adminwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UAdmin\AdminWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.lblTitle = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.btnCoffeeManagement = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\..\UAdmin\AdminWindow.xaml"
            this.btnCoffeeManagement.Click += new System.Windows.RoutedEventHandler(this.btnCoffeeManagement_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnStaffManagement = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\..\UAdmin\AdminWindow.xaml"
            this.btnStaffManagement.Click += new System.Windows.RoutedEventHandler(this.btnStaffManagement_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnOrderManagement = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\..\UAdmin\AdminWindow.xaml"
            this.btnOrderManagement.Click += new System.Windows.RoutedEventHandler(this.btnOrderManagement_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.lblAdminName = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.btnLogout = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\..\UAdmin\AdminWindow.xaml"
            this.btnLogout.Click += new System.Windows.RoutedEventHandler(this.btnLogout_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.frAdmin = ((System.Windows.Controls.Frame)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

