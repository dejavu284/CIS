using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CIS.Views;

namespace CIS.Models
{
    internal class FilmScreening
    {
        public List<FilmScreening> filmScreenings = new();
        public FilmScreening() { }
        public FilmScreening(string name, DateOnly data, TimeOnly time, int countTiket, int price)
        {
            Name = name;
            Date = data;
            Time = time;
            CountTicket = countTiket;
            Price = price;
        }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("data")]
        public DateOnly Date { get; set; }
        [JsonPropertyName("time")]
        public TimeOnly Time { get; set; }
        [JsonPropertyName("countTiket")]
        public int CountTicket { get; set; }
        [JsonPropertyName("price")]
        public int Price { get; set; }


        public static FilmScreening ChooseFilmScreeningsInCertainTime(List<FilmScreening> filmScreeningsInCertainDate)
        {
            FilmScreening filmScreeningsInCertainTime;
            bool flagChooseTime;
            do
            {
                OutputTimeFilmScreening(filmScreeningsInCertainDate);
                filmScreeningsInCertainTime = ChoiseFilmScreeningByTime(filmScreeningsInCertainDate);
                if (!IsPlacesNotEmpty(filmScreeningsInCertainTime))
                {
                    Console.WriteLine("На выбранное время мест нет.\n");
                    throw new InvalidExpressionException();// сделать свой эксепшен
                }
                flagChooseTime = !ConsoleMessages.PoolYesOrNo("Оставить выбранное время");
            } while (flagChooseTime);
            return filmScreeningsInCertainTime;
        }
        public static bool IsPlacesNotEmpty(FilmScreening filmScreening)
        {
            return filmScreening.CountTicket != 0;
        }
        public static void OutputTimeFilmScreening(List<FilmScreening> filmscreenings)
        {
            Console.WriteLine("Время показа фильма:");
            for (int i = 0; i < filmscreenings.Count; i++)
            {
                Console.WriteLine("\n{0}. {1}. Цена: {2} руб.", i + 1, filmscreenings[i].Time, filmscreenings[i].Price);
            }
        }
        public static FilmScreening ChoiseFilmScreeningByTime(List<FilmScreening> filmScreeningInCertainDay)//??
        {
            return ConsoleMessages.ChooseEl(filmScreeningInCertainDay);
        }
        public static void OutputCountPlace(FilmScreening filmScreening)
        {
            Console.WriteLine("Количесво оставшихся мест на сеанс: {0}\n", filmScreening.CountTicket);
        }
    }
}
