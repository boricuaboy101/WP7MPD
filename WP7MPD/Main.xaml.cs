﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace WP7MPD
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void textBox1_Loaded(object sender, RoutedEventArgs e)
        {

            textBox1.Text = (App.Current as App).serverResponse;
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {


           (App.Current as App).connection.Send(textBox2.Text+"\r\n");
            (App.Current as App).serverResponse = (App.Current as App).connection.Receive();
            textBox1.Text = (App.Current as App).serverResponse;
        }

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}