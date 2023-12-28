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
    public partial class ListCinema : UserControl
    {
        private ListCinemaViewModel _listCinemaViewModel;
        public ListCinema(CinemaChain cinemas, ContentControl contentControl)
        {
            InitializeComponent();

            _listCinemaViewModel = new ListCinemaViewModel(cinemas, contentControl);
            DataContext = _listCinemaViewModel;
        }
    }
}
