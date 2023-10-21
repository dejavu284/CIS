using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp2.Classes
{
    internal class Basket
    {
        public Basket() {}
        public static List<Ticket> Tickets { get; private set; } = new List<Ticket>();
        private static int _numberTickets = 0;
        public static int Price { get; private set; } = 0;
        public void AddTicket(FilmScreening filmScreening)
        {
            Tickets.Add(new Ticket(filmScreening.name, filmScreening.data, filmScreening.time, filmScreening.price));
            Price += filmScreening.price;
            _numberTickets++;
            MessageTicketPurchased();
        }
        public static void MessageTicketPurchased()
        {
            Console.WriteLine("Билет куплен");
        }
        public void MessageCheck()
        {
            Console.WriteLine("\n---------------------------");
            Console.WriteLine("Чек:");
            for (int i = 0; i < _numberTickets; i++)
            {
                Console.WriteLine("\nБилет №{0}", i + 1);
                Tickets[i].MessangInfo();
            }
            Console.WriteLine("\nИтоговая стоимость составила: {0}", Price);
            Console.WriteLine("---------------------------");
        }
        public void Save(string path)
        {
            var options = new JsonSerializerOptions { WriteIndented = true }; // опция для развертывания json файла
            string jsonString = JsonSerializer.Serialize(this, options); // список в строку
            jsonString = Regex.Replace(jsonString, @"\\u([0-9A-Fa-f]{4})", m => "" + (char)Convert.ToInt32(m.Groups[1].Value, 16)); // меняем кодировку 
            File.WriteAllText(path, jsonString); // запись в файл .json
        }
    }
}
