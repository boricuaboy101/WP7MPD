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
    public partial class PanoramaPage1 : PhoneApplicationPage
    {
        public PanoramaPage1()
        {
            InitializeComponent();
        }

        private void listBox2_Loaded(object sender, RoutedEventArgs e)
        {

            if ((App.Current as App).panFirstLoad)
            {
                int artists=0;
                int albums=0;
                (App.Current as App).serverResponse = (App.Current as App).connection.readResponse();
                (App.Current as App).serverResponse = (App.Current as App).connection.Exec("stats" + System.Environment.NewLine);
                listBox2.Items.Add((App.Current as App).serverResponse.Message[0]);
                listBox2.Items.Add((App.Current as App).serverResponse.Message[1]);
                listBox2.Items.Add((App.Current as App).serverResponse.Message[2]);
                listBox2.Items.Add((App.Current as App).serverResponse.Message[3]+"s");
                listBox2.Items.Add((App.Current as App).serverResponse.Message[4] + "s");
                listBox2.Items.Add((App.Current as App).serverResponse.Message[5] + "s");
                (App.Current as App).panFirstLoad = false;
                artists = int.Parse((App.Current as App).serverResponse.Message[0].Substring(8));
                albums = int.Parse((App.Current as App).serverResponse.Message[1].Substring(7));
                (App.Current as App).artists = artists;
                (App.Current as App).albums = albums;

            }
            else
            {
                listBox2.Items.Clear();

               // (App.Current as App).serverResponse = (App.Current as App).connection.readResponse();
                (App.Current as App).serverResponse = (App.Current as App).connection.Exec("stats" + System.Environment.NewLine);
                listBox2.Items.Add((App.Current as App).serverResponse.Message[0]);
                listBox2.Items.Add((App.Current as App).serverResponse.Message[1]);
                listBox2.Items.Add((App.Current as App).serverResponse.Message[2]);
                listBox2.Items.Add((App.Current as App).serverResponse.Message[3] + "s");
                listBox2.Items.Add((App.Current as App).serverResponse.Message[4] + "s");
                listBox2.Items.Add((App.Current as App).serverResponse.Message[5] + "s");
            }
        }

        private void ListBoxItem_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page2.xaml", UriKind.Relative));
        }

        private void ListBoxItem_Tap_1(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PivotPage2.xaml", UriKind.Relative));
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}