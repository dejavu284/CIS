using CinemaModel;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using WpfAppCIS.View;

namespace WpfAppCIS.ViewModel
{
    internal class MainWindowInitializeDataViewModel
    {
        public MainWindowInitializeDataViewModel(string[] args,MainWindow mainWindow) 
        {
            GetData(args, mainWindow);
        }
        public CinemaChain CinemaChain;
        public WorkingData Data;
        
        private void GetData(string[] args, MainWindow mainWindow)
        {
            try
            {
                Data = new(args);
                CinemaChain = Data.GetCinemaChain();
            }
            catch (Exception ex)
            {
                OutputErrorMessage(ex.Message);
                mainWindow.Close();
            }
        }

        private void OutputErrorMessage(string messageThis)
        {
            MessageBox.Show(messageThis, "Ошибка");
        }
    }
}

