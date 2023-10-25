using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace CIS.Models
{
    internal class Ticket
    {
        public Ticket(string name, DateOnly data, TimeOnly time, int price)
        {
            Name = name;
            Data = data;
            Time = time;
            Price = price;
            Cod = CreateRandomCod();
        }
        public string Name { get; }
        public DateOnly Data { get; }
        public TimeOnly Time { get; }
        public int Price { get; }
        public int Cod { get; }

        private static int CreateRandomCod()
        {
            Random random = new();
            int res = random.Next(1000, 9999);
            return res;
        }
    }

}
