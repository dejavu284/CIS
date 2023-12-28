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

namespace WpfAppCIS.ViewModel
{
    public class CinemaInfoViewModel : INotifyPropertyChanged
    {
        private Cinema _cinema;
        public CinemaInfoViewModel(Cinema cinema) { _cinema = cinema; }

        public List<Film> Films
        {
            get { return _cinema.Poster.Films; }
            //set { _cinema.Address = value; OnPropertyChanged("Address"); }
        }



        public string Name
        {
            get { return _cinema.Name; }
            //set { _cinema.Name = value; OnPropertyChanged("Name"); }
        }

        public string Address
        {
            get { return $"Адресс: {_cinema.Address.Street} {_cinema.Address.NumberHouse}"; }
            //set { _cinema.Address = value; OnPropertyChanged("Address"); }
        }
        public string Rating
        {
            get { return $"Рейтинг: {_cinema.Rating.ToString()}";}
            //set { _cinema.Rating = value; OnPropertyChanged("Rating"); }
        }

        /*public Schedule Schedule
        {
            get { return _cinema.Schedule; }
            //set { _cinema.Schedule = value; OnPropertyChanged("Schedule"); }
        }*/

        public string CountHalls
        {
            get { return $"Кол-во залов: {_cinema.Halls.Count}";}
            //set { _cinema.Halls = value; OnPropertyChanged("Halls"); }
        }

        /*public Poster Poster
        {
            get { return _cinema.Poster; }
            //set { _cinema.Poster = value; OnPropertyChanged("Poster"); }
        }*/

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {

        }
    }
}

