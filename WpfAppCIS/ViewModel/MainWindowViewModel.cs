using CinemaModel;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            windowPartView.LoadView(new Welcome());
            InitCommand();
        }
        private CinemaChain _cinemaChain;
        private WindowPartView _windowPartView;
        private DataBase _dataBase;


        public ICommand SearchCinemaCommand { get; private set; }
        public ICommand MapCommand { get; private set; }
        public ICommand BasketCommand { get; private set; }

        private void InitCommand() 
        {
            SearchCinemaCommand = new RelayCommand(LoadListCinemaView);
            MapCommand = new RelayCommand(LoadMapView);
            BasketCommand = new RelayCommand(LoadBasketView);
        }
        private void LoadListCinemaView()
        {
            _windowPartView.LoadView(new ListCinema(new ListCinemaViewModel (_cinemaChain, _windowPartView, _dataBase)));
        }
        private void LoadMapView()
        {
            _windowPartView.LoadView(new Map(new MapViewModel(_cinemaChain, _windowPartView, _dataBase)));
        }
        private void LoadBasketView()
        {
            _windowPartView.LoadView(new BasketInfo(new BasketInfoViewModel(_windowPartView,_dataBase)));
        }
    }
}
