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
    public partial class Page2 : PhoneApplicationPage
    {
     
        public Page2()
        {
            int repeat;
            int random;
            string paused;

            InitializeComponent();
            (App.Current as App).serverResponse = (App.Current as App).connection.Exec("status" + System.Environment.NewLine);
            int volume = int.Parse((App.Current as App).serverResponse.Message[0].Substring(7));
            textBlock6.Text = "" + volume;
            repeat = int.Parse((App.Current as App).serverResponse.Message[1].Substring(7));
            random = int.Parse((App.Current as App).serverResponse.Message[2].Substring(7));
            paused = (App.Current as App).serverResponse.Message[6].Substring(6);
            if (repeat == 0)
            {
                (App.Current as App).repeat = false;
                button9.Content = "Rep Off";
            }
            else
            {
                (App.Current as App).repeat = true;
                button9.Content = "Rep On";
            }

            if (random== 0)
            {
                (App.Current as App).random = false;
                button8.Content = "Rand Off";
            }
            else
            {
                (App.Current as App).random = true;
                button8.Content = "Rand On";
            }
            if (paused == "stop" || paused == "pause")
            {
                (App.Current as App).paused = true;
            }
            else
            {
                (App.Current as App).paused = false;
            }

            (App.Current as App).serverResponse = (App.Current as App).connection.Exec("currentsong" + System.Environment.NewLine);
            string checkPlaying=(App.Current as App).serverResponse.Message[0];
            if (checkPlaying == "OK" ||checkPlaying == "OKOK")
            {
                textBox1.Text = "N/A";
                textBox2.Text = "N/A";
                textBox3.Text = "N/A";
                textBox4.Text = "N/A";
            }
            else
            {
                textBox1.Text ="Song: "+ (App.Current as App).serverResponse.Message[4].Substring(6);
                int songLength = int.Parse((App.Current as App).serverResponse.Message[1].Substring(6));
                textBox2.Text = "Artist: "+(App.Current as App).serverResponse.Message[2].Substring(7);
                textBox3.Text = "Song Length: " + songLength / 60 + ":" + songLength % 60;
                textBox4.Text = "Album: " + (App.Current as App).serverResponse.Message[5].Substring(7);
            }
        }

       

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if ((App.Current as App).paused)
            {
              (App.Current as App).connection.Exec("play" + System.Environment.NewLine);
               
                (App.Current as App).paused = false;
                button4.Content = "Pause";

            }
            else
            {
                (App.Current as App).connection.Exec("pause 1" + System.Environment.NewLine);
              
                (App.Current as App).paused = true;
                button4.Content = "Play";
            }
            (App.Current as App).serverResponse = (App.Current as App).connection.Exec("currentsong" + System.Environment.NewLine);
            string checkPlaying = (App.Current as App).serverResponse.Message[0];
            if (checkPlaying == "OK" || checkPlaying == "OKOK")
            {
                textBox1.Text = "N/A";
                textBox2.Text = "N/A";
                textBox3.Text = "N/A";
                textBox4.Text = "N/A";
            }
            else
            {
                textBox1.Text = "Song: " + (App.Current as App).serverResponse.Message[4].Substring(6);
                int songLength = int.Parse((App.Current as App).serverResponse.Message[1].Substring(6));
                textBox2.Text = "Artist: " + (App.Current as App).serverResponse.Message[2].Substring(7);
                textBox3.Text = "Song Length: " + songLength / 60 + ":" + songLength % 60;
                textBox4.Text = "Album: " + (App.Current as App).serverResponse.Message[5].Substring(7);
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {

            (App.Current as App).connection.Exec("next" + System.Environment.NewLine);
            (App.Current as App).serverResponse = (App.Current as App).connection.Exec("currentsong" + System.Environment.NewLine);
            string checkPlaying = (App.Current as App).serverResponse.Message[0];
            if (checkPlaying == "OK" || checkPlaying == "OKOK")
            {
                textBox1.Text = "N/A";
                textBox2.Text = "N/A";
                textBox3.Text = "N/A";
                textBox4.Text = "N/A";
            }
            else
            {
                textBox1.Text = "Song: " + (App.Current as App).serverResponse.Message[4].Substring(6);
                int songLength = int.Parse((App.Current as App).serverResponse.Message[1].Substring(6));
                textBox2.Text = "Artist: " + (App.Current as App).serverResponse.Message[2].Substring(7);
                textBox3.Text = "Song Length: " + songLength / 60 + ":" + songLength % 60;
                textBox4.Text = "Album: " + (App.Current as App).serverResponse.Message[5].Substring(7);
            }
          
           
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            (App.Current as App).connection.Exec("previous" + System.Environment.NewLine);

            (App.Current as App).serverResponse = (App.Current as App).connection.Exec("currentsong" + System.Environment.NewLine);
            string checkPlaying = (App.Current as App).serverResponse.Message[0];
            if (checkPlaying == "OK" || checkPlaying == "OKOK")
            {
                textBox1.Text = "N/A";
                textBox2.Text = "N/A";
                textBox3.Text = "N/A";
                textBox4.Text = "N/A";
            }
            else
            {
                textBox1.Text = "Song: " + (App.Current as App).serverResponse.Message[4].Substring(6);
                int songLength = int.Parse((App.Current as App).serverResponse.Message[1].Substring(6));
                textBox2.Text = "Artist: " + (App.Current as App).serverResponse.Message[2].Substring(7);
                textBox3.Text = "Song Length: " + songLength / 60 + ":" + songLength % 60;
                textBox4.Text = "Album: " + (App.Current as App).serverResponse.Message[5].Substring(7);
            }
          
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            (App.Current as App).serverResponse = (App.Current as App).connection.Exec("status" + System.Environment.NewLine);
            int volume = int.Parse((App.Current as App).serverResponse.Message[0].Substring(7));
            (App.Current as App).connection.Exec("setvol "+(volume+5) + System.Environment.NewLine);
            if (volume == 105 || volume == -5)
            {
                textBlock6.Text = "" + (volume + 5);
            }
            else
            {
                textBlock6.Text = "" + (volume);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            (App.Current as App).serverResponse = (App.Current as App).connection.Exec("status" + System.Environment.NewLine);
            int volume = int.Parse((App.Current as App).serverResponse.Message[0].Substring(7));
            (App.Current as App).connection.Exec("setvol " + (volume - 5) + System.Environment.NewLine);
            if (volume==105 || volume==-5)
            {
                textBlock6.Text = "" + (volume - 5);
            }
            else
            {
                textBlock6.Text = "" + (volume);
            }
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
             (App.Current as App).connection.Exec("stop" + System.Environment.NewLine);
             textBox1.Text = "N/A";
             textBox2.Text = "N/A";
             textBox3.Text = "N/A";
             textBox4.Text = "N/A";
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            if ((App.Current as App).repeat)
            {
                (App.Current as App).connection.Exec("repeat 0" + System.Environment.NewLine);
                (App.Current as App).repeat = false;
                button9.Content = "Rep Off";
            }
            else
            {
               (App.Current as App).connection.Exec("repeat 1" + System.Environment.NewLine);
                (App.Current as App).repeat= true;
                button9.Content = "Rep On";
            }
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            if ((App.Current as App).random)
            {
                (App.Current as App).connection.Exec("random 0" + System.Environment.NewLine);
                (App.Current as App).random = false;
                button8.Content = "Rand Off";
            }
            else
            {
                (App.Current as App).serverResponse = (App.Current as App).connection.Exec("random 1" + System.Environment.NewLine);
                (App.Current as App).random = true;
                button8.Content = "Rand On";
            }
        }
    }
}