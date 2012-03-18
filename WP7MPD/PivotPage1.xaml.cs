using System;
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

namespace WP7MPD
{
    public partial class PivotPage1 : PhoneApplicationPage
    {
        public PivotPage1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            listBox1.Items.Clear();
            (App.Current as App).serverResponse = (App.Current as App).connection.Exec(textBox2.Text + System.Environment.NewLine);
            listBox1.Items.Add((App.Current as App).serverResponse.ToString());
            
            Testing.SelectedItem = Output;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            textBox2.Text = "stats";
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            textBox2.Text = "lsinfo";
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            textBox2.Text = "search";
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            textBox2.Text = "count";
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            textBox2.Text = "password";
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            textBox2.Text = "ping";
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PanoramaPage1.xaml", UriKind.Relative));
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page2.xaml", UriKind.Relative));

        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add((App.Current as App).serverResponse.ToString());
            if ((App.Current as App).serverResponse[0].Equals( "Operation Timeout"))
            {
                (App.Current as App).connection.Connect((App.Current as App).serverName, (App.Current as App).portNumber);
               (App.Current as App).connection.Exec((App.Current as App).passwd);
            }
        }

    
      
    }
}