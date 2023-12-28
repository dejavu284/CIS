using CinemaModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using WpfAppCIS.View;

namespace WpfAppCIS.ViewModel
{
    internal class ListCinemaViewModel : INotifyPropertyChanged
    {
        public ListCinemaViewModel(CinemaChain cinemas, MainWindow mainWindow) 
        {
            CinemasInfo = ConvertCinemaInCinemaViewModel(cinemas.Cinemas);
            Cinemas = cinemas;
            _parentWindow = mainWindow;
        }
        public List<ListCinemaModel> CinemasInfo { get; }
        public CinemaChain Cinemas { get; }
        private MainWindow _parentWindow;
        public ListCinemaModel? ItemSelected { 
            get { return _itemSelected; } 
            set{ _itemSelected = value; OnPropertyChanged("ItemSelected"); OnLeftMouseClick(); } }
        private ListCinemaModel? _itemSelected = null;
        private List<ListCinemaModel> ConvertCinemaInCinemaViewModel(List<Cinema> cinemas)
        {
            List<ListCinemaModel> cinemaViewModels = new List<ListCinemaModel>();
            foreach (var item in cinemas)
            {
                cinemaViewModels.Add(new ListCinemaModel(item));
            }
            return cinemaViewModels;
        }
        private void OnLeftMouseClick()
        {
            Cinema? cinema = Cinemas.Cinemas.FirstOrDefault(item => item.Id == ItemSelected?.Id);
            if (cinema != null)
            {
                _parentWindow.ContentControl.Content = new CinemaInfo(cinema, _parentWindow);
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {

        }
    }
}
