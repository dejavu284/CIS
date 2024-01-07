using CinemaModel;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfAppCIS.Data;

namespace WpfAppCIS.ViewModel
{
    public class TicketsViewModel
    {
        public TicketsViewModel(Ticket ticket, BasketInfoViewModel parent, DataBase dataBase)
        {
            _ticket = ticket;
            _dataBase = dataBase;
            _parent = parent;
            DeletClick = new RelayCommand(DeletTicketFromBasket);
        }
        private Ticket _ticket;
        private DataBase _dataBase;
        private BasketInfoViewModel _parent;
        public string NameCinema { get { return _ticket.NameCinema; } }
        public string NameFilm { get { return _ticket.NameFilm; } }
        public string DateShow 
        {
            get 
            {
                string dayTag = _ticket.Date.Day.ToString("00");
                string mountTag = _ticket.Date.Month.ToString("00");
                string yearTag = _ticket.Date.Year.ToString("0000");
                string dateTag = $"Дата показа: {dayTag}.{mountTag}.{yearTag}";
                return dateTag;
            } 
        }
        public string TimeShow
        {
            get
            {
                string minuteTag = _ticket.Time.Minute.ToString("00");
                string hourTag = _ticket.Time.Hour.ToString("00");
                string TimeTag = $"Время показа: {hourTag}:{minuteTag}";
                return TimeTag;
            }
        }
        public string NumberPlace { get { return $"Номер места: {_ticket.Place.Number}"; } }
        public string NumberRow { get { return $"Номер ряда: {_ticket.Place.Row + 1}"; } }
        public string Price { get { return $"Цена: {_ticket.Place.Price}р."; } }

        public ICommand DeletClick { get; }
        private void DeletTicketFromBasket()
        {
            _dataBase.Basket.RemoveTicket(_ticket);
            _parent.InitBasket();
        }

    }
}
