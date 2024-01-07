using CinemaModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using WpfAppCIS.Data;
using WpfAppCIS.Model;
using WpfAppCIS.View;

namespace WpfAppCIS.ViewModel
{
    public class CinemaInfoViewModel
    {
        private Cinema _cinema;
        public CinemaInfoViewModel(Cinema cinema, WindowPartView windowPartView, DataBase dataBase) 
        { 
            _cinema = cinema; 
            _windowPartView = windowPartView;
            _dataBase = dataBase;
        }
        private Film? _filmSelected;
        private WindowPartView _windowPartView;
        private DataBase _dataBase;
        public Film? FilmSelected 
        { 
            get { return _filmSelected; } 
            set 
            {
                _filmSelected = value; 
                LoadShowsView(); 
            } 
        }
        private void LoadShowsView()
        {
            if (FilmSelected != null)
                _windowPartView.LoadView(
                    new FilmInfoAndHisShows(
                        new FilmInfoAndHisShowsViewModel(
                            FilmSelected,
                            _cinema.Schedule.FindByName(FilmSelected.Name),
                            _cinema.Halls
                            ,_cinema.Id,
                            _windowPartView,
                            _dataBase
                            )
                        )
                    );
        }
        public List<Film> Films
        {
            get { return _cinema.Poster.Films; }
            
        }
        public string Name
        {
            get { return _cinema.Name; }
        }

        public string Address
        {
            get { return $"Адресс: {_cinema.Address.Street} {_cinema.Address.NumberHouse}"; }
        }
        public string Rating
        {
            get { return $"Рейтинг: {_cinema.Rating}";}
        }

        public string CountHalls
        {
            get { return $"Кол-во залов: {_cinema.Halls.Count}";}
        }
    }
}

