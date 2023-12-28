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
using WpfAppCIS.View;

namespace WpfAppCIS.ViewModel
{
    internal class ListCinemaViewModel
    {
        public ListCinemaViewModel(CinemaChain cinemas, ContentControl contentControl) 
        {
            CinemaChain = cinemas;
            _contentControl = contentControl;
        }
        public CinemaChain CinemaChain { get; }
        private ContentControl _contentControl;
        private Cinema? _itemSelected = null;
        public Cinema? ItemSelected { 
            get { return _itemSelected; } 
            set{ _itemSelected = value; OnLeftMouseClick(); } }
        
        private void OnLeftMouseClick()
        {
            Cinema? cinema = CinemaChain.Cinemas.FirstOrDefault(item => item.Id == ItemSelected?.Id);
            if (cinema != null)
            {
                _contentControl.Content = new CinemaInfo(cinema, _contentControl);
            }
        }
        
    }
}
