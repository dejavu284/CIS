using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Classes
{
    internal class Film_screening
    {
        public Film_screening(/*Film film,*/ DateOnly data, TimeOnly time, int countTiket, int price)
        {
            /*this.film = film;*/
            this.data = data;
            this.time = time;
            this.countTiket = countTiket;
            this.price = price;
        }
       /* public Film film;*/
        public DateOnly data { get; set; }
        public TimeOnly time { get; set; }
        public int countTiket { get; set; }
        public int price { get; set; }
    }
}
