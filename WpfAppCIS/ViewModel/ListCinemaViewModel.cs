using CinemaModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using WpfAppCIS.Model;
using WpfAppCIS.View;

namespace WpfAppCIS.ViewModel
{
    internal class ListCinemaViewModel
    {
        public ListCinemaViewModel(CinemaChain cinemas, WindowPartView windowPartView) 
        {
            CinemaChain = cinemas;
            _windowPartView = windowPartView;
        }
        public CinemaChain CinemaChain { get; }
        private WindowPartView _windowPartView;
        private Cinema? _itemSelected = null;
        public Cinema? ItemSelected 
        { 
            get { return _itemSelected; } 
            set
            { 
                _itemSelected = value; 
                LoadCinemaInfoView(); 
            } 
        }
        
        private void LoadCinemaInfoView()
        {
            Cinema? cinema = ItemSelected;
            if (cinema != null)
                _windowPartView.LoadView(new CinemaInfo(cinema, _windowPartView));
        }
        
    }
}
