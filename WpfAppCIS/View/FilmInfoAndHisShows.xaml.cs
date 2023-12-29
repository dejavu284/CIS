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
using WpfAppCIS.Model;
using WpfAppCIS.ViewModel;

namespace WpfAppCIS.View
{
    /// <summary>
    /// Логика взаимодействия для FilmInfoAndHisShows.xaml
    /// </summary>
    public partial class FilmInfoAndHisShows : UserControl
    {
        public FilmInfoAndHisShows(FilmInfoAndHisShowsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
