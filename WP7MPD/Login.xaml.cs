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
using Microsoft.Phone.Shell;

namespace WP7MPD
{
    public partial class Login : PhoneApplicationPage
    {
        // Constructor
        public Login()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            (App.Current as App).portNumber = Int32.Parse(textBox3.Text);
            (App.Current as App).serverName = textBox1.Text;
            (App.Current as App).connection = new MpcConnection();
            string connected = (App.Current as App).connection.Connect((App.Current as App).serverName, (App.Current as App).portNumber);
           textBox5.Text = connected;
           (App.Current as App).passwd = "password " + passwordBox1.Password + "\r\n" + " ";
           (App.Current as App).clientMessage = (App.Current as App).passwd;
           (App.Current as App).connection.Send((App.Current as App).clientMessage);
           (App.Current as App).serverResponse = (App.Current as App).connection.Receive();
           NavigationService.Navigate(new Uri("/PivotPage1.xaml", UriKind.Relative));
        }

      

     

        private void textBox1_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (textBox1.Text == "Server")
            {
                textBox1.Text = "";
            }
        }

        private void passwordBox1_TextInputStart(object sender, TextCompositionEventArgs e)
        {
            if (passwordBox1.Password == "******")
            {
                passwordBox1.Password = "";
            }
        }

        private void textBox3_TextInputStart(object sender, TextCompositionEventArgs e)
        {
            if (textBox1.Text == "Port")
            {
                textBox1.Text = "";
            }
        }

       

    }
}