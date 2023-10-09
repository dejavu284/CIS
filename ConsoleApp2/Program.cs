using ConsoleApp2.Classes;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;
using File = System.IO.File;
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

            RunScript(film_screening, films, basket_json_path, film_screening_json_path);
        }
        public static void RunScript(Dictionary<string, List<Film_screening>> film_screening, List<Film> films, string basket_json, string film_screening_json)
        {
            Film film = null;
            List<Ticket> basket = new List<Ticket>();
            List<Film_screening> thisFilmScrinings = new List<Film_screening>();
            List<DateOnly> dates_film_screenings = new List<DateOnly>();
            Film_screening thisFilmScrining = null;
            bool repeat = true;
            int scriptNumber = 0;

            while (repeat)
            {
                switch (scriptNumber)
                {
                    case 0:
                        OutputFilms(films);
                        scriptNumber = 1;
                        break;
                    case 1:
                        film = ChoiceFilm(films);
                        OutputInfoFilm(film);
                        scriptNumber = 2;
                        break;
                    case 2:
                        thisFilmScrinings = FindThisFilmScrinings(film, film_screening, ref scriptNumber);
                        break;
                    case 3:
                        dates_film_screenings = FindDataFilmScreening(thisFilmScrinings, ref scriptNumber);
                        break;
                    case 4:
                        OutputDataFilmScreening(dates_film_screenings, ref scriptNumber);
                        break;
                    case 5:
                        DateOnly dataFilmSreening = ChoiseDataFilmScreening(dates_film_screenings);
                        thisFilmScrinings = FindFilmScreeningByData(dataFilmSreening, thisFilmScrinings);
                        scriptNumber = 6;
                        break;
                    case 6:
                        OutputTimeFilmScreening(thisFilmScrinings);
                        scriptNumber = 7;
                        break;
                    case 7:
                        thisFilmScrining = ChoiseTimeFilmScreening(thisFilmScrinings);
                        scriptNumber = 8;
                        break;
                    case 8:
                        OutputPlaseFilmScreening(thisFilmScrining, ref scriptNumber);
                        break;
                    case 9:
                        OutputActionReturn(ref scriptNumber);
                        break;
                    case 10:
                        ProofBuyTicket(ref scriptNumber, ref basket, thisFilmScrining, film);
                        break;
                    case 11:
                        OutputInfoTicket(film, thisFilmScrining, ref scriptNumber);
                        break;
                    case 12:
                        scriptNumber = PoolCotinuationBuy();
                        break;
                    case 13:
                        scriptNumber = ChoiseActionReturn();
                        break;
                    case 14:
                        EndProgram(ref repeat, basket, basket_json);
                        break;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Спасибо за покупку, приходите ещё");
            Console.ReadKey();
        }

        public static Film ChoiceFilm(List<Film> films) // 1 // Кирилл
        {
            Film? film = null;
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Для выбора фильма напишите его цифру.");
                string? inputStr = Console.ReadLine();
                if (CheckingNumberEntryInList(films, inputStr))
                {
                    film = films[int.Parse(inputStr!) - 1];
                    break;
                }
                else DisplayMessageIncorrectInput();
            }
            return film!;
        }
        public static bool CheckingNumberEntryInList<T>(List<T> films, string? index_str)
        {
            uint index;
            bool resultChecked = false;
            bool tryParseCheced = uint.TryParse(index_str, out index);
            if (tryParseCheced && films.Count >= index)
                resultChecked = true;
            return resultChecked;
        }
        public static void DisplayMessageIncorrectInput()
        {
            Console.WriteLine();
            Console.WriteLine("Некорректный ввод поробуйте ещё раз");
        }
        public static void OutputFilms(List<Film> films) // 0 // Кирилл
        {
            Console.WriteLine();
            Console.WriteLine("Фильмы в прокате:");
            Console.WriteLine();
            for (int i = 0; i < films.Count; i++)
                Console.WriteLine("{0}. {1}", i + 1, films[i].name);
        }
        public static void OutputInfoFilm(Film film) // 1
        {
            Console.WriteLine("\nИнформация о фильме: {0}", film.name); // Кирилл
            Console.WriteLine("Жанр: {0}", film.genre);
            Console.WriteLine("Год выхода: {0}", film.year);
            Console.WriteLine("Описание: {0}", film.description);
            Console.WriteLine("Фильм идёт в следующте даты:\n");
        }
        public static List<Film_screening> FindThisFilmScrinings(Film film, Dictionary<string, List<Film_screening>> film_screenings, ref int script) // 2 // Саша
        {
            List<Film_screening> thisFilmScrinings = new List<Film_screening>();
            foreach (KeyValuePair<string, List<Film_screening>> film_screening in film_screenings)
                if (film.name == film_screening.Key)
                    thisFilmScrinings = film_screening.Value;
            script = 3;
            return thisFilmScrinings;
        }
        public static List<DateOnly> FindDataFilmScreening(List<Film_screening> film_screenings, ref int script) // 3 // Саша
        {
            List<DateOnly> dates_film_screenings = new List<DateOnly>();
            foreach (Film_screening film_screening in film_screenings) {
                bool repeat = false;
                foreach (DateOnly data in dates_film_screenings) {
                    if (data == film_screening.data)
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
        public static void OutputDataFilmScreening(List<DateOnly> dates_film_screenings, ref int script) // 4 // Саша
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
        //сделал проверку в метод OutputDataFilmScreening не увидел что он твой(((
        public static bool CheckAvailabilityDateFilmScreening(List<DateOnly> dates_film_screenings)
        {
            bool thereDateFilmScreening;
            if (dates_film_screenings.Count == 0 || dates_film_screenings == null)
                thereDateFilmScreening = false;
            else thereDateFilmScreening = true;
            return thereDateFilmScreening;
        }
        public static DateOnly ChoiseDataFilmScreening(List<DateOnly> allDateFilmScreenings) // 5 // Кирилл
        {
            DateOnly dateFilmScreenings;
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Для выбора даты показа напишите её цифру.");
                string? inputStr = Console.ReadLine();
                if (CheckingNumberEntryInList(allDateFilmScreenings, inputStr))
                {
                    dateFilmScreenings = allDateFilmScreenings[int.Parse(inputStr!) - 1];
                    break;
                }
                else DisplayMessageIncorrectInput();
            }
            return dateFilmScreenings!;
        }

        // перенести метод в класс FilmScreening
        public static List<Film_screening> FindFilmScreeningByData(DateOnly date_film_screenings, List<Film_screening> all_film_screenings) // 5 // Кирилл
        {
            List<Film_screening> film_screenings_temp = new List<Film_screening>();
            for (int i = 0; i < all_film_screenings.Count; i++)
                if(date_film_screenings == all_film_screenings[i].data)
                    film_screenings_temp.Add(all_film_screenings[i]);
            return film_screenings_temp;
        }

        public static void OutputTimeFilmScreening(List<Film_screening> Film_screenings) // 6 // Кирилл
        {
            Console.WriteLine();
            Console.WriteLine("Время показа фильма:");
            for (int i = 0; i < Film_screenings.Count; i++)
            {
                Console.WriteLine();
                Console.WriteLine("{0}. {1}. Цена: {2} руб.", i + 1, Film_screenings[i].time, Film_screenings[i].price);
            }
        }
        public static Film_screening ChoiseTimeFilmScreening(List<Film_screening> filmScreeningInCertainDay) // 7 // Кирилл
        {
            Film_screening filmScreeningInCertainTime;
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Для выбора времени показа напишите её цифру.");
                string? inputStr = Console.ReadLine();
                if (CheckingNumberEntryInList(filmScreeningInCertainDay, inputStr))
                {
                    filmScreeningInCertainTime = filmScreeningInCertainDay[int.Parse(inputStr!) - 1];
                    break;
                }
                else DisplayMessageIncorrectInput();
            }
            return filmScreeningInCertainTime!;
        }
        public static void OutputPlaseFilmScreening(Film_screening Film_screening, ref int script) // 8 // Саша
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
        public static void OutputActionReturn(ref int script)//9 // Саша
        {
            Console.WriteLine("Куда хотите вернуться?");
            Console.WriteLine();
            Console.WriteLine("1.Выбор фильма");
            Console.WriteLine("2.Выбор даты");
            Console.WriteLine("3.Выбор времени");
            script = 13;
        }
        public static void ProofBuyTicket(ref int script, ref List<Ticket> basket, Film_screening film_Screening, Film film)//10 // Саша
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
        public static void OutputInfoTicket(Film Film, Film_screening Film_screening, ref int script)//11 // Саша
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

        public static int PoolCotinuationBuy()//12 // //Кирилл
        {
            Console.WriteLine("Купить ещё билет?(да/нет)");
            while (true)
            {
                string? ansewer = Console.ReadLine();
                if (ansewer == "да")
                    return 0;
                else if (ansewer == "нет")
                    return 14;
                else
                    DisplayMessageIncorrectInput();
            }
        }
       
        public static int ChoiseActionReturn()//13 //Кирилл
        {
            while(true) 
            {
                string? input = Console.ReadLine();
                uint scriptNumber;
                if (uint.TryParse(input,out scriptNumber))
                    switch (scriptNumber)
                    {
                        case 1:
                            return 0;
                        case 2:
                            return 4;
                        case 3:
                            return 6;
                        default:
                            DisplayMessageIncorrectInput();
                            break;
                    }
                else DisplayMessageIncorrectInput();
            }
        }
        public static void EndProgram(ref bool repeat, List<Ticket> basket, string path)//14 // Саша
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

        public static void PrintTickets(List<Ticket> basket) // Саша
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
    }
}