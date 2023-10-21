using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Classes
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
        public string Name { get;}
        public DateOnly Data { get;}
        public TimeOnly Time { get;}
        public int Price { get;}
        public int Cod { get;}

        private int CreateRandomCod()
        {
            Random random = new ();
            int res = random.Next(1000, 9999);
            return res;
        }
        public void MessangInfoOfTicket()
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("Информация о билете:");
            Console.WriteLine("Название фильма: {0}", Name);
            Console.WriteLine("Дата сеанса: {0}", Data);
            Console.WriteLine("Время сеанса: {0}", Time);
            Console.WriteLine("Цена билета: {0}", Price);
            Console.WriteLine("---------------------------\n");
        }
    }
    
}
