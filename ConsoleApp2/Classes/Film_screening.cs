using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Classes
{
    internal class Film_screening
    {
        public Film_screening(/*Film film,*/ DateOnly data, TimeOnly time, int countPlace, int price)
        {
            /*this.film = film;*/
            this.data = data;
            this.time = time;
            this.countPlase = countPlace;
            this.price = price;
        }
       /* public Film film;*/
        public DateOnly data { get; set; }
        public TimeOnly time { get; set; }
        public int countPlase { get; set; }
        public int price { get; set; }
    }
}
