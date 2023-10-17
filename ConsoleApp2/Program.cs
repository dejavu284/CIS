using ConsoleApp2.Classes;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;
using System.Text.RegularExpressions;
using File = System.IO.File;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            if (DataIsCorrect(args))
            {
                string currentDirectory = $"{Environment.CurrentDirectory}";
                string filmJsonPath = currentDirectory + "\\Data\\" + args[0];
                string filmScreeningJsonPath = currentDirectory + "\\Data\\" + args[1];
                string basketJsonPath = currentDirectory + "\\Data\\" + args[2];

                List<Film> films = new();
                Dictionary<string, List<FilmScreening>> filmScreening = new();

                if (TryDeserializ(filmJsonPath, ref films) && TryDeserializ(filmScreeningJsonPath, ref filmScreening))
                {
                    List<Ticket> basket = BuyTickets(filmScreening, films);
                    SaveTickets(basket, basketJsonPath);
                }
                Console.WriteLine("\nСпасибо за покупку, приходите ещё");
                Console.ReadKey();
            }
            else
            {
                DisplayMessageIncorrectInput();
            }
        }
        public static bool TryDeserializ<T>(string path, ref T element)
            where T : new()
        {
            try
            {
                // Десериализация film.json в список films
                string textJson = File.ReadAllText(path);
                element = JsonSerializer.Deserialize<T>(textJson)!;
                var a = new T();
                return true;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Не найден файл:\n{0}", path);
                return false;
            }
            catch (JsonException)
            {
                Console.WriteLine("Ошибка в файле:\n{0}", path);
                return false;
            }
            catch
            {
                Console.WriteLine("Ошибка, попробуйте еще раз.", path);
                return false;
            }
        }
        public static bool DataIsCorrect(string[] args)
        {
            if (args.Length == 3)
            {
               return  args.All(x => x.Contains(".json"));
            }
            return false;
        }
        public static bool IsFilmScreeningNotNull(List<FilmScreening> filmScreening)
        {
            return filmScreening.Count != 0;
        }
        //метод класса Basket (возможно конструктор класса)
        public static List<Ticket> BuyTickets(Dictionary<string, List<FilmScreening>> filmScreening, List<Film> films)
        {
            List<Ticket> basket = new();
            bool flagBuyTickets = true;
            while (flagBuyTickets)
            {

                OutputFilms(films);
                // Выбор фильма
                List<FilmScreening> filmScreeningsInOneFilm = ChooseFilm(films, filmScreening);
                // Выбор даты
                List<FilmScreening> filmScreeningsInCertainDate = ChooseFilmScreeingInCertainDate(filmScreeningsInOneFilm);
                // Выбор времени
                FilmScreening filmScreeningInCertainTime = ChooseFilmScreeningsInCertainTime(filmScreeningsInCertainDate);

                if (IsPlacesNotEmpty(filmScreeningInCertainTime))
                {
                    OutputPlaseFilmScreening(filmScreeningInCertainTime);
                    if (PoolYesOrNo("Купить билет","y","n"))
                    {
                        basket = AddTicket(basket, filmScreeningInCertainTime);//метод класса Basket
                    }
                    OutputCheck(basket);
                    flagBuyTickets = !PoolYesOrNo("Закончить", "y", "n");
                }
            }
            return new List<Ticket>();
        }
        public static FilmScreening ChooseFilmScreeningsInCertainTime(List<FilmScreening> filmScreeningsInCertainDate)
        {
            FilmScreening filmScreeningsInCertainTime;
            bool flagChooseTime = true;
            while (flagChooseTime)
            {
                OutputTimeFilmScreening(filmScreeningsInCertainDate);
                filmScreeningsInCertainTime = ChoiseTimeFilmScreening(filmScreeningsInCertainDate);
                if (!IsPlacesNotEmpty(filmScreeningsInCertainTime))
                {
                    Console.WriteLine("На выбранное время мест нет.\n");
                    return filmScreeningsInCertainTime;
                }
                flagChooseTime = PoolYesOrNo("Выбрать другое время", "y", "n");
            }
            throw new InvalidExpressionException ();// сделать свой эксепшен
        }
        public static List<FilmScreening> ChooseFilmScreeingInCertainDate(List<FilmScreening> filmScreeningsInOneFilm)
        {
            bool flagChooseDate = true;
            List < FilmScreening > filmScreeningsInCertainDay = new();
            while (flagChooseDate)
            {
                List<DateOnly>  datesFilmScreenings = FindDataFilmScreening(filmScreeningsInOneFilm);
                OutputDataFilmScreening(datesFilmScreenings);
                DateOnly certainDataFilmSreening = ChoiseDataFilmScreening(datesFilmScreenings);
                filmScreeningsInCertainDay = FindFilmScreeningByData(certainDataFilmSreening, filmScreeningsInOneFilm);
                flagChooseDate = PoolYesOrNo("Выбрать другую дату", "y", "n");
            }
            return filmScreeningsInCertainDay;
        }
        public static List<FilmScreening> ChooseFilm(List<Film> films, Dictionary<string, List<FilmScreening>> filmScreening)
        {
            Film film;
            List<FilmScreening> filmScreeningsInOneFilm = new();
            bool flagChooseFilm = true;
            while (flagChooseFilm) // Выбор фильма
            {
                film = GetFilm(films);
                filmScreeningsInOneFilm = FindThisFilmScrinings(film.name, filmScreening);
                if (IsFilmScreeningNotNull(filmScreeningsInOneFilm))
                {
                    OutputInfoFilm(film);
                    flagChooseFilm = false;
                }
                Console.WriteLine("К сожалению, фильм не идет в кинотеатре\nВыберете другой фильм\n");
            }
            return filmScreeningsInOneFilm;
        }
        public static Film GetFilm(List<Film> films) 
        {
            Film? film;
            while (true)
            {
                Console.WriteLine("Для выбора фильма напишите его цифру:\n");
                string? inputIndex = Console.ReadLine();
                film = ChooseFilm(films, inputIndex);
                if(film.name != null)
                {
                    break;
                }
            }
            Console.WriteLine();
            return film!;
        }
        public static Film ChooseFilm(List<Film> films, string index)
        {
            if (IsNumberInList(films, index))
            {
                return films[int.Parse(index!) - 1];
            }
            else
            {
                DisplayMessageIncorrectInput();
                return new Film(null, null, null, -1);
            }
        }
        public static bool IsNumberInList<T>(List<T> films, string? indexStr)
        {
            bool tryParseChecked = uint.TryParse(indexStr, out uint index);
            return tryParseChecked && films.Count >= index;
        }
        public static void DisplayMessageIncorrectInput()
        {
            Console.WriteLine("\nНекорректный ввод поробуйте ещё раз");
        }
        public static void OutputFilms(List<Film> films)
        {
            Console.WriteLine("Фильмы в прокате:\n");
            for (int i = 0; i < films.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, films[i].name);
            }
            Console.WriteLine();
        }
        public static void OutputInfoFilm(Film film) 
        {
            Console.WriteLine("Информация о фильме {0}", film.name);
            Console.WriteLine("Жанр: {0}", film.genre);
            Console.WriteLine("Год выхода: {0}", film.year);
            Console.WriteLine("Описание: {0}\n", film.description);
        }
        public static List<FilmScreening> FindThisFilmScrinings(string filmName, Dictionary<string, List<FilmScreening>> filmScreenings)
        {
            List<FilmScreening> thisFilmScrinings = new();
            foreach (KeyValuePair<string, List<FilmScreening>> filmScreening in filmScreenings)
            {
                if (filmName == filmScreening.Key)
                {
                    thisFilmScrinings = filmScreening.Value;
                }
            }
            return thisFilmScrinings;
        }
        public static List<DateOnly> FindDataFilmScreening(List<FilmScreening> filmScreenings)
        {
            List<DateOnly> datesFilmScreenings = new();
            foreach (FilmScreening filmScreening in filmScreenings)
            {
                if (!IsDatesRepeating(datesFilmScreenings, filmScreening))
                {
                    datesFilmScreenings.Add(filmScreening.data);
                }
            }
            return datesFilmScreenings;
        }
        public static bool IsDatesRepeating(List<DateOnly> datesFilmScreenings, FilmScreening filmScreening)
        {
            bool repeat = false;
            foreach (DateOnly data in datesFilmScreenings)
            {
                if (IsDatesEqual(data, filmScreening))
                {
                    repeat = true;
                    break;
                }
            }
            return repeat;
        }
        public static bool IsDatesEqual(DateOnly data, FilmScreening filmScreening)
        {
            return data == filmScreening.data;
        }
        public static void OutputDataFilmScreening(List<DateOnly> datesFilmScreenings)
        {
            Console.WriteLine("Даты показа фильма:\n");
            for (int i = 0; i < datesFilmScreenings.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, datesFilmScreenings[i]);
            }
            Console.WriteLine();
        }

        public static bool DateIsEmpty(List<DateOnly> datesFilmScreenings)
        {
            return (datesFilmScreenings.Count == 0 || datesFilmScreenings == null);
        }

        public static DateOnly ChoiseDataFilmScreening(List<DateOnly> allDateFilmScreenings) 
        {
            DateOnly dateFilmScreenings;
            while (true)
            {
                Console.WriteLine("Для выбора даты показа напишите её цифру.");
                string? inputIndex = Console.ReadLine();
                if (IsNumberInList(allDateFilmScreenings, inputIndex))
                {
                    dateFilmScreenings = allDateFilmScreenings[int.Parse(inputIndex!) - 1];
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

        public static List<FilmScreening> FindFilmScreeningByData(DateOnly datesFilmScreenings, List<FilmScreening> allFilmScreenings)
        {
            List<FilmScreening> filmScreeningsTemp = new List<FilmScreening>();
            for (int i = 0; i < allFilmScreenings.Count; i++)
            {
                if(datesFilmScreenings == allFilmScreenings[i].data)
                {
                    filmScreeningsTemp.Add(allFilmScreenings[i]);
                }
            }
            return filmScreeningsTemp;
        }
      
        public static void OutputTimeFilmScreening(List<FilmScreening> filmscreenings) 
        {
            Console.WriteLine("Время показа фильма:");
            for (int i = 0; i < filmscreenings.Count; i++)
            {
                Console.WriteLine("\n{0}. {1}. Цена: {2} руб.", i + 1, filmscreenings[i].time, filmscreenings[i].price);
            }
        }
        public static FilmScreening ChoiseTimeFilmScreening(List<FilmScreening> filmScreeningInCertainDay)
        {
            FilmScreening filmScreeningInCertainTime;
            while (true)
            {
                Console.WriteLine("\nДля выбора времени показа напишите её цифру.");
                string? inputStr = Console.ReadLine();
                if (IsNumberInList(filmScreeningInCertainDay, inputStr))
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

        // поменять название, на // OutputCountPlace
        public static void OutputPlaseFilmScreening(FilmScreening filmScreening)
        {
            Console.WriteLine("Количесво оставшихся мест на сеанс: {0}\n", filmScreening.countTiket);
        }
     
        public static bool IsPlacesNotEmpty(FilmScreening filmScreening)
        {
            return (filmScreening.countTiket != 0);
        }
        public static void OutputActionReturn()//9 // Саша
        {
            Console.WriteLine("Куда хотите вернуться?\n");
            Console.WriteLine("1.Выбор фильма");
            Console.WriteLine("2.Выбор даты");
            Console.WriteLine("3.Выбор времени");
        }

        //метод класса Basket
        public static List<Ticket> AddTicket(List<Ticket> basket, FilmScreening filmScreening)
        { 
            basket.Add(new Ticket(filmScreening.name, filmScreening.data, filmScreening.time, filmScreening.price));
            Console.WriteLine("Билет куплен\n");
            return basket;
        }
        public static bool PoolYesOrNo(string question,string yes,string no)
        {
            Console.WriteLine("{0}? ({1}/{2})",question,yes,no);
            while (true)
            {
                string answer = Console.ReadLine()!;
                if (answer!.ToLower() == yes)
                    return true;
                else if (answer.ToLower() == no)
                    return false;
                else
                    DisplayMessageIncorrectInput();
            }
        }
        public static void OutputTicket(Ticket ticket)
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
        public static int ChoiseActionReturn()
        {
            while(true) 
            {
                string? input = Console.ReadLine();
                if (IsIndexCorrect(input, out uint scriptNumber))
                    switch (scriptNumber)
                    {
                        case 1:
                            return 1;
                        case 2:
                            return 2;
                        case 3:
                            return 3;
                        default:
                            DisplayMessageIncorrectInput();
                            break;
                    }
                else DisplayMessageIncorrectInput();
            }
        }

        public static bool IsIndexCorrect(string? input, out uint scriptNumber)
        {
            return uint.TryParse(input, out scriptNumber);
        }
        public static void SaveTickets(List<Ticket> basket, string path)//14 // Саша
        {
            var options = new JsonSerializerOptions { WriteIndented = true }; // опция для развертывания json файла
            string jsonString = JsonSerializer.Serialize(basket, options); // список в строку
            jsonString = Regex.Replace(jsonString, @"\\u([0-9A-Fa-f]{4})", m => "" + (char)Convert.ToInt32(m.Groups[1].Value, 16)); // меняем кодировку 
            File.WriteAllText(path, jsonString); // запись в файл .json
        }
        public static void OutputCheck(List<Ticket> basket)
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