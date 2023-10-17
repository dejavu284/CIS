using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Classes
{
    internal class FilmScreening
    {
        public FilmScreening(string name, DateOnly data, TimeOnly time, int countTiket, int price)
        {
            this.name = name;
            this.data = data;
            this.time = time;
            this.countTiket = countTiket;
            this.price = price;
        }
        public string name { get; set; }
        public DateOnly data { get; set; }
        public TimeOnly time { get; set; }
        public int countTiket { get; set; }
        public int price { get; set; }
    }
}
