using CinemaModel;
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
using WpfAppCIS.ViewModel;

namespace WpfAppCIS.View
{
    /// <summary>
    /// Логика взаимодействия для ListCinema.xaml
    /// </summary>
    /// 
    public partial class ListCinema : UserControl
    {
        private List<Cinema> Cinemas;
        private Window _parentWindow;
        public ListCinema(List<Cinema> cinemas)
        {
            InitializeComponent();
            List <ListCinemaModel> cinemaViewModels = ConvertCinemaInCinemaViewModel(cinemas);
            Cinemas = cinemas;
            menulist.ItemsSource = cinemaViewModels;
            _parentWindow = (Window)this.Parent;

        }

        private List<ListCinemaModel> ConvertCinemaInCinemaViewModel(List<Cinema> cinemas)
        {
            List<ListCinemaModel> cinemaViewModels = new List<ListCinemaModel>();
            foreach (var item in cinemas)
            {
                cinemaViewModels.Add(new ListCinemaModel(item));
            }
            return cinemaViewModels;
        }

        private void leftmouse(object sender, MouseButtonEventArgs e)
        {
            int i = menulist.SelectedIndex;
            Cinema cinema = Cinemas[i];
            new CinemaInfo(cinema).Show();
            //_parentWindow.Close();
        }
    }
}
