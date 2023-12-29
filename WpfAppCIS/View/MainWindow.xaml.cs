using CinemaModel;
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
using WpfAppCIS.Model;
using WpfAppCIS.View;
using WpfAppCIS.ViewModel;

namespace WpfAppCIS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CinemaChain CinemaChain;//?
        private MainWindowInitializeDataViewModel InitializeDataViewModel;
        private MainWindowViewModel ViewModel;
        public ContentControl ContentControl { get; set; }//??
        public MainWindow()
        {
            InitializeComponent();
            string[] args = { "cinemas_test.json", "basket.json" };
            
            InitializeDataViewModel = new MainWindowInitializeDataViewModel(args, this);
            CinemaChain = InitializeDataViewModel.CinemaChain;
            WindowPartView windowPartView = new(contentControl_MainWindow);
            ViewModel = new MainWindowViewModel(CinemaChain,windowPartView);
            DataContext = ViewModel;
        }
    }
}