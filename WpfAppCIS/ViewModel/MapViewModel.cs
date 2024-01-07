using CinemaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppCIS.Model;

namespace WpfAppCIS.ViewModel
{
    public class MapViewModel
    {
        public MapViewModel(CinemaChain cinemaChain, WindowPartView windowPartView, DataBase dataBase) 
        { 
            _cinemaChain = cinemaChain;
            _windowPartView = windowPartView;
            _dataBase = dataBase;
        }
        private CinemaChain _cinemaChain;
        private WindowPartView _windowPartView;
        private DataBase _dataBase;

    }
}
