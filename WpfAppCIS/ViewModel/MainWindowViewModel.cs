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
        public MainWindowViewModel(CinemaChain cinemaChain, WindowPartView windowPartView) 
        {
            _cinemaChain = cinemaChain;
            _windowPartView = windowPartView;

            InitCommand();
        }
        private CinemaChain _cinemaChain;
        private WindowPartView _windowPartView;


        public ICommand SearchCinemaCommand { get; private set; }
        public ICommand SearchFilmCommand { get; private set; }
        public ICommand MapCommand { get; private set; }
        public ICommand BasketCommand { get; private set; }

        private void InitCommand() 
        {
            SearchCinemaCommand = new RelayCommand(LoadListCinemaView);
            SearchFilmCommand = new RelayCommand(LoadListFilmView);
            MapCommand = new RelayCommand(LoadMapView);
            BasketCommand = new RelayCommand(LoadBasketView);
        }
        private void LoadListCinemaView()
        {
            _windowPartView.LoadView(new ListCinema(_cinemaChain, _windowPartView));
        }

        private void LoadListFilmView()
        {

        }

        private void LoadMapView()
        {

        }

        private void LoadBasketView()
        {

        }
    }
}
