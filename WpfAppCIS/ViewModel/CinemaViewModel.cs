using CinemaModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace WpfAppCIS.ViewModel
{
    class CinemaViewModel
    {
        public CinemaViewModel(Cinema cinema) 
        {
            Name = cinema.Name;
            Address = $"{cinema.Address.Street} {cinema.Address.NumberHouse}";
            Rating = cinema.Rating;
            Id = cinema.Id;
        }
        public string Name { get; }
        public string Address { get; }
        public float Rating { get; }
        public int Id { get; }
    }
}
