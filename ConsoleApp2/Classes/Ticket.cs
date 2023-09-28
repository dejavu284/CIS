using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Classes
{
    internal class Ticket
    {
        public Ticket(string name, DateOnly data, TimeOnly time, int price)
        {
            this.Name = name;
            this.Data = data;
            this.Time = time;
            this.Price = price;
            cod = "";
        }
        public string Name { get; set; }
        public DateOnly Data { get; set; }
        public TimeOnly Time { get; set; }
        public int Price { get; set; }
        public string cod { get; set; }
    }
}
