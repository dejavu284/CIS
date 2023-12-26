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
        private CinemaChain CinemaChain { get;}
        private ListCollectionView CinemasView { get;}
        public ListCinema(List<Cinema> cinemas)
        {
            InitializeComponent();
            List <CinemaViewModel> cinemaViewModels = ConvertCinemaInCinemaViewModel(cinemas);
            menulist.ItemsSource = cinemaViewModels;
        }

        private List<CinemaViewModel> ConvertCinemaInCinemaViewModel(List<Cinema> cinemas)
        {
            List<CinemaViewModel> cinemaViewModels = new List<CinemaViewModel>();
            foreach (var item in cinemas)
            {
                cinemaViewModels.Add(new CinemaViewModel(item));
            }
            return cinemaViewModels;
        }

        private void leftmouse(object sender, MouseButtonEventArgs e)
        {
            int i = menulist.SelectedIndex;
            Cinema cinema = CinemaChain.Cinemas[i];
            // переход на форму кинофильма(cinema)
        }
    }
}
