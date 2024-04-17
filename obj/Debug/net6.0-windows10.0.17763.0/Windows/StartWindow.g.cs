﻿#pragma checksum "..\..\..\..\Windows\StartWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "694CA63C37795074931AF19C17C2E8A5E3E720F3"
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
using TableForGto;
using TableForGto.Commands;


namespace TableForGto.Windows {
    
    
    /// <summary>
    /// StartWindow
    /// </summary>
    public partial class StartWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 12 "..\..\..\..\Windows\StartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl _tabControl;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\Windows\StartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem _startMenu;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\Windows\StartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox _recentProjectList;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\Windows\StartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button _createNewProjectButton;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\Windows\StartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button _openProjectButton;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\Windows\StartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem _createNewProjectMenu;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\..\Windows\StartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox _projectName;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\..\Windows\StartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox _projectPath;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\..\Windows\StartWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button _backButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TableForGto;component/windows/startwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Windows\StartWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 11 "..\..\..\..\Windows\StartWindow.xaml"
            ((TableForGto.Windows.StartWindow)(target)).Closed += new System.EventHandler(this.OnWindowClosed);
            
            #line default
            #line hidden
            return;
            case 2:
            this._tabControl = ((System.Windows.Controls.TabControl)(target));
            return;
            case 3:
            this._startMenu = ((System.Windows.Controls.TabItem)(target));
            return;
            case 4:
            this._recentProjectList = ((System.Windows.Controls.ListBox)(target));
            return;
            case 6:
            this._createNewProjectButton = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\..\..\Windows\StartWindow.xaml"
            this._createNewProjectButton.Click += new System.Windows.RoutedEventHandler(this.OnCreateNewProjectButtonClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this._openProjectButton = ((System.Windows.Controls.Button)(target));
            
            #line 60 "..\..\..\..\Windows\StartWindow.xaml"
            this._openProjectButton.Click += new System.Windows.RoutedEventHandler(this.OnOpenProjectButtonClick);
            
            #line default
            #line hidden
            return;
            case 8:
            this._createNewProjectMenu = ((System.Windows.Controls.TabItem)(target));
            return;
            case 9:
            
            #line 69 "..\..\..\..\Windows\StartWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.CreateNewProjectCommandExecuted);
            
            #line default
            #line hidden
            return;
            case 10:
            this._projectName = ((System.Windows.Controls.TextBox)(target));
            
            #line 87 "..\..\..\..\Windows\StartWindow.xaml"
            this._projectName.Loaded += new System.Windows.RoutedEventHandler(this.OnProjectNameTextBoxLoaded);
            
            #line default
            #line hidden
            return;
            case 11:
            this._projectPath = ((System.Windows.Controls.TextBox)(target));
            
            #line 92 "..\..\..\..\Windows\StartWindow.xaml"
            this._projectPath.Initialized += new System.EventHandler(this.OnProjectPathInitialized);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 94 "..\..\..\..\Windows\StartWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnProjectLocationButtonClick);
            
            #line default
            #line hidden
            return;
            case 13:
            this._backButton = ((System.Windows.Controls.Button)(target));
            
            #line 99 "..\..\..\..\Windows\StartWindow.xaml"
            this._backButton.Click += new System.Windows.RoutedEventHandler(this.OnBackButtonClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 5:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.Controls.Control.MouseDoubleClickEvent;
            
            #line 45 "..\..\..\..\Windows\StartWindow.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.OnProjectDoubleClick);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}
