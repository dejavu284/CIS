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
                        thisFilmScrinings = FindThisFilmScrinings(film, film_screening);
                        scriptNumber = 3;
                        break;
                    case 3:
                        dates_film_screenings = FindDataFilmScreening(thisFilmScrinings);
                        scriptNumber = 4;
                        break;
                    case 4:
                        scriptNumber = OutputDataFilmScreening(dates_film_screenings);
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
                        scriptNumber = OutputPlaseFilmScreening(thisFilmScrining);
                        break;
                    case 9:
                        OutputActionReturn();
                        scriptNumber = 13;
                        break;
                    case 10:
                        scriptNumber = ProofBuyTicket(basket, thisFilmScrining, film); // не делал
                        break;
                    case 11:
                        OutputTicket(basket[^1]); // не делал
                        scriptNumber = 12;
                        break;
                    case 12:
                        scriptNumber = PoolCotinuationBuy();
                        break;
                    case 13:
                        scriptNumber = ChoiseActionReturn();
                        break;
                    case 14:
                        SaveTickets(basket, basket_json);
                        PrintReceipt(basket);
                        repeat = false;
                        break;
                }
            }
            Console.WriteLine("\nСпасибо за покупку, приходите ещё");
            Console.ReadKey();
        }

        public static Film ChoiceFilm(List<Film> films) // 1 // Кирилл
        {
            Film? film;
            while (true)
            {
                Console.WriteLine("Для выбора фильма напишите его цифру.");
                string? inputStr = Console.ReadLine();
                if (CheckingNumberEntryInList(films, inputStr))
                {
                    film = films[int.Parse(inputStr!) - 1];
                    break;
                }
                else DisplayMessageIncorrectInput();
            }
            Console.WriteLine();
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
            Console.WriteLine("\nНекорректный ввод поробуйте ещё раз");
        }
        public static void OutputFilms(List<Film> films) // 0 // Кирилл
        {
            Console.WriteLine("Фильмы в прокате:\n");
            for (int i = 0; i < films.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, films[i].name);
            }
            Console.WriteLine();
        }
        public static void OutputInfoFilm(Film film) // 1
        {
            Console.WriteLine("Информация о фильме {0}", film.name); // Кирилл
            Console.WriteLine("Жанр: {0}", film.genre);
            Console.WriteLine("Год выхода: {0}", film.year);
            Console.WriteLine("Описание: {0}\n", film.description);
            Console.WriteLine("Фильм идёт в следующте даты:\n");
        }
        public static List<Film_screening> FindThisFilmScrinings(Film film, Dictionary<string, List<Film_screening>> film_screenings) // 2 // Саша
        {
            List<Film_screening> thisFilmScrinings = new ();
            foreach (KeyValuePair<string, List<Film_screening>> film_screening in film_screenings)
                if (film.name == film_screening.Key)
                    thisFilmScrinings = film_screening.Value;
            return thisFilmScrinings;
        }
        public static List<DateOnly> FindDataFilmScreening(List<Film_screening> film_screenings) // 3 // Саша
        {
            List<DateOnly> dates_film_screenings = new ();
            foreach (Film_screening film_screening in film_screenings)
            {
                if (!CheckRepeatDates(dates_film_screenings, film_screening))
                {
                    dates_film_screenings.Add(film_screening.data);
                }
            }
            return dates_film_screenings;
        }
        public static bool CheckRepeatDates(List<DateOnly> dates_film_screenings, Film_screening film_screening)
        {
            bool repeat = false;
            foreach (DateOnly data in dates_film_screenings)
            {
                if (CheckDatesEqual(data, film_screening))
                {
                    repeat = true;
                    break;
                }
            }
            return repeat;
        }
        public static bool CheckDatesEqual(DateOnly data, Film_screening film_screening)
        {
            return data == film_screening.data;
        }
        public static int OutputDataFilmScreening(List<DateOnly> dates_film_screenings) // 4 // Саша
        {
            int script;
            if (CheckDateFilmScreeningNotEmpty(dates_film_screenings))
            {
                Console.WriteLine("Даты показа фильма:\n"); 
                for (int i = 0; i < dates_film_screenings.Count; i++)
                {
                    Console.WriteLine("{0}. {1}", i + 1, dates_film_screenings[i]);
                }
                script = 5;
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("К сожелению у фильма нет показов. Пожалуйста выбирите другой фильм\n");
                script = 0;
            }
            return script;
        }
        //сделал проверку в метод OutputDataFilmScreening не увидел что он твой(((спасибо)))
        public static bool CheckDateFilmScreeningNotEmpty(List<DateOnly> dates_film_screenings)
        {
            return !(dates_film_screenings.Count == 0 || dates_film_screenings == null);
        }
        public static DateOnly ChoiseDataFilmScreening(List<DateOnly> allDateFilmScreenings) // 5 // Кирилл
        {
            DateOnly dateFilmScreenings;
            while (true)
            {
                Console.WriteLine("Для выбора даты показа напишите её цифру.");
                string? inputStr = Console.ReadLine();
                if (CheckingNumberEntryInList(allDateFilmScreenings, inputStr))
                {
                    dateFilmScreenings = allDateFilmScreenings[int.Parse(inputStr!) - 1];
                    break;
                }
                else
                {
                    DisplayMessageIncorrectInput();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
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
            Console.WriteLine("Время показа фильма:");
            for (int i = 0; i < Film_screenings.Count; i++)
            {
                Console.WriteLine("\n{0}. {1}. Цена: {2} руб.", i + 1, Film_screenings[i].time, Film_screenings[i].price);
            }
        }
        public static Film_screening ChoiseTimeFilmScreening(List<Film_screening> filmScreeningInCertainDay) // 7 // Кирилл
        {
            Film_screening filmScreeningInCertainTime;
            while (true)
            {
                Console.WriteLine("\nДля выбора времени показа напишите её цифру.");
                string? inputStr = Console.ReadLine();
                if (CheckingNumberEntryInList(filmScreeningInCertainDay, inputStr))
                {
                    filmScreeningInCertainTime = filmScreeningInCertainDay[int.Parse(inputStr!) - 1];
                    break;
                }
                else
                {
                    DisplayMessageIncorrectInput();
                }
            }
            Console.WriteLine();
            return filmScreeningInCertainTime!;
        }
        public static int OutputPlaseFilmScreening(Film_screening filmScreening) // 8 // Саша
        {
            int script;
            if (CheckPlaseNotEmpty(filmScreening))
            {
                Console.WriteLine("Количесво оставшихся мест на сеанс: {0}\n", filmScreening.countTiket);
                script = 10;
            }
            else
            {
                Console.WriteLine("К сожелению места закончились.");
                script = 9;
            }
            return script;
        }
        public static bool CheckPlaseNotEmpty(Film_screening filmScreening)
        {
            return !(filmScreening.countTiket == 0);
        }
        public static void OutputActionReturn()//9 // Саша
        {
            Console.WriteLine("Куда хотите вернуться?\n");
            Console.WriteLine("1.Выбор фильма");
            Console.WriteLine("2.Выбор даты");
            Console.WriteLine("3.Выбор времени");
        }
        public static int ProofBuyTicket(List<Ticket> basket, Film_screening film_Screening, Film film)//10 // Саша
        {
            Console.WriteLine("Купить билет?(да/нет)");
            bool x = true;
            int script = -1;
            while (x)
            {
                string answer = Console.ReadLine();
                if (CheckAnswerIsPositive(answer))
                {
                    script = 11;
                    x = false;
                    basket.Add(new Ticket(film.name, film_Screening.data, film_Screening.time, film_Screening.price));
                    // добавить изменение данных(изменения кол-ва свободных мест)
                }
                else if (CheckAnswerIsNegative(answer))
                {
                    script = 9;
                    x = false;
                }
                else
                {
                    DisplayMessageIncorrectInput();
                }
                Console.WriteLine();
            }
            Console.WriteLine("Билет куплен\n");
            return script;
        }
        public static bool CheckAnswerIsPositive(string answer)
        {
            return answer == "да";
        }
        public static bool CheckAnswerIsNegative(string answer)
        {
            return answer == "нет";
        }
        public static void OutputTicket(Ticket ticket)//11 // Саша
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("Информация о билете:");
            OutputInfoOfTicket(ticket);
            Console.WriteLine("---------------------------\n");
        }
        public static void OutputInfoOfTicket(Ticket ticket)
        {
            Console.WriteLine("Название фильма: {0}", ticket.Name);
            Console.WriteLine("Дата сеанса: {0}", ticket.Data);
            Console.WriteLine("Время сеанса: {0}", ticket.Time);
            Console.WriteLine("Цена билета: {0}", ticket.Price);
        }
        public static int PoolCotinuationBuy()//12 //Кирилл
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
                if (uint.TryParse(input,out uint scriptNumber))
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
        public static void SaveTickets(List<Ticket> basket, string path)//14 // Саша
        {
            var options = new JsonSerializerOptions { WriteIndented = true }; // опция для развертывания json файла
            string jsonString = JsonSerializer.Serialize(basket, options); // список в строку
            jsonString = Regex.Replace(jsonString, @"\\u([0-9A-Fa-f]{4})", m => "" + (char)Convert.ToInt32(m.Groups[1].Value, 16)); // меняем кодировку 
            File.WriteAllText(path, jsonString); // запись в файл .json
        }

        public static void PrintReceipt(List<Ticket> basket) // Саша
        {
            int priceOfTickets = 0;
            Console.WriteLine("\n---------------------------");
            Console.WriteLine("Чек:");
            for (int i = 0; i < basket.Count; i++)
            {
                priceOfTickets += basket[i].Price;
                Console.WriteLine("\nБилет №{0}", i + 1);
                OutputInfoOfTicket(basket[i]);
            }
            Console.WriteLine("\nИтоговая стоимость составила: {0}", priceOfTickets);
            Console.WriteLine("---------------------------");
        }
    }
}