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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfAppCIS.View
{
    /// <summary>
    /// Логика взаимодействия для Loading.xaml
    /// </summary>
    public partial class Loading : Window
    {
        public Loading()
        {
            InitializeComponent();
            string url = $"{Environment.CurrentDirectory}" + "\\Video\\AppInfo.mp4";
            mediaEl.Source = new Uri(url);
            mTimer.Interval = TimeSpan.FromMilliseconds(1000);
            mTimer.Tick += new EventHandler(mTimer_Tick!);
            mTimer.Start();

        }
        private DispatcherTimer mTimer = new DispatcherTimer();
        void mTimer_Tick(object sender, EventArgs e)
        {
            if (mediaEl.NaturalDuration.HasTimeSpan)
            {
                double x = mediaEl.Position.TotalSeconds / mediaEl.NaturalDuration.TimeSpan.TotalSeconds;
                Slider.Value = x * 100;
            }
            if(Slider.Value == 100)
            {
                SkipLoading();
                mTimer.Stop();
            }
        }
        private void Button_Click_Skip(object sender, RoutedEventArgs e)
        {
            SkipLoading();
        }
        private void SkipLoading()
        {
            try
            {
                MainWindow mainWindow = new MainWindow(App.DbConnectionStr);
                mainWindow.Show();
            }
            catch(Exception ex) { MessageBox.Show(ex.Message, "Ошибка"); }
            this.Close();
            
        }
    }
}
