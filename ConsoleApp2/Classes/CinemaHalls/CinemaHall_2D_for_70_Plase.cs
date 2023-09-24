using ConsoleApp2.Classes.Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp2.Classes.CinemaHall
{
    class CinemaHall_2D_for_70_Plase : ICinemaHall
    {
        CinemaHall_2D_for_70_Plase()
        {
            places = new Place[,] { 
                { },
                { },
                { },
                { },
                { },
                { },
                { },
            };
            screen = new Screen_2D();
        }
        public Place[,] places { get; set; }
        public IScreen screen { get;}
        string IScreen.type => throw new NotImplementedException();
        double IScreen.coefficient => throw new NotImplementedException();

    }
}
