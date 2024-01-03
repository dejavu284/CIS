using CinemaModel;
using WpfAppCIS.Model;

namespace WpfAppCIS.ViewModel
{
    public class BasketInfoViewModel
    {
        public BasketInfoViewModel(WindowPartView windowPartView,DataBase dataBase) 
        {
            _basket = dataBase.Basket;
        }
        private Basket _basket;


        public Basket Basket { get { return _basket; } }

        public string PriceTag { get { return $"Общая стоимость: {_basket.Price} р."; } }
        public string CountTicketsTag { get { return $"Количество билетов: {_basket.NumberTickets} шт."; } }

    }
}