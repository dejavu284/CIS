using CinemaModel;
using GalaSoft.MvvmLight.CommandWpf;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WpfAppCIS.Model;

namespace WpfAppCIS.ViewModel
{
    public class BasketInfoViewModel : INotifyPropertyChanged
    {
        public BasketInfoViewModel(WindowPartView windowPartView,DataBase dataBase)
        {
            _dataBase = dataBase;
            BuyTicketsClick = new RelayCommand(BuyTickets);
            InitBasket();
        }
        private DataBase _dataBase;
        private List<TicketsViewModel> _ticketsViewModels;
        public List<TicketsViewModel> TicketsViewModels 
        {   get { return _ticketsViewModels; }
            set 
            { 
                _ticketsViewModels = value;
                OnPropertyChanged(nameof(TicketsViewModels)); 
            }
        }
        public ICommand  BuyTicketsClick { get; }
        private string _priceTag;
        public string PriceTag 
        { 
            get { return _priceTag; } 
            private set 
            { 
                _priceTag = value;
                OnPropertyChanged("PriceTag");
            }
        }
        private string _countTicketsTag;
        public string CountTicketsTag 
        { 
            get { return _countTicketsTag; }
            set 
            { 
                _countTicketsTag = value;
                OnPropertyChanged(nameof(CountTicketsTag)); 
            }
        }
        private List<TicketsViewModel> GetTicketsViewModels(List<Ticket> tickets)
        {
            List<TicketsViewModel> ticketsViewModels = new List<TicketsViewModel>();

            foreach(Ticket ticket in tickets)
                ticketsViewModels.Add(new TicketsViewModel(ticket,this, _dataBase));    

            return ticketsViewModels;
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void BuyTickets()
        {
            try
            {
                _dataBase.BookingPlace();
                _dataBase.Basket.Clear();
                InitBasket();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }
        public void InitBasket()
        {
            PriceTag = $"Общая стоимость: {_dataBase.Basket.Price} р.";
            CountTicketsTag = $"Количество билетов: {_dataBase.Basket.NumberTickets} шт.";
            TicketsViewModels = GetTicketsViewModels(_dataBase.Basket.Tickets);
        }

    }
}