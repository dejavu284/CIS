using CinemaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppCIS.Model;
using WpfAppCIS.View;

namespace WpfAppCIS.ViewModel
{
    public class FilmInfoAndHisShowsViewModel
    {
        public FilmInfoAndHisShowsViewModel(Film film,Schedule schedule,List<Hall> halls,int idCinema, WindowPartView windowPartView, DataBase dataBase)
        {
            _film = film;
            _schedule = schedule;
            _windowPartView = windowPartView;
            _halls = halls;
            _idCinema = idCinema;
            _dataBase = dataBase;
        }
        private Show? _showSelected;
        private readonly Film _film;
        private Schedule _schedule;
        private WindowPartView _windowPartView;
        private List<Hall> _halls;
        private int _idCinema;
        private DataBase _dataBase;
        public Show? ShowSelected 
        { 
            get { return _showSelected; }
            set 
            { 
                _showSelected = value;
                LoadSeatingView();
            } 
        }

        private void LoadSeatingView()
        {

            if (ShowSelected != null) 
            {
                Hall? hallSelected = _halls.Where(x => x.Id == ShowSelected.Seating.IdHall).FirstOrDefault();
                if (hallSelected != null)
                    _windowPartView.LoadView(new ShowInfo(new ShowInfoViewModel(ShowSelected, hallSelected,_idCinema, _windowPartView,_dataBase)));
            }
             
            
        }

        

        public string FilmName 
        {
            get { return _film.Name; }
        }
        public string FilmDescription 
        { 
            get { return $"{_film.Description}"; } 
        }
        public string FilmGenre
        {
            get { return $"{_film.Genre}";}
        }
        public string YearFilmRelease
        {
            get { return $"{_film.Year} г.";} 
        }
        public List<Show> Shows
        {
            get { return _schedule.Shows; }
        }
    }
}
