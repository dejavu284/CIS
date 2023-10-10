using ConsoleApp2.Classes;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.RegularExpressions;
using File = System.IO.File;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (CheckDataIsCorrect(args))
            {
                string currentDirectory = $"{Environment.CurrentDirectory}";
                string filmJsonPath = currentDirectory + "\\Data\\" + args[0];
                string filmScreeningJsonPath = currentDirectory + "\\Data\\" + args[1];
                string basketJsonPath = currentDirectory + "\\Data\\" + args[2];

                List<Film> films = new ();
                Dictionary<string, List<Film_screening>> filmScreening = new();

                if (TryDeserializ(filmJsonPath, ref films) && TryDeserializ(filmScreeningJsonPath, ref filmScreening))
                {
                    List<Ticket> basket = RunScript(filmScreening, films);
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
        public static bool CheckDataIsCorrect(string[] args)
        {
            bool flag = false;
            if (args.Length == 3)
            {
                foreach (var nameFile in args)
                {
                    if (nameFile.Contains(".json"))
                    {
                        flag = true; 
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return flag;
        }
        public static List<Ticket> RunScript(Dictionary<string, List<Film_screening>> filmScreening, List<Film> films)
        {
            List<Ticket> basket = new List<Ticket>();
            int numberAction = 0;
            do
            {
                // выбор фильма 
                OutputFilms(films);
                Film film = ChoiceFilm(films);
                OutputInfoFilm(film);
                List<Film_screening> filmScreeningsInOneFilm = FindThisFilmScrinings(film, filmScreening);
                List<DateOnly> datesFilmScreenings = FindDataFilmScreening(filmScreeningsInOneFilm);
                do
                {
                    // проверка на наличие показов фильма
                    if (CheckDateFilmScreeningNotEmpty(datesFilmScreenings))
                        OutputDataFilmScreening(datesFilmScreenings);
                    else
                    {
                        Console.WriteLine("К сожелению у фильма нет показов. Пожалуйста выбирите другой фильм\n");
                        numberAction = 1;
                        break;
                    }
                    // выбор даты 
                    DateOnly dataFilmSreening = ChoiseDataFilmScreening(datesFilmScreenings);
                    List<Film_screening> filmScreeningsInOneDay = FindFilmScreeningByData(dataFilmSreening, filmScreeningsInOneFilm);
                    do
                    {
                        //выбор времени 
                        OutputTimeFilmScreening(filmScreeningsInOneDay);
                        Film_screening filmScreeningsInOneTime = ChoiseTimeFilmScreening(filmScreeningsInOneDay);
                        if (CheckPlaseNotEmpty(filmScreeningsInOneTime))
                        {
                            OutputPlaseFilmScreening(filmScreeningsInOneTime);
                            if (PoolYesOrNo("Купить билет?(да/нет)"))
                            {
                                basket = BuyTicket(basket, filmScreeningsInOneTime, film);
                                OutputReceipt(basket);
                                break;
                            }
                            else
                            {
                                OutputActionReturn();
                                numberAction = ChoiseActionReturn();
                                if (numberAction == 3)
                                    continue;
                                else
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("К сожелению места закончились.");
                            OutputActionReturn();
                            numberAction = ChoiseActionReturn();
                            if (numberAction == 3)
                                continue;
                            else
                                break;
                        }
                    } while (true);
                    if (numberAction == 2)
                        continue;
                    else
                        break;
                } while (true);
                if (numberAction == 1)
                    continue;
            } 
            while (PoolYesOrNo("Купить ещё билет?(да/нет)"));
            return basket;
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
                else
                {
                    DisplayMessageIncorrectInput();
                }
            }
            Console.WriteLine();
            return film!;
        }
        public static bool CheckingNumberEntryInList<T>(List<T> films, string? index_str)
        {
            bool tryParseCheced = uint.TryParse(index_str, out uint index);
            return tryParseCheced && films.Count >= index;
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
        }
        public static List<Film_screening> FindThisFilmScrinings(Film film, Dictionary<string, List<Film_screening>> filmScreenings) // 2 // Саша
        {
            List<Film_screening> thisFilmScrinings = new ();
            foreach (KeyValuePair<string, List<Film_screening>> filmScreening in filmScreenings)
            {
                if (film.name == filmScreening.Key)
                {
                    thisFilmScrinings = filmScreening.Value;
                }
            }
            return thisFilmScrinings;
        }
        public static List<DateOnly> FindDataFilmScreening(List<Film_screening> filmScreenings) // 3 // Саша
        {
            List<DateOnly> datesFilmScreenings = new ();
            foreach (Film_screening filmScreening in filmScreenings)
            {
                if (!CheckRepeatDates(datesFilmScreenings, filmScreening))
                {
                    datesFilmScreenings.Add(filmScreening.data);
                }
            }
            return datesFilmScreenings;
        }
        public static bool CheckRepeatDates(List<DateOnly> datesFilmScreenings, Film_screening filmScreening)
        {
            bool repeat = false;
            foreach (DateOnly data in datesFilmScreenings)
            {
                if (CheckDatesEqual(data, filmScreening))
                {
                    repeat = true;
                    break;
                }
            }
            return repeat;
        }
        public static bool CheckDatesEqual(DateOnly data, Film_screening filmScreening)
        {
            return data == filmScreening.data;
        }
        public static void OutputDataFilmScreening(List<DateOnly> datesFilmScreenings) // 4 // Саша
        {
            Console.WriteLine("Даты показа фильма:\n"); 
            for (int i = 0; i < datesFilmScreenings.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, datesFilmScreenings[i]);
            }
            Console.WriteLine();
        }
        //сделал проверку в метод OutputDataFilmScreening не увидел что он твой(((спасибо)))
        public static bool CheckDateFilmScreeningNotEmpty(List<DateOnly> datesFilmScreenings)
        {
            return !(datesFilmScreenings.Count == 0 || datesFilmScreenings == null);
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
        public static List<Film_screening> FindFilmScreeningByData(DateOnly datesFilmScreenings, List<Film_screening> all_film_screenings) // 5 // Кирилл
        {
            List<Film_screening> filmScreeningsTemp = new List<Film_screening>();
            for (int i = 0; i < all_film_screenings.Count; i++)
                if(datesFilmScreenings == all_film_screenings[i].data)
                    filmScreeningsTemp.Add(all_film_screenings[i]);
            return filmScreeningsTemp;
        }

        public static void OutputTimeFilmScreening(List<Film_screening> filmscreenings) // 6 // Кирилл
        {
            Console.WriteLine("Время показа фильма:");
            for (int i = 0; i < filmscreenings.Count; i++)
            {
                Console.WriteLine("\n{0}. {1}. Цена: {2} руб.", i + 1, filmscreenings[i].time, filmscreenings[i].price);
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
        public static void OutputPlaseFilmScreening(Film_screening filmScreening) // 8 // Саша
        {
            Console.WriteLine("Количесво оставшихся мест на сеанс: {0}\n", filmScreening.countTiket);
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

        //метод класса Basket
        public static List<Ticket> BuyTicket(List<Ticket> basket, Film_screening filmScreening, Film film)//10 // Саша
        {
            basket.Add(new Ticket(film.name, filmScreening.data, filmScreening.time, filmScreening.price));
            Console.WriteLine("Билет куплен\n");
            return basket;
        }
        public static bool PoolYesOrNo(string question)
        {
            Console.WriteLine(question);
            while (true)
            {
                string? answer = Console.ReadLine();
                if (answer == "да")
                    return true;
                else if (answer == "нет")
                    return false;
                else
                    DisplayMessageIncorrectInput();
            }
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
       
        public static int ChoiseActionReturn()//13 //Кирилл
        {
            while(true) 
            {
                string? input = Console.ReadLine();
                if (uint.TryParse(input,out uint scriptNumber))
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
        public static void SaveTickets(List<Ticket> basket, string path)//14 // Саша
        {
            var options = new JsonSerializerOptions { WriteIndented = true }; // опция для развертывания json файла
            string jsonString = JsonSerializer.Serialize(basket, options); // список в строку
            jsonString = Regex.Replace(jsonString, @"\\u([0-9A-Fa-f]{4})", m => "" + (char)Convert.ToInt32(m.Groups[1].Value, 16)); // меняем кодировку 
            File.WriteAllText(path, jsonString); // запись в файл .json
        }

        public static void OutputReceipt(List<Ticket> basket) // Саша
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