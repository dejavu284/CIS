using ConsoleApp2.Classes;
using System.Data;
using System.Text.Json;
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
                    FilmsPoster filmsPoster = new (films);
                    Basket basket = BuyTickets(filmScreening, filmsPoster);
                    basket.Save(basketJsonPath);
                }
                Console.WriteLine("\nСпасибо за покупку, приходите ещё");
                Console.ReadKey();
            }
            else
            {
                MessageIncorrectInput();
            }
        }
        public static bool TryDeserializ<T>(string path, ref T element)//вынести текст ошибок в свой класс ошибок
            where T : new()
        {
            try
            {
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
        //возможно метод класса Basket (конструктор класса)
        public static Basket BuyTickets(Dictionary<string, List<FilmScreening>> filmScreening, FilmsPoster filmsPoster)
        {
            Basket basket = new();
            bool flagBuyTickets = true;
            while (flagBuyTickets)
            {
                // Выбор фильма
                List<FilmScreening> filmScreeningsInOneFilm = ChooseFilmScreeingInCertainFilm(filmScreening, filmsPoster);
                // Выбор даты
                List<FilmScreening> filmScreeningsInCertainDate = ChooseFilmScreeingInCertainDate(filmScreeningsInOneFilm);
                // Выбор времени
                FilmScreening filmScreeningInCertainTime = ChooseFilmScreeningsInCertainTime(filmScreeningsInCertainDate);

                if (IsPlacesNotEmpty(filmScreeningInCertainTime))
                {
                    OutputCountPlace(filmScreeningInCertainTime);
                    if (PoolYesOrNo("Купить билет","y","n"))
                    {
                        basket.AddTicket(filmScreeningInCertainTime);
                    }
                    basket.MessageCheck();
                    flagBuyTickets = !PoolYesOrNo("Закончить", "y", "n");
                }
            }
            return new Basket();
        }
        public static FilmScreening ChooseFilmScreeningsInCertainTime(List<FilmScreening> filmScreeningsInCertainDate)
        {
            FilmScreening filmScreeningsInCertainTime;
            bool flagChooseTime = true;
            do
            {
                OutputTimeFilmScreening(filmScreeningsInCertainDate);
                filmScreeningsInCertainTime = ChoiseTimeFilmScreening(filmScreeningsInCertainDate);
                if (!IsPlacesNotEmpty(filmScreeningsInCertainTime))
                {
                    Console.WriteLine("На выбранное время мест нет.\n");
                    throw new InvalidExpressionException();// сделать свой эксепшен
                }
                flagChooseTime = PoolYesOrNo("Выбрать другое время", "y", "n");
            } while (flagChooseTime);
            return filmScreeningsInCertainTime;
        }
        public static List<FilmScreening> ChooseFilmScreeingInCertainDate(List<FilmScreening> filmScreeningsInOneFilm)
        {
            bool flagChooseDate = true;
            List < FilmScreening > filmScreeningsInCertainDay = new();
            while (flagChooseDate)
            {
                List<DateOnly>  datesFilmScreenings = FindDatesFilmScreenings(filmScreeningsInOneFilm);
                OutputDataFilmScreening(datesFilmScreenings);
                DateOnly certainDataFilmSreening = ChoiseDataFilmScreening(datesFilmScreenings);
                filmScreeningsInCertainDay = FindFilmScreeningByData(certainDataFilmSreening, filmScreeningsInOneFilm);
                flagChooseDate = PoolYesOrNo("Выбрать другую дату", "y", "n");
            }
            return filmScreeningsInCertainDay;
        }
        public static DateOnly ChoiseDataFilmScreening(List<DateOnly> allDateFilmScreenings)//??
        {
            /*MessageToSelectItemEnterNumber(); 

            string? inputIndex = Console.ReadLine();
            DateOnly dateFilmScreenings = FindElByIndex(allDateFilmScreenings, inputIndex);
            return dateFilmScreenings;*/
            return ChooseEl(allDateFilmScreenings);
        }
        public static FilmScreening ChoiseTimeFilmScreening(List<FilmScreening> filmScreeningInCertainDay)//??
        {
            /*MessageToSelectItemEnterNumber();

            string? inputIndex = Console.ReadLine();
            FilmScreening filmScreeningInCertainTime = FindElByIndex(filmScreeningInCertainDay, inputIndex);
            return filmScreeningInCertainTime;*/
            return ChooseEl(filmScreeningInCertainDay);
        }
        public static Film ChooseFilm(List<Film> films)//повтор методов вопрос что делать
        {
            return ChooseEl(films);
        }
        public static T ChooseEl<T>(List<T> elements)//повтор методов вопрос что делать
        {
            MessageToSelectItemEnterNumber();
            T el;
            do
            {
                string? inputNumber = Console.ReadLine();
                el = FindElByIndex(elements, inputNumber);
            } while (el == null || el.Equals(default(T)));
            return el;
        }
        public static List<FilmScreening> ChooseFilmScreeingInCertainFilm(Dictionary<string, List<FilmScreening>> filmScreening, FilmsPoster filmsPoster)
        {
            List<FilmScreening> filmScreeningsInOneFilm = new();
            do
            {
                filmsPoster.MessageNamesAllFilms();
                Film film = ChooseFilm(filmsPoster.Films);

                filmScreeningsInOneFilm = FindFilmScriningsByName(film.Name, filmScreening);
                if (IsFilmScreeningNotNull(filmScreeningsInOneFilm))
                    film.MessageInfo();
                else
                    Console.WriteLine("К сожалению, фильм не идет в кинотеатре\nВыберете другой фильм\n");
            }
            while (!IsFilmScreeningNotNull(filmScreeningsInOneFilm));

            return filmScreeningsInOneFilm;
        }
        public static T FindElByIndex<T>(List<T> list, string? indexStr)
        {
            int index;
            if (IsNumberInList(list, indexStr, out index))
            {
                return list[index - 1];
            }
            else
            {
                MessageIncorrectInput();
                return default(T);
               // throw new ArgumentException("Выбранного елемента нет в списке");
            }
            
        }
        public static bool IsNumberInList<T>(List<T> films, string? indexStr, out int index)
        {
            bool tryParseChecked = int.TryParse(indexStr, out index);
            return tryParseChecked && films.Count >= index;
        }
        public static void MessageIncorrectInput()
        {
            Console.WriteLine("\nНекорректный ввод поробуйте ещё раз");
        }
        public static void MessageToSelectItemEnterNumber()
        {
            Console.WriteLine("\nДля выбора элемента введите его номер");
            Console.WriteLine();
        }
        public static List<FilmScreening> FindFilmScriningsByName(string filmName, Dictionary<string, List<FilmScreening>> filmScreenings)
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
        public static List<DateOnly> FindDatesFilmScreenings(List<FilmScreening> filmScreenings)
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
            foreach (DateOnly data in datesFilmScreenings)
            {
                if (IsDatesEqual(data, filmScreening))
                {
                    return true;
                }
            }
            return false;
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
        public static void OutputCountPlace(FilmScreening filmScreening)
        {
            Console.WriteLine("Количесво оставшихся мест на сеанс: {0}\n", filmScreening.countTiket);
        }
        public static bool IsPlacesNotEmpty(FilmScreening filmScreening)
        {
            return (filmScreening.countTiket != 0);
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
                    MessageIncorrectInput();
            }
        }
    }
}