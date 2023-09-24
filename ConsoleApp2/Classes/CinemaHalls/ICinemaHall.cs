using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp2.Classes.Screen;

namespace ConsoleApp2.Classes.CinemaHall
{
    internal interface ICinemaHall
    {
        //public Screen screen;
        public Place[,] places { get; }
    }
}
