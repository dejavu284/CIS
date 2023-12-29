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
    /// Логика взаимодействия для ShowInfo.xaml
    /// </summary>
    public partial class ShowInfo : UserControl
    {
        public ShowInfo(ShowInfoViewModel showInfoViewModel)
        {
            InitializeComponent();
            DataContext = showInfoViewModel;
        }
    }
}
