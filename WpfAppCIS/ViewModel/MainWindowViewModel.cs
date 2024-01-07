using CinemaModel;
using GalaSoft.MvvmLight.Command;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfAppCIS.Data;
using WpfAppCIS.Model;
using WpfAppCIS.View;

namespace WpfAppCIS.ViewModel
{
    internal class MainWindowViewModel
    {
        
        public MainWindowViewModel(CinemaChain cinemaChain, WindowPartView windowPartView,DataBase dataBase) 
        {
            _cinemaChain = cinemaChain;
            _windowPartView = windowPartView;
            _dataBase = dataBase;
            
            windowPartView.LoadView(new AppInfo());
            InitCommand();
        }
        private CinemaChain _cinemaChain;
        private WindowPartView _windowPartView;
        private DataBase _dataBase;
        


        public ICommand SearchCinemaCommand { get; private set; }
        public ICommand BasketCommand { get; private set; }
        public ICommand AppInfoCommand { get; private set; }

        private void InitCommand() 
        {
            SearchCinemaCommand = new RelayCommand(LoadListCinemaView);
            BasketCommand = new RelayCommand(LoadBasketView);
            AppInfoCommand = new RelayCommand(LoadAppInfoView);
        }
        private void LoadListCinemaView()
        {
            _windowPartView.LoadView(new ListCinema(new ListCinemaViewModel (_cinemaChain, _windowPartView, _dataBase)));
        }
        private void LoadBasketView()
        {
            _windowPartView.LoadView(new BasketInfo(new BasketInfoViewModel(_windowPartView,_dataBase)));
        }
        private void LoadAppInfoView()
        {
            _windowPartView.LoadView(new AppInfo());
        }
    }
}
