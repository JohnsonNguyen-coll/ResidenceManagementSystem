﻿#pragma checksum "..\..\..\..\Citizen\SubmitHouseholdTransferWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2FF98A5E1227C2344D9BF8AD6DFE0062835FA7EB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Assignment_PRN;
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


namespace Assignment_PRN {
    
    
    /// <summary>
    /// SubmitHouseholdTransferWindow
    /// </summary>
    public partial class SubmitHouseholdTransferWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\..\Citizen\SubmitHouseholdTransferWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtFullName;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\Citizen\SubmitHouseholdTransferWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtEmail;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\Citizen\SubmitHouseholdTransferWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCurrentAddress;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Citizen\SubmitHouseholdTransferWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtNewAddress;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\Citizen\SubmitHouseholdTransferWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtReason;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ResidenceHub;V1.0.0.0;component/citizen/submithouseholdtransferwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Citizen\SubmitHouseholdTransferWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.txtFullName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.txtEmail = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtCurrentAddress = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtNewAddress = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.txtReason = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            
            #line 29 "..\..\..\..\Citizen\SubmitHouseholdTransferWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SubmitTransfer_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 30 "..\..\..\..\Citizen\SubmitHouseholdTransferWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseWindow_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

