using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyNewsApp_Desktop_
{
    public partial class Form1 : Form
    {
        const string US_URI = "https://newsapi.org/v2/top-headlines?" +"country=us&" +"apiKey=27e5ee2f6d704aaa8be077730cd1d85c";
        const string BBC_Sport_URI_ = "https://newsapi.org/v2/everything?" +"q=Apple&" +"from=2020-01-04&" +"sortBy=popularity&" +"apiKey=27e5ee2f6d704aaa8be077730cd1d85c";
        const string BBC_News_URI = "https://newsapi.org/v2/top-headlines?"+"sources=bbc-news&"+"apiKey=27e5ee2f6d704aaa8be077730cd1d85c";
        const string Nigeria_URI = "https://newsapi.org/v2/top-headlines?country=ng&apiKey=27e5ee2f6d704aaa8be077730cd1d85c";
        const string Isreal_URI = "https://newsapi.org/v2/top-headlines?country=il&apiKey=27e5ee2f6d704aaa8be077730cd1d85c";
        const string Uk_URI = "https://newsapi.org/v2/top-headlines?country=gb&apiKey=27e5ee2f6d704aaa8be077730cd1d85c";
        const string LondonSports_URI = "https://newsapi.org/v2/top-headlines?country=gb&category=sports&apiKey=27e5ee2f6d704aaa8be077730cd1d85c";
        const string LondonTechNews_URI = "https://newsapi.org/v2/top-headlines?country=gb&category=technology&apiKey=27e5ee2f6d704aaa8be077730cd1d85c";
        const string LondonScience_URI = "https://newsapi.org/v2/top-headlines?country=gb&category=science&apiKey=27e5ee2f6d704aaa8be077730cd1d85c";
        const string USTech_URI = "https://newsapi.org/v2/top-headlines?country=us&category=technology&apiKey=27e5ee2f6d704aaa8be077730cd1d85c";
        const string BitCoin_URI = "https://newsapi.org/v2/everything?q=bitcoin&from=2019-12-05&sortBy=publishedAt&apiKey=27e5ee2f6d704aaa8be077730cd1d85c";


        List<string> URI_Collection;
        public News.Rootobject Root { get; set; }
        public int NewsCounter { get; set; } = 0;
        public int SourceCounter { get; set; } = 0;
        private void SetupList()
        {
            try
            {
                this.URI_Collection = new List<string>();
                this.URI_Collection.Add(BBC_News_URI);
                this.URI_Collection.Add(BBC_Sport_URI_);
                this.URI_Collection.Add(US_URI);
                this.URI_Collection.Add(Nigeria_URI);
                this.URI_Collection.Add(Isreal_URI);
                this.URI_Collection.Add(Uk_URI);
                this.URI_Collection.Add(LondonSports_URI);
                this.URI_Collection.Add(LondonTechNews_URI);
                this.URI_Collection.Add(LondonScience_URI);
                this.URI_Collection.Add(USTech_URI);




            }
            catch (Exception ex) when (ex is System.Net.WebException || ex is IOException || ex is System.NullReferenceException)
            {
                MessageBox.Show("Check Internet Connection ");
            }
           
            //this.URI_Collection.Add(Google_URI);
        }
        private async void CallAPI()
        {
            try
            {
                NewsCounter = 0;
                Root = await Config.Deserialize(this.URI_Collection[SourceCounter]);
                SetupForm();
            }
            catch(Exception ex) when (ex is System.Net.WebException || ex is IOException || ex is System.NullReferenceException)
            {
                MessageBox.Show("Check Internet Connection ");
            }
           
        }
        private void SetupForm()
        {
            try
            {
                richTextBox1.Text = string.Empty;
                ////////
                ///
                pictureBox1.Load(Root.articles[NewsCounter].urlToImage);
                if(label1 == null)
                {
                    label1.Text = "Loading";
                }
                if (richTextBox1 == null)
                {
                    richTextBox1.Text = "Loading";
                }
                label1.Text = Root.articles[NewsCounter].source.name;
                richTextBox1.AppendText($"News Article: {NewsCounter}{Environment.NewLine}{Environment.NewLine}");
                richTextBox1.AppendText($"Posted at: {Root.articles[NewsCounter].publishedAt}{Environment.NewLine}");
                richTextBox1.AppendText(Root.articles[NewsCounter].title + Environment.NewLine);
                richTextBox1.AppendText(Root.articles[NewsCounter].description);
                richTextBox1.AppendText(Root.articles[NewsCounter].content);

            }
            catch (Exception ex) when (ex is System.Net.WebException || ex is IOException || ex is System.NullReferenceException)
            {
                MessageBox.Show("Check Internet Connection ");
            }
            //CleanUp
            
        }

        public Form1()
        {
            InitializeComponent();
            try
            {
                SetupList();
                CallAPI();
            }
            catch(Exception ex) when (ex is System.Net.WebException || ex is IOException || ex is System.NullReferenceException)
            {
                MessageBox.Show("Check Internet Connection ");
            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch(Exception ex) when (ex is System.Net.WebException || ex is IOException || ex is System.NullReferenceException)
            {
                MessageBox.Show("Check your internet Connections ");
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (NewsCounter < 9)
                {
                    this.NewsCounter++;
                    SetupForm();
                }
            }
            catch(Exception ex) when (ex is System.Net.WebException || ex is IOException || ex is System.NullReferenceException)
            {
                MessageBox.Show("Check internet Connection");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(NewsCounter > 0)
            {
                this.NewsCounter--;
                SetupForm();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(SourceCounter < URI_Collection.Count - 1)
            {
                this.SourceCounter++;
                CallAPI();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(SourceCounter > 0)
            {
                this.SourceCounter--;
                CallAPI();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CallAPI();
            MessageBox.Show("All News are up to Date");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if(label1 == null)
            {
                label1.Text = "Loading";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The NewsApp was created by Seth Michael, this software fetches data from a data source(API) and displays the informaion gathered.", DateTime.Now.ToShortDateString());
        }
    }
}
