using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace CIS.Models
{
    internal class Ticket
    {
        public Ticket(string name, DateOnly data, TimeOnly time, Place place)
        {
            Name = name;
            Data = data;
            Time = time;
            Place = place;
            Cod = CreateRandomCod();
        }
        public string Name { get; }
        public DateOnly Data { get; }
        public TimeOnly Time { get; }
        public Place Place { get; }
        public int Cod { get; }

        private static int CreateRandomCod() // хорошо или плохо?
        {
            Random random = new();
            int res = random.Next(1000, 9999);
            return res;
        }
    }

}
