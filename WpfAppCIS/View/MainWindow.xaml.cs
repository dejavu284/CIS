﻿using CinemaModel;
using Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppCIS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CinemaChain cinemas { get; set; }
        private WorkingData data { get; set; }
        public MainWindow()
        {
            string[] args = { "cinemas.json", "basket.json" };
            InitializeComponent();
            GetData(args);
        }
        private void GetData(string[] args)
        {
            try
            {
                data = new(args);
                cinemas = data.GetCinemaChain();
            }
            catch (Exception ex)
            {
                OutputErrorMessage(ex.Message);
                Close();
            }
        }

        private void OutputErrorMessage(string messageThis)
        {
            MessageBox.Show(messageThis, "Ошибка");
        }

        private void bt_SearchCinema_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bt_SearchFilm_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bt_Map_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bt_Basket_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}