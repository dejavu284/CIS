using ConsoleApp2.Classes;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
//using static System.Net.WebRequestMethods;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = $"{Environment.CurrentDirectory}";

            string film_json_path = currentDirectory + "\\Data\\" + args[0];
            string film_screening_json_path = currentDirectory + "\\Data\\" + args[1];
            string basket_json_path = currentDirectory + "\\Data\\" + args[2];

            // Десериализация film.json в список films
            string str_film_json = File.ReadAllText(film_json_path);
            List<Film> films = JsonSerializer.Deserialize<List<Film>>(str_film_json)!;

            // Десериализация film_screening.json в словарь film_screening
            string str_film_screening_json = File.ReadAllText(film_screening_json_path);
            Dictionary<string, List<Film_screening>> film_screening = JsonSerializer.Deserialize<Dictionary<string, List<Film_screening>>>(str_film_screening_json)!;

            RunScript(film_screening, films, ref basket_json_path, ref film_screening_json_path);

            Console.WriteLine();
            Console.WriteLine("Спасибо за покупку, приходите ещё");
            Console.ReadKey();

        }
        public static void RunScript(Dictionary<string, List<Film_screening>> film_screening, List<Film> films, ref string basket_json, ref string film_screening_json)
        {
            Film film = null;
            List<Ticket> basket = new List<Ticket>();
            List<Film_screening> thisFilmScrinings = new List<Film_screening>();
            List<DateOnly> dates_film_screenings = new List<DateOnly>();
            Film_screening thisFilmScrining = null;
            bool repeat = true;
            int script = 0;

            while (repeat)
            {
                switch (script)
                {
                    case 0:
                        OutputFilms(films, ref script);
                        break;
                    case 1:
                        film = ChoiceFilm(films, ref script);
                        OutputInfoFilm(film);
                        break;
                    case 2:
                        thisFilmScrinings = FindThisFilmScrinings(film, film_screening, ref script);
                        break;
                    case 3:
                        dates_film_screenings = FindDataFilmScreening(thisFilmScrinings, ref script);
                        break;
                    case 4:
                        OutputDataFilmScreening(dates_film_screenings, ref script);
                        break;
                    case 5:
                        FindFilmScreeningByData(ChoiseDataFilmScreening(dates_film_screenings, ref script), thisFilmScrinings, ref thisFilmScrinings, ref script);
                        break;
                    case 6:
                        OutputTimeFilmScreening(thisFilmScrinings, ref script);
                        break;
                    case 7:
                        thisFilmScrining = ChoiseTimeFilmScreening(thisFilmScrinings, ref script);
                        break;
                    case 8:
                        OutputPlaseFilmScreening(thisFilmScrining, ref script);
                        break;
                    case 9:
                        OutputActionReturn(ref script);
                        break;
                    case 10:
                        ProofBuyTicket(ref script, ref basket, thisFilmScrining, film);
                        break;
                    case 11:
                        OutputInfoTicket(film, thisFilmScrining, ref script);
                        break;
                    case 12:
                        CotinuationBuy(thisFilmScrining, ref script);
                        break;
                    case 13:
                        ChoiseActionReturn(ref script);
                        break;
                    case 14:
                        EndProgram(ref repeat, basket, basket_json);
                        break;
                }
            }
        }

        public static Film ChoiceFilm(List<Film> films, ref int script) // 1
        {
            bool x = true;
            script = 2;
            Film film = null;
            while (x)
            {
                try
                {
                    string input = Console.ReadLine();
                    int number = int.Parse(input);
                    film = films[number - 1];
                    x = false;
                }
                catch
                {
                    Console.WriteLine("Некорректный ввод поробуйте ещё раз");
                }
            }
            return film;
        }
        public static void OutputFilms(List<Film> films, ref int script) // 0
        {
            Console.WriteLine("Фильмы в прокате:");
            Console.WriteLine();
            for (int i = 0; i < films.Count; i++)
                Console.WriteLine("{0}. {1}", i + 1, films[i].name);
            Console.WriteLine();
            Console.WriteLine("Для выбора фильма напишите его цифру.");
            script = 1;
        }
        public static void OutputInfoFilm(Film film) // 1
        {
            Console.WriteLine("\nИнформация о фильме: {0}", film.name);
            Console.WriteLine("Жанр: {0}", film.genre);
            Console.WriteLine("Год выхода: {0}", film.year);
            Console.WriteLine("Описание: {0}", film.description);
            Console.WriteLine("Фильм идёт в следующте даты:\n");
        }
        public static List<Film_screening> FindThisFilmScrinings(Film film, Dictionary<string, List<Film_screening>> film_screenings, ref int script) // 2
        {
            List<Film_screening> thisFilmScrinings = new List<Film_screening>();
            foreach (KeyValuePair<string, List<Film_screening>> film_screening in film_screenings)
                if (film.name == film_screening.Key)
                    thisFilmScrinings = film_screening.Value;
            script = 3;
            return thisFilmScrinings;
        }
        public static List<DateOnly> FindDataFilmScreening(List<Film_screening> film_screenings, ref int script) // 3
        {
            List<DateOnly> dates_film_screenings = new List<DateOnly>();
            foreach (Film_screening film_screening in film_screenings) {
                bool repeat = false;
                foreach (DateOnly data in dates_film_screenings) {
                    if (data.Year == film_screening.data.Year && data.Month == film_screening.data.Month && data.Day == film_screening.data.Day)
                    {
                        repeat = true;
                        break;
                    }
                }
                if (!repeat)
                    dates_film_screenings.Add(film_screening.data);
            }
            script = 4;
            return dates_film_screenings;
        }
        public static void OutputDataFilmScreening(List<DateOnly> dates_film_screenings, ref int script) // 4
        {
            Console.WriteLine("Даты показа фильма:");
            Console.WriteLine();
            if (dates_film_screenings.Count == 0)
            {
                Console.WriteLine("К сожелению у фильма нет показов. Пожалуйста выбирите другой фильм");
                script = 0;
            }
            else
            {
                for (int i = 0; i < dates_film_screenings.Count; i++)
                {
                    Console.WriteLine("{0}. {1}", i + 1, dates_film_screenings[i]);
                    Console.WriteLine();
                }
                script = 5;
            }
        }
        public static DateOnly ChoiseDataFilmScreening(List<DateOnly> dates_film_screenings, ref int script) // 5
        {
            Console.WriteLine("Для выбора даты сеанса напишите её цифру.");
            DateOnly date_film_screenings = dates_film_screenings[0];
            bool x = true;
            while (x)
            {
                try
                {
                    string input = Console.ReadLine();
                    int number = int.Parse(input);
                    date_film_screenings = dates_film_screenings[number - 1];
                    x = false;
                }
                catch
                {
                    Console.WriteLine("Некорректный ввод поробуйте ещё раз");
                }
            }
            return date_film_screenings;
        }
        public static void FindFilmScreeningByData(DateOnly date_film_screenings, List<Film_screening> all_film_screenings, ref List<Film_screening> film_screenings, ref int script) // 5
        {
            List<Film_screening> film_screenings_temp = new List<Film_screening>();
            for (int i = 0; i < all_film_screenings.Count; i++)
            {
                if (date_film_screenings.Year == all_film_screenings[i].data.Year & date_film_screenings.Month == all_film_screenings[i].data.Month & date_film_screenings.Day == all_film_screenings[i].data.Day)
                    film_screenings_temp.Add(all_film_screenings[i]);
            }
            film_screenings = film_screenings_temp;
            script = 6;
        }

        public static void OutputTimeFilmScreening(List<Film_screening> Film_screenings, ref int script) // 6
        {
            Console.WriteLine("Время показа фильма:");
            Console.WriteLine();
            if (Film_screenings.Count == 0)
            {
                Console.WriteLine("К сожелению у фильма нет показов в это время.");
                script = 0;
            }
            else
            {
                for (int i = 0; i < Film_screenings.Count; i++)
                {
                    Console.WriteLine("{0}. {1}. Цена: {2} руб.", i + 1, Film_screenings[i].time, Film_screenings[i].price);
                    Console.WriteLine();
                }
                script = 7;
            }
        }
        public static Film_screening ChoiseTimeFilmScreening(List<Film_screening> Film_screenings, ref int script) // 7
        {
            Film_screening film_screening = Film_screenings[0];
            bool x = true;
            while (x)
            {
                try
                {
                    string input = Console.ReadLine();
                    int number = int.Parse(input);
                    film_screening = Film_screenings[number - 1];
                    x = false;
                    script = 8;
                }
                catch
                {
                    Console.WriteLine("Некорректный ввод поробуйте ещё раз");
                }
            }
            return film_screening;
        }
        public static void OutputPlaseFilmScreening(Film_screening Film_screening, ref int script) // 8
        {
            Console.WriteLine("Количесво оставшихся мест на сеанс: {0}", Film_screening.countPlase);
            Console.WriteLine();
            if (Film_screening.countPlase == 0)
            {
                Console.WriteLine("К сожелению места закончились.");
                script = 9;
            }
            else
                script = 10;
        }
        public static void OutputActionReturn(ref int script)//9
        {
            Console.WriteLine("Куда хотите вернуться?");
            Console.WriteLine();
            Console.WriteLine("1.Выбор фильма");
            Console.WriteLine("2.Выбор даты");
            Console.WriteLine("3.Выбор времени");
            script = 13;
        }
        public static void ProofBuyTicket(ref int script, ref List<Ticket> basket, Film_screening film_Screening, Film film)//10
        {
            Console.WriteLine("Купить билет?(да/нет)");
            bool x = true;
            while (x)
            {
                string ansewer = Console.ReadLine();
                if (ansewer == "да")
                {
                    script = 11;
                    x = false;
                    basket.Add(new Ticket(film.name, film_Screening.data, film_Screening.time, film_Screening.price));
                    // добавить изменение данных(изменения кол-ва свободных мест)
                }
                else if (ansewer == "нет")
                {
                    script = 9;
                    x = false;
                }
                else
                    Console.WriteLine("Некорректный ввод поробуйте ещё раз");
            }
        }
        public static void OutputInfoTicket(Film Film, Film_screening Film_screening, ref int script)//11
        {
            Console.WriteLine("\n---------------------------");
            Console.WriteLine("Билет куплен\n");
            Console.WriteLine("Информация о билете:");
            Console.WriteLine("Название фильма: {0}", Film.name);
            Console.WriteLine("Дата сеанса: {0}", Film_screening.data);
            Console.WriteLine("Время сеанса: {0}", Film_screening.time);
            Console.WriteLine("Цена билета: {0}", Film_screening.price);
            Console.WriteLine("---------------------------\n");

            script = 12;
        }

        public static void CotinuationBuy(Film_screening Film_screening, ref int script)//12
        {
            Console.WriteLine("Купить ещё?(да/нет)");

            bool x = true;
            while (x)
            {
                string ansewer = Console.ReadLine();
                if (ansewer == "да")
                {
                    script = 0;
                    x = false;
                }
                else if (ansewer == "нет")
                {
                    script = 14;
                    x = false;
                }
                else
                    Console.WriteLine("Некорректный ввод поробуйте ещё раз");
            }
        }
       
        public static void ChoiseActionReturn(ref int script)//13
        {
            bool x = true;
            while (x)
            {
                try
                {
                    string input = Console.ReadLine();
                    int number = int.Parse(input);
                    switch (number)
                    {
                        case 1:
                            script = 0;
                            x = false;
                            break;
                        case 2:
                            x = false;
                            script = 4;
                            break;
                        case 3:
                            script = 6;
                            x = false;
                            break;
                        default: Console.WriteLine("Некорректный ввод поробуйте ещё раз");
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Некорректный ввод поробуйте ещё раз");
                }
            }
        }
        public static void EndProgram(ref bool repeat, List<Ticket> basket, string path)//14
        {
            repeat = false;
            //string path = "C:\\Users\\Я\\source\\repos\\ConsoleApp2\\ConsoleApp2\\Data\\basket.json";
            // Сериализация
            var options = new JsonSerializerOptions { WriteIndented = true }; // опция для развертывания json файла

            string jsonString = JsonSerializer.Serialize(basket, options); // список в строку

            jsonString = Regex.Replace(jsonString, @"\\u([0-9A-Fa-f]{4})", m => "" + (char)Convert.ToInt32(m.Groups[1].Value, 16)); // меняем кодировку 

            File.WriteAllText(path, jsonString); // запись в файл .json
            PrintTickets(basket);

            //Console.WriteLine(Regex.Replace(File.ReadAllText(path), @"\\u([0-9A-Fa-f]{4})", m => "" + (char)Convert.ToInt32(m.Groups[1].Value, 16))); // чтение из файла
        }

        public static void PrintTickets(List<Ticket> basket)
        {
            int counter = 0;
            int priceOfTickets = 0;
            Console.WriteLine("\n---------------------------");
            Console.WriteLine("Чек:");
            foreach (var ticket in basket)
            {
                counter++;
                priceOfTickets += ticket.Price;
                Console.WriteLine("\nБилет №{0}", counter);
                Console.WriteLine("Название фильма: {0}", ticket.Name);
                Console.WriteLine("Дата сеанса: {0}", ticket.Data);
                Console.WriteLine("Время сеанса: {0}", ticket.Time);
                Console.WriteLine("Цена билета: {0}", ticket.Price);
            }
            Console.WriteLine("\nИтоговая стоимость составила: {0}", priceOfTickets);
            Console.WriteLine("---------------------------");
        }

        /*public static List<Cinema> GetCinemas(Dictionary<string, List<Film_screening>> film_screening) 
        {
            List<Cinema> cimena = new List<Cinema>() {new Cinema(film_screening,"Победа") };

            return new List<Cinema>(); 
        }*/
    }
}