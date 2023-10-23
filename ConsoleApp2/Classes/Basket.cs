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
        public static int NumberTickets { get; private set; } = 0;
        public static int Price { get; private set; } = 0;
        public static void AddTicket(FilmScreening filmScreening)
        {
            Tickets.Add(new Ticket(filmScreening.Name, filmScreening.Date, filmScreening.Time, filmScreening.Price));
            Price += filmScreening.Price;
            NumberTickets++;
            MessageTicketPurchased();
        }
        public static void MessageTicketPurchased()
        {
            Console.WriteLine("Билет куплен");
        }
        public static Basket BuyTickets(Dictionary<string, List<FilmScreening>> filmScreenings, FilmsPoster filmsPoster)
        {
            Basket basket = new();
            bool flagBuyTickets = true;
            while (flagBuyTickets)
            {
                // Выбор фильма
                List<FilmScreening> filmScreeningsInOneFilm = FilmScreenings.ChooseFilmScreeingInCertainFilm(filmScreenings, filmsPoster);
                // Выбор даты
                List<FilmScreening> filmScreeningsInCertainDate = FilmScreenings.ChooseFilmScreeingInCertainDate(filmScreeningsInOneFilm);
                // Выбор времени
                FilmScreening filmScreeningInCertainTime = FilmScreening.ChooseFilmScreeningsInCertainTime(filmScreeningsInCertainDate);

                if (FilmScreening.IsPlacesNotEmpty(filmScreeningInCertainTime))
                {
                    FilmScreening.OutputCountPlace(filmScreeningInCertainTime);
                    if (FilmScreening.PoolYesOrNo("Купить билет"))
                    {
                        AddTicket(filmScreeningInCertainTime);
                    }
                    ConsoleMessages.MessageCheck();
                    flagBuyTickets = !FilmScreening.PoolYesOrNo("Закончить");
                }
            }
            return basket;
        }
        public static void Save(string path)
        {
            var options = new JsonSerializerOptions { WriteIndented = true }; // опция для развертывания json файла
            string jsonString = JsonSerializer.Serialize(Tickets, options); // список в строку
            jsonString = Regex.Replace(jsonString, @"\\u([0-9A-Fa-f]{4})", m => "" + (char)Convert.ToInt32(m.Groups[1].Value, 16)); // меняем кодировку 
            File.WriteAllText(path, jsonString); // запись в файл .json
        }
    }
}
