﻿#pragma checksum "..\..\..\..\..\GUI\Police\HouseholdDetailsDialog.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B5344C6586ADE34189026C03C41F6FA2DC9AEAAB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace ResidenceHub.GUI.Police {
    
    
    /// <summary>
    /// HouseholdDetailsDialog
    /// </summary>
    public partial class HouseholdDetailsDialog : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 36 "..\..\..\..\..\GUI\Police\HouseholdDetailsDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblHouseholdId;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\..\GUI\Police\HouseholdDetailsDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblCreatedDate;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\..\GUI\Police\HouseholdDetailsDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblHeadOfHousehold;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\..\GUI\Police\HouseholdDetailsDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblAddress;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\..\GUI\Police\HouseholdDetailsDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgMembers;
        
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
            System.Uri resourceLocater = new System.Uri("/ResidenceHub;component/gui/police/householddetailsdialog.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\GUI\Police\HouseholdDetailsDialog.xaml"
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
            this.lblHouseholdId = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.lblCreatedDate = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.lblHeadOfHousehold = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.lblAddress = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.dgMembers = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 6:
            
            #line 71 "..\..\..\..\..\GUI\Police\HouseholdDetailsDialog.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

