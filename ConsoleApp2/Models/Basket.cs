using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using System.Threading.Tasks;
using CIS.Views;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CIS.Models
{
    internal class Basket 
    {
        public Basket() { }
        public static List<Ticket> Tickets { get; private set; } = new List<Ticket>();
        public static int NumberTickets { get; private set; } = 0;
        public static int Price { get; private set; } = 0;
        public void AddTicket(FilmScreening filmScreening)
        {
            Tickets.Add(new Ticket(filmScreening.Name, filmScreening.Date, filmScreening.Time, filmScreening.Price));
            Price += filmScreening.Price;
            NumberTickets++;
        }
        public void Save(string path) // Не получается сериализовать весь объект,
                                      // чтобы и билеты и количество билетов и
                                      // общая сумма были сериализованы
                                      // 
        {
            var options = new JsonSerializerOptions // опции для развертывания json файла и сериализации в Кириллицу
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(Tickets, options); // список в строку 
            File.WriteAllText(path, jsonString); // запись в файл .json
        }
    }
}
