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
        private CinemaChain _cinemas;
        private MainWindow _parentWindow;
        private ListCinemaViewModel _listCinemaViewModel;
        public ListCinema(CinemaChain cinemas, MainWindow parentWindow)
        {
            InitializeComponent();
            _cinemas = cinemas;
            _parentWindow = parentWindow;

            _listCinemaViewModel = new ListCinemaViewModel(cinemas, parentWindow);
            this.DataContext = _listCinemaViewModel;
        }
        private void leftmouse(object sender, MouseButtonEventArgs e)
        {
            int i = menulist.SelectedIndex;
            Cinema cinema = _cinemas.Cinemas[i];
            _parentWindow.ContentControl.Content = new CinemaInfo(cinema,_parentWindow);
        }
    }
}
