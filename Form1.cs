using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Weather_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "Please enter city";
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            button1.Click += button1_Click;
            label1.BackColor = Color.Transparent;
            label1.Visible = false;
            label2.BackColor = Color.Transparent;
            label2.Visible = false;
            label3.BackColor = Color.Transparent;
            label3.Visible = false;
            label4.BackColor = Color.Transparent;
            label4.Visible = false;
            label5.BackColor = Color.Transparent;
            label5.Visible = false;
            label6.BackColor = Color.Transparent;
            label6.Visible = false;
            label7.BackColor = Color.Transparent;
            label7.Visible = false;
            label8.BackColor = Color.Transparent;
            label8.Visible = false;
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Visible = false;
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Visible = false;
            //richTextBox1.Text = string.Format("{0}°C", degrees);
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel2.BackColor = Color.Transparent;
            tableLayoutPanel3.BackColor = Color.Transparent;
            tableLayoutPanel4.BackColor = Color.Transparent;
            panel1.BackColor = Color.Transparent;
            webBrowser1.Visible = false;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                textBox1.ForeColor = SystemColors.GrayText;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string city = textBox1.Text;

            string url1 = "http://api.openweathermap.org/data/2.5/weather?q=" + city + " &appid=1fbff9ce38ef2e2e896b7b80c83507c7&units=metric";
            //instad of sample type api + instead of their id type your id + &units=metric to get temperature in Celusius
            //example: http://api.openweathermap.org/data/2.5/weather?q=sarajevo&appid=1fbff9ce38ef2e2e896b7b80c83507c7&units=metric

            WebClient wc = new WebClient();
            string mystring1 = wc.DownloadString(url1);                       //Download whole json file
            JObject json = JObject.Parse(mystring1);                          //Take json object

            var item1 = json["main"]["temp"];
            label1.Text = (string)item1;                                      //Display temperature when button is pressed            
            label1.Visible = true;

            var item2 = json["weather"][0]["description"];      
            label2.Text = "Condition           " + (string)item2;             //Display description when button is pressed
            label2.Visible = true;

            var item3 = json["main"]["pressure"];             
            label3.Text = "Pressure           " + (string)item3;              //Display pressure when button is pressed
            label3.Visible = true;

            var item4 = json["main"]["humidity"];          
            label4.Text = "Humidity            " + (string)item4;             //Display humidity when button is pressed
            label4.Visible = true;

            var item5 = json["wind"]["speed"];          
            label5.Text = "Wind Speed     " + (string)item5;                  //Display wind speed when button is pressed
            label5.Visible = true;

            var item6 = json["weather"][0]["icon"];                           //Take icon id from json
            string url2 = "http://openweathermap.org/img/w/" + item6 + ".png";
            pictureBox1.ImageLocation = url2;                                 //Display weather icon when button is pressed
            pictureBox1.Visible = true;

            Type type = typeof(DateTime);

            var item7 = json["sys"]["sunrise"];
            var result1 = item7.ToObject<double>();
            var epoch1 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            epoch1 = epoch1.AddSeconds(result1);

            label6.Text = "Sunrise            " + epoch1;                      //Display sunrise time when button is pressed
            label6.Visible = true;

            var item8 = json["sys"]["sunset"];
            var result2 = item8.ToObject<double>();
            var epoch2 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            epoch2 = epoch2.AddSeconds(result2);

            label7.Text = "Sunset            " + epoch2;                       //Display sunset time when button is pressed
            label7.Visible = true;

            var item9 = json["name"];
            label8.Text = (string)item9;                                      //Display name of the city when button is pressed
            label8.Visible = true;

            var item10 = json["sys"]["country"];                              //Take country id from json
            string url3 = "https://flagpedia.net/data/flags/normal/" + item10 + ".png";
            string urllower = url3.ToLower();                                 //item10 has to be in lower letter
            pictureBox2.ImageLocation = urllower;                             //Display flag when button is pressed
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;           //Fit image to picture box
            pictureBox2.Visible = true;

            webBrowser1.Visible = true;

            //My code for google map:
            //string url4 = "http://maps.google.com/maps?q=" + city;
            //webBrowser1.Navigate(city.ToString());  
                      
            //Code from YT tutorial
            //https://www.youtube.com/watch?v=lRoAjs8RwfE

              try
             {
                 StringBuilder quearryaddress = new StringBuilder();
                 quearryaddress.Append("http://maps.google.com/maps?q=");

                 if(city != string.Empty)
                 {
                     quearryaddress.Append(city + "," + "+");
                 }

                 webBrowser1.Navigate(quearryaddress.ToString());
             }

             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message.ToString(), "Error");
             }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)         //Enter instead of button
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
                e.SuppressKeyPress = true;                                   //These two lines to turn of sound
                e.Handled = true;
            }
        }
    }
}

