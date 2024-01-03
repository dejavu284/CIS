using CinemaModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppCIS.Model;

namespace WpfAppCIS.ViewModel
{
    public class SeatViewModel : INotifyPropertyChanged
    {
        public SeatViewModel(Seat seat, ShowInfoViewModel parent)
        {
            _seat = seat;
            Parent = parent;
        }
        private Seat _seat;

        public static readonly double _widthCell = 30;
        public static readonly double _heightCell = 30;
        public static readonly double _marginCell = 10;
        private ShowInfoViewModel Parent { get; }
        public static double WidthCell { get { return _widthCell; } }
        public static double HeightCell { get { return _heightCell; } }
        public static double MarginCell { get { return _marginCell; } }

        private bool _checked = false;
        public bool Enabled { get { return _seat.Free; } }
        public int NumberRow { get {  return _seat.NumberRow; } }
        public int NumberColum { get {  return _seat.Number; } }
        public bool Checked
        {
            get { return _checked; }
            set
            {
                _checked = value;
                OnPropertyChanged("Checked");
                Parent.UpdateCountCheckedPlaces();
            }
        }
        public string PriseTag
        {
            get
            {
                if (_seat.Prise == null)
                    return "место куплено";
                else
                    return $"{_seat.Prise}р"; 
            } 
        }
        public int? Prise
        {
            get
            {
                return _seat.Prise;
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
