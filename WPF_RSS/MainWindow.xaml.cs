using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Net;
using System.Diagnostics;

namespace WPF_RSS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            url.Text = Properties.Settings.Default.url;
        }
        private void OpenWeb(object sender, RoutedEventArgs e)
        {
            dynamic tmp = list.SelectedValue;
            Uri uri = tmp.Links[0].Uri;
            //MessageBox.Show(uri.ToString());
            Process.Start(uri.ToString());
        } 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OnGetFeed();
        }
        private async Task OnGetFeed()
        {
            while(true)
            {
                try
                {
                    using (XmlReader reader = XmlReader.Create(url.Text))
                    {
                        var formatter = new Rss20FeedFormatter();
                        formatter.ReadFrom(reader);
                        this.DataContext = formatter.Feed;
                        this.feedContent.DataContext = formatter.Feed.Items;
                    }
                    
                }
                catch (WebException ex)
                {
                    MessageBox.Show(ex.Message, "Syndication Reader");
                }
                await Task.Delay(Properties.Settings.Default.update);
            }
        }
    }
}
