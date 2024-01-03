using CinemaModel;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfAppCIS.Model;
using WpfAppCIS.View;

namespace WpfAppCIS.ViewModel
{
    public class ShowInfoViewModel : INotifyPropertyChanged
    {
        public ShowInfoViewModel(Show show, Hall hall,int idCinema, WindowPartView windowPartView, DataBase dataBase)
        {
            _seats = GetSeats(show,hall);
            _hall = hall;
            _show = show;
            _idCinema = idCinema;
            _windowPartView = windowPartView;
            ContextButtonAddBasket = GenerateTextButon(0);
            CountCheckedPlaces = 0;
            AddTicketsInBasket = new RelayCommand(AddTicketsInBasketClick);
            _dataBase = dataBase;
        }
        private WindowPartView _windowPartView;
        public List<SeatViewModel> SeatsViewModel 
        { 
            get { return _seats; }
        }
        private List<SeatViewModel> _seats = new List<SeatViewModel>();
        private Hall _hall;
        private Show _show;
        private int _idCinema;
        private DataBase _dataBase;

        public double Width { get { return _hall.CountCols * (SeatViewModel.WidthCell + 2 * SeatViewModel.MarginCell); }}
        public double Height { get {return _hall.CountRows * (SeatViewModel.HeightCell + 2 * SeatViewModel.MarginCell); }}
        public double CountColumns { get { return _hall.CountCols; } }
        public double CountRows { get { return _hall.CountRows; } }
        public int CountCheckedPlaces { get; private set; }
        public ICommand AddTicketsInBasket { get; }

        public string _contextButtonAddBasket = "";
        public string _contextErrorMessage = "";
        public bool EnabledButtonAddBasket 
        {
            get 
            {
                return CheckedEnabledButtonAddBasket(CountCheckedPlaces);
            }
            private set
            {
                OnPropertyChanged("EnabledButtonAddBasket");
            }

        }
        public string ContextButtonAddBasket
        {
            get { return _contextButtonAddBasket; }
            private set
            {
                _contextButtonAddBasket = value;
                OnPropertyChanged("ContextButtonAddBasket");
            }
        }
        public string ContextErrorMessage
        {
            get { return _contextErrorMessage; }
            private set
            {
                _contextErrorMessage = value;
                OnPropertyChanged("ContextErrorMessage");
            }
        }
        private bool CheckedEnabledButtonAddBasket(int countCheckedPlaces)
        {
            if (countCheckedPlaces == 0)
                return false;
            else
                return true;
        }
        public void UpdateCountCheckedPlaces()
        {
            int count = 0;
            foreach (var seat in SeatsViewModel)
            {
                if (seat.Checked)
                {
                    count++;
                }
            }
            CountCheckedPlaces = count;
            ContextButtonAddBasket = GenerateTextButon(count);
            EnabledButtonAddBasket = CheckedEnabledButtonAddBasket(count);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        private string GenerateTextButon(int countCheckedPlaces)
        {
            string textButton = "Добавить в корзину";
            string nameTickets;
            if (countCheckedPlaces == 0)
                return "Выберете место";
            else if (countCheckedPlaces == 1)
                nameTickets = "билет";
            else if ((countCheckedPlaces % 10) >= 2 && (countCheckedPlaces % 10) <= 4 && (countCheckedPlaces / 10) != 1)
                nameTickets = "билетa";
            else
                nameTickets = "билетов";

            return $"{textButton} {countCheckedPlaces} {nameTickets}";
        }
        private List<SeatViewModel> GetSeats(Show show, Hall hall)
        {
            List<SeatViewModel> seats = new List<SeatViewModel>();

            for (int i = 0; i < show.Seating.CountRow; i++)
            {
                for (int j = 0; j < show.Seating.Places[i].Length; j++)
                {
                    int numberSeat = hall.Layout[i][j];
                    int numberRowSeat = i;

                    int? priseSeat;
                    bool freeSeat;
                    if (show.Seating.CheckAvailabilityPlace(i + 1, j + 1))
                    {
                        freeSeat = true;
                        priseSeat = show.Seating.Places[i][j];
                    }
                    else
                    {
                        freeSeat = false;
                        priseSeat = null;
                    }
                    seats.Add(new SeatViewModel(new Seat(numberSeat,priseSeat,numberRowSeat,freeSeat),this));
                }
            }
            return seats;
        }
        private List<Ticket> GeneratedGroupTikets()
        {
            List<Ticket> groupTikets = new List<Ticket>();
            for (int i = 0; i < _seats.Count; i++)
            {
                if (_seats[i].Checked && _seats[i].Prise != null)
                {
                    _seats[i].Checked = false;
                    Ticket ticket = new Ticket
                        (
                        _idCinema,
                        _show,
                        new Place(_seats[i].NumberRow, _seats[i].NumberColum, (int)_seats[i].Prise!)
                        ) ;
                    groupTikets.Add(ticket);
                }
            }
            return groupTikets;
        }
        private void AddTicketsInBasketClick()
        {
            List<Ticket> groupTikets = GeneratedGroupTikets();
            try
            {
                foreach (Ticket ticket in groupTikets)
                {
                    _dataBase.Basket.AddTicket(ticket);
                    _dataBase.BookingPlace();
                }
                ContextErrorMessage = "Билеты добавленные в корзину <З";
            }
            catch (Exception ex)
            {
                //вывод предупреждения
                ContextErrorMessage = ex.Message;
            }

        }
    }
}
