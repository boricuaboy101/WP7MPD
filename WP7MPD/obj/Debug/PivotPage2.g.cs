﻿#pragma checksum "C:\Users\Josh\documents\visual studio 2010\Projects\WP7MPD\WP7MPD\PivotPage2.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AC758F511CDA1AABC7A9AB60333CCE8B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace WP7MPD.Images {
    
    
    public partial class PivotPage1 : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Pivot library;
        
        internal Microsoft.Phone.Controls.PivotItem artists;
        
        internal System.Windows.Controls.ListBox listBox2;
        
        internal Microsoft.Phone.Controls.PivotItem albums;
        
        internal System.Windows.Controls.ListBox listBox3;
        
        internal System.Windows.Controls.ListBox listBox1;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/WP7MPD;component/PivotPage2.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.library = ((Microsoft.Phone.Controls.Pivot)(this.FindName("library")));
            this.artists = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("artists")));
            this.listBox2 = ((System.Windows.Controls.ListBox)(this.FindName("listBox2")));
            this.albums = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("albums")));
            this.listBox3 = ((System.Windows.Controls.ListBox)(this.FindName("listBox3")));
            this.listBox1 = ((System.Windows.Controls.ListBox)(this.FindName("listBox1")));
        }
    }
}

