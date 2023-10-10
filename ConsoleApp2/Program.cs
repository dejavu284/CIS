using ConsoleApp2.Classes;
using System.Text.Json;
using System.Text.RegularExpressions;
using File = System.IO.File;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (IsCorrectData(args))
            {
                string currentDirectory = $"{Environment.CurrentDirectory}";
                string filmJsonPath = currentDirectory + "\\Data\\" + args[0];
                string filmScreeningJsonPath = currentDirectory + "\\Data\\" + args[1];
                string basketJsonPath = currentDirectory + "\\Data\\" + args[2];

                List<Film> films = new ();
                Dictionary<string, List<Film_screening>> filmScreening = new();

               if(TryDeserializ(filmJsonPath,ref films) && TryDeserializ(filmScreeningJsonPath, ref filmScreening))
               RunScript(filmScreening, films, basketJsonPath);
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
        public static bool IsCorrectData(string[] args)
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
        public static void RunScript(Dictionary<string, List<Film_screening>> filmScreening, List<Film> films, string basketJson)
        {
            Film film = null;
            List<Ticket> basket = new List<Ticket>();
            List<Film_screening> thisFilmScrinings = new List<Film_screening>();
            List<DateOnly> datesFilmScreenings = new List<DateOnly>();
            Film_screening thisFilmScrining = null;
            bool repeat = true;
            int scriptNumber = 0;

            while (repeat)
            {
                switch (scriptNumber)
                {
                    case 0:
                        OutputFilms(films);

                        film = ChoiceFilm(films);
                        OutputInfoFilm(film);

                        thisFilmScrinings = FindThisFilmScrinings(film, filmScreening);

                        datesFilmScreenings = FindDataFilmScreening(thisFilmScrinings);

                        scriptNumber = 4;
                        break;
                    case 4:
                        scriptNumber = OutputDataFilmScreening(datesFilmScreenings);
                        break;
                    case 5:
                        DateOnly dataFilmSreening = ChoiseDataFilmScreening(datesFilmScreenings);
                        thisFilmScrinings = FindFilmScreeningByData(dataFilmSreening, thisFilmScrinings);
                        scriptNumber = 6;
                        break;
                    case 6:
                        OutputTimeFilmScreening(thisFilmScrinings);

                        thisFilmScrining = ChoiseTimeFilmScreening(thisFilmScrinings);

                        scriptNumber = OutputPlaseFilmScreening(thisFilmScrining);
                        break;
                    case 9:
                        OutputActionReturn();
                        scriptNumber = 13;
                        break;
                    case 10:
                        scriptNumber = ProofBuyTicket(basket, thisFilmScrining, film); 
                        break;
                    case 11:
                        OutputTicket(basket[^1]); 
                        scriptNumber = PoolCotinuationBuy();
                        break;
                    case 13:
                        scriptNumber = ChoiseActionReturn();
                        break;
                    case 14:
                        SaveTickets(basket, basketJson);
                        OutputReceipt(basket);
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
                string? inputIndex = Console.ReadLine();
                if (IsNumberInList(films, inputIndex))
                {
                    film = films[int.Parse(inputIndex!) - 1];
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
        public static bool IsNumberInList<T>(List<T> films, string? indexStr)
        {
            bool tryParseChecked = uint.TryParse(indexStr, out uint index);
            return tryParseChecked && films.Count >= index;
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
                if (!IsDatesRepeating(datesFilmScreenings, filmScreening))
                {
                    datesFilmScreenings.Add(filmScreening.data);
                }
            }
            return datesFilmScreenings;
        }
        public static bool IsDatesRepeating(List<DateOnly> datesFilmScreenings, Film_screening filmScreening)
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
        public static bool IsDatesEqual(DateOnly data, Film_screening filmScreening)
        {
            return data == filmScreening.data;
        }
        public static int OutputDataFilmScreening(List<DateOnly> datesFilmScreenings) // 4 // Саша
        {
            int script;
            if (IsDateFilmScreeningExist(datesFilmScreenings))
            {
                Console.WriteLine("Даты показа фильма:\n"); 
                for (int i = 0; i < datesFilmScreenings.Count; i++)
                {
                    Console.WriteLine("{0}. {1}", i + 1, datesFilmScreenings[i]);
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
        public static bool IsDateFilmScreeningExist(List<DateOnly> datesFilmScreenings)
        {
            return !(datesFilmScreenings.Count == 0 || datesFilmScreenings == null);
        }
        public static DateOnly ChoiseDataFilmScreening(List<DateOnly> allDateFilmScreenings) // 5 // Кирилл
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
        public static List<Film_screening> FindFilmScreeningByData(DateOnly datesFilmScreenings, List<Film_screening> allFilmScreenings) // 5 // Кирилл
        {
            List<Film_screening> filmScreeningsTemp = new List<Film_screening>();
            for (int i = 0; i < allFilmScreenings.Count; i++)
            {
                if(datesFilmScreenings == allFilmScreenings[i].data)
                {
                    filmScreeningsTemp.Add(allFilmScreenings[i]);
                }
            }
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
        public static int OutputPlaseFilmScreening(Film_screening filmScreening) // 8 // Саша
        {
            int script;
            if (IsPlacesNotEmpty(filmScreening))
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
        public static bool IsPlacesNotEmpty(Film_screening filmScreening)
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
        public static int ProofBuyTicket(List<Ticket> basket, Film_screening filmScreening, Film film)//10 // Саша
        {
            Console.WriteLine("Купить билет?(да/нет)");
            bool x = true;
            int script = -1;
            while (x)
            {
                string answer = Console.ReadLine();
                if (IsAnswerPositive(answer))
                {
                    script = 11;
                    x = false;
                    basket.Add(new Ticket(film.name, filmScreening.data, filmScreening.time, filmScreening.price));
                    // добавить изменение данных(изменения кол-ва свободных мест)
                }
                else if (IsAnswerNegative(answer))
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
        public static bool IsAnswerPositive(string answer)
        {
            return answer == "да";
        }
        public static bool IsAnswerNegative(string answer)
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
                string? answer = Console.ReadLine();
                if (IsAnswerPositive(answer))
                {
                    return 0;
                }
                else if (IsAnswerNegative(answer))
                {
                    return 14;
                }
                else
                {
                    DisplayMessageIncorrectInput();
                }
            }
        }
        public static int ChoiseActionReturn()//13 //Кирилл
        {
            while(true) 
            {
                string? input = Console.ReadLine();
                if (IsIndexCorrect(input, out uint scriptNumber))
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