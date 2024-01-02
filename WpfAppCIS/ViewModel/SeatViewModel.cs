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
    public class SeatViewModel
    {
        public SeatViewModel(Seat seat, ShowInfoViewModel parent)
        {
            _seat = seat;
            Parent = parent;
            Number = _seat.Number;
        }
        private Seat _seat;

        public static readonly double _widthCell = 30;
        public static readonly double _heightCell = 30;
        public static readonly double _marginCell = 10;
        private ShowInfoViewModel Parent { get; }
        public static double WidthCell { get { return _widthCell; } }
        public static double HeightCell { get { return _heightCell; } }
        public static double MarginCell { get { return _marginCell; } }
        public int Number { get; }

        private bool _checked = false;
        public bool Enabled { get { return _seat.Free; } }
        public bool Checked
        {
            get { return _checked; }
            set
            {
                _checked = value;
                Parent.UpdateCountCheckedPlaces();
            }
        }
        public string Prise
        {
            get
            {
                if (_seat.Prise == null)
                    return "место куплено";
                else
                    return $"{_seat.Prise}р"; 
            } 
        }
       

    }
}
