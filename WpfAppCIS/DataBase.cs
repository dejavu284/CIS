using CinemaModel;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using WpfAppCIS.View;

namespace WpfAppCIS.ViewModel
{
    public class DataBase
    {
        public DataBase(string[] args) 
        {
                Data = new(args);
                CinemaChain = Data.GetCinemaChain();
                Basket = new Basket();
        }
        public CinemaChain CinemaChain { get; }
        public WorkingData Data { get; }
        public Basket Basket { get; }
        public void BookingPlace()
        {
            try
            {
                CinemaChain.BookingPlaces(Basket);
                Data.Save(CinemaChain);
                Data.Save(Basket);
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось забранировать места");
            }
        }
        
    }
}

