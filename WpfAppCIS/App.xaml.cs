using System.Configuration;
using System.Data;
using System.Windows;
using WpfAppCIS.Data;

namespace WpfAppCIS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string[] DbConnectionStr { get {return DataLoader.LoadData(); }}
    }

}
