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
    /// Логика взаимодействия для CinemaInfo.xaml
    /// </summary>
    public partial class CinemaInfo : UserControl
    {
        private Cinema Cinema;
        private MainWindow _parentWindow;
        public CinemaInfoViewModel CinemaInfoViewModel;
        public CinemaInfo(Cinema cinema, MainWindow parentWindow)
        {
            InitializeComponent();
            Cinema = cinema;
            _parentWindow = parentWindow;
            CinemaInfoViewModel = new CinemaInfoViewModel(cinema);
            this.DataContext = CinemaInfoViewModel;
        }
    }
}
