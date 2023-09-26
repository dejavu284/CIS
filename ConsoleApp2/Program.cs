using ConsoleApp2.Classes;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Film> films = GetFilmList();
            Dictionary<string, List<Film_screening>> film_screening = GetFilmScreeningList();
            List<Ticket> basket = new List<Ticket>();
            /*List<Cinema> cinemas = GetCinemas(film_screening);*/

            bool repeat = true;
            int script = 0;
            Film film = null;
            var thisFilmScrinings = new List<Film_screening>();
            List<DateOnly> dates_film_screenings = new List<DateOnly>();
            Film_screening thisFilmScrining = null;

            string fileName = "C:\\Users\\Я\\source\\repos\\ConsoleApp2\\ConsoleApp2\\Data\\film_screening.json";

            // Синхронный код
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(film_screening, options);
            jsonString = Regex.Replace(jsonString, @"\\u([0-9A-Fa-f]{4})", m => "" + (char)Convert.ToInt32(m.Groups[1].Value, 16));

            File.WriteAllText(fileName, jsonString);

            // Асинхронный код
            /*using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, film_screening);*/

            Console.WriteLine(Regex.Replace(File.ReadAllText(fileName), @"\\u([0-9A-Fa-f]{4})", m => "" + (char)Convert.ToInt32(m.Groups[1].Value, 16)));



            Console.ReadLine();

            while (repeat)
            {
                switch (script)
                {
                    case 0:
                        OutputFilms(films, ref script);
                        break;
                    case 1:
                        OutputInfoFilm(ChoiceFilm(films, ref film, ref script));
                        break;
                    case 2:
                        FindThisFilmScrinings(film, film_screening, ref thisFilmScrinings, ref script);
                        break;
                    case 3:
                        FindDataFilmScreening(thisFilmScrinings, ref dates_film_screenings, ref script);
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
                        ProofBuyTicket(ref script, ref basket,thisFilmScrining);
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
                        EndProgram(ref repeat, basket);
                        break;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Спасибо за покупку, приходите ещё");
            Console.ReadLine();

        }

        public static Film ChoiceFilm(List<Film> films, ref Film film, ref int script) // 1
        {
            bool x = true;
            script = 2;
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
            Console.WriteLine("Информация о фильме: {0}", film.name);
            Console.WriteLine();
            Console.WriteLine("Жанр: {0}", film.genre);
            Console.WriteLine();
            Console.WriteLine("Год выхода: {0}", film.year);
            Console.WriteLine();
            Console.WriteLine("Описание: {0}", film.description);
            Console.WriteLine();
            Console.WriteLine("Фильм идёт в следующте даты:");
            Console.WriteLine();
        }
        public static List<Film_screening> FindThisFilmScrinings(Film film, Dictionary<string, List<Film_screening>> film_screenings, ref List<Film_screening> thisFilmScrinings, ref int script) // 2
        {
            foreach (KeyValuePair<string, List<Film_screening>> film_screening in film_screenings)
                if (film.name == film_screening.Key)
                    thisFilmScrinings = film_screening.Value;
            script = 3;
            return thisFilmScrinings;
        }
        public static void FindDataFilmScreening(List<Film_screening> film_screenings, ref List<DateOnly> dates_film_screenings, ref int script) // 3
        {
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
                    Console.WriteLine("{0}. {1}.0{2}.{3}", i + 1, dates_film_screenings[i].Day, dates_film_screenings[i].Month, dates_film_screenings[i].Year);
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
                    Console.WriteLine("{0}. {1}:{2}", i + 1, Film_screenings[i].time. Hour, Film_screenings[i].time.Minute);
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
            Console.WriteLine("Количесво оставшихся мест на сеанс: {0}", Film_screening.countTiket);
            Console.WriteLine();
            if (Film_screening.countTiket == 0)
            {
                Console.WriteLine("К сожелению места закончились.");
                script = 9;
            }
            else
                script = 10;
        }

        public static void ProofBuyTicket(ref int script,ref List<Ticket> basket,Film_screening film_Screening)//10
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
                    basket.Add(new Ticket(film_Screening));
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
            Console.WriteLine("Билет куплен");
            Console.WriteLine();
            Console.WriteLine("Информация о билете:");
            Console.WriteLine("Фильм: {0}", Film.name);
            Console.WriteLine();
            Console.WriteLine("Жанр: {0}", Film.genre);
            Console.WriteLine();
            Console.WriteLine("Дата показа: {0}.{1}.{2}", Film_screening.data.Day, Film_screening.data.Month, Film_screening.data.Year);
            Console.WriteLine();
            Console.WriteLine("Время показа: {0}:{1}", Film_screening.time.Hour, Film_screening.time.Minute);
            Console.WriteLine();

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
        public static void OutputActionReturn(ref int script)//9
        {
            Console.WriteLine("Куда хотите вернуться?");
            Console.WriteLine();
            Console.WriteLine("1.Выбор фильма");
            Console.WriteLine("2.Выбор даты");
            Console.WriteLine("3.Выбор времени");
            script = 13;
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
        public static void EndProgram(ref bool repeat,List<Ticket> basket)//14
        {
            repeat = false;
            // добавить создание файла 
        }

        public static List<Film> GetFilmList()
        {
            List<Film> films = new List<Film>() {
                new Film(
                    "Интерстеллар",
                    "Научная фантастика",
                    "Фильм рассказывает историю группы исследователей, которые отправляются в космическое путешествие, чтобы найти новый дом для человечества в другой галактике.",
                    2014
                    ),
                new Film(
                    "Темный рыцарь",
                    "Боевик",
                    "Этот фильм о супергерое Бэтмене, который сражается с преступником по имени Джокер, чтобы спасти Готэм-сити.",
                    2008
                    ),
                new Film(
                    "Зеленая миля",
                    "Драма",
                    "Фильм рассказывает историю тюремного смотрителя, который обнаруживает, что один из заключенных обладает необычными способностями.",
                    1999
                    ),
                new Film(
                    "Властелин колец: Братство кольца",
                    "Фэнтези",
                    "Фильм о группе героев, отправляющихся в опасное путешествие, чтобы уничтожить кольцо власти.",
                    2001
                    )
            };
            return films;
        }
        public static Dictionary<string, List<Film_screening>> GetFilmScreeningList()
        {
            var film_screening = new Dictionary<string, List<Film_screening>>()
            {
                { "Властелин колец: Братство кольца", new List<Film_screening>()
                    {
                        new Film_screening(
                            new DateOnly(2023, 9, 24),
                            new TimeOnly(17,30),
                            12,
                            100),
                        new Film_screening(
                            new DateOnly(2023,9,24),
                            new TimeOnly(19,15),
                            3,
                            200),
                        new Film_screening(
                            new DateOnly(2023,9,24),
                            new TimeOnly(22,00),
                            3,
                            200),
                        new Film_screening(
                            new DateOnly(2023,9,25),
                            new TimeOnly(14,30),
                            3,
                            200),
                        new Film_screening(
                            new DateOnly(2023,9,25),
                            new TimeOnly(06,00),
                            3,
                            200)
                    }
                },
                { "Зеленая миля", new List<Film_screening>()
                    {
                        new Film_screening(
                            new DateOnly(2023,10,24),
                            new TimeOnly(16,30),
                            10,
                            200),
                        new Film_screening(
                            new DateOnly(2023,10,24),
                            new TimeOnly(14,30),
                            1,
                            250)
                    }
                },
                { "Темный рыцарь", new List<Film_screening>()
                    {
                        new Film_screening(
                            new DateOnly(2023,11,24),
                            new TimeOnly(12,30),
                            4,
                            300),
                        new Film_screening(
                            new DateOnly(2023,10,24),
                            new TimeOnly(12,30),
                            13,
                            150)
                    }
                },
                { "Интерстеллар", new List<Film_screening>()
                    {
                        new Film_screening(
                            new DateOnly(2023,9,24),
                            new TimeOnly(14,30),
                            0,
                            200)
                    }
                }
            };
            return film_screening;
        }
        /*public static List<Cinema> GetCinemas(Dictionary<string, List<Film_screening>> film_screening) 
        {
            List<Cinema> cimena = new List<Cinema>() {new Cinema(film_screening,"Победа") };

            return new List<Cinema>(); 
        }*/
    }
}