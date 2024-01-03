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
        public MainWindow()
        {
            InitializeComponent();
            string[] args = { "cinemas_test.json", "basket.json" };
            
            try 
            {
                DataBase dataBase = new DataBase(args);
                CinemaChain cinemaChain = dataBase.CinemaChain;
                WindowPartView windowPartView = new(contentControl_MainWindow);
                DataContext = new MainWindowViewModel(cinemaChain, windowPartView, dataBase);
            }
            catch (Exception ex)
            {
                OutputErrorMessage(ex.Message);
                this.Close();
            }
        }
        private void OutputErrorMessage(string messageThis)
        {
            MessageBox.Show(messageThis, "Ошибка");
        }
    }

}