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


namespace WP7MPD.Images
{
    public partial class PivotPage1 : PhoneApplicationPage
    {
        public PivotPage1()
        {
            InitializeComponent();

            (App.Current as App).serverResponse = (App.Current as App).connection.Exec("list artist" + System.Environment.NewLine);
            List<string> artists = new List<string>();
            for (int i = 0; i < (App.Current as App).serverResponse.Message.Count; i++)
            {
                if (!(App.Current as App).serverResponse.Message[i].Equals("OK"))
                {
                    artists.Add((App.Current as App).serverResponse.Message[i]);
                }
            }

            while(artists.Count<=  (App.Current as App).artists){

                (App.Current as App).serverResponse = (App.Current as App).connection.readResponse();
                for (int i = 0; i < (App.Current as App).serverResponse.Message.Count; i++)
                {
                    if (!(App.Current as App).serverResponse.Message[i].Equals("OK"))
                    {
                        artists.Add((App.Current as App).serverResponse.Message[i]);
                    }
                }
                }

            artists.Sort();

            foreach (string i in artists)
            {
                if (!string.IsNullOrEmpty(i) && i.StartsWith("Artist:"))
                {
                    listBox2.Items.Add(i.Substring(7));
                }
            }


            listBox3.Items.Clear();
            (App.Current as App).serverResponse = (App.Current as App).connection.Exec("list album" + System.Environment.NewLine);
            List<string> albums = new List<string>();
            for (int i = 0; i < (App.Current as App).serverResponse.Message.Count; i++)
            {
                if (!(App.Current as App).serverResponse.Message[i].Equals("OK"))
                {
                    albums.Add((App.Current as App).serverResponse.Message[i]);
                }
            }

            while (albums.Count<=(App.Current as App).albums)
            {

                (App.Current as App).serverResponse = (App.Current as App).connection.readResponse();
                for (int i = 0; i < (App.Current as App).serverResponse.Message.Count; i++)
                {
                    if (!(App.Current as App).serverResponse.Message[i].Equals("OK"))
                    {
                        albums.Add((App.Current as App).serverResponse.Message[i]);
                    }
                }
            }
            albums.Sort();
            foreach (string i in albums)
            {
                if (!string.IsNullOrEmpty(i) && i.StartsWith("Album:"))
                {
                    listBox3.Items.Add(i.Substring(6));
                }

            }
            listBox3.Items.Add(listBox3.Items.Count);

            (App.Current as App).serverResponse = (App.Current as App).connection.Exec("playlistid" + System.Environment.NewLine);
            
            for (int i = 0; i < (App.Current as App).serverResponse.Message.Count; i++)
            {
                if ((App.Current as App).serverResponse.Message[i].StartsWith("Title:"))
                {
                    listBox1.Items.Add((App.Current as App).serverResponse.Message[i].Substring(6));
                }
            }
            (App.Current as App).serverResponse = (App.Current as App).connection.readResponse();
            for (int i = 0; i < (App.Current as App).serverResponse.Message.Count; i++)
            {
                if ((App.Current as App).serverResponse.Message[i].StartsWith("Title:"))
                {
                    listBox1.Items.Add((App.Current as App).serverResponse.Message[i].Substring(6));
                }
            }

        }

        

    

    }

}