using System.Data;
using System.Text.Json;
using File = System.IO.File;
using CIS.Models;
using CIS.Views;

namespace CIS.ViewModels
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
                List<FilmScreening> filmScreening = new();

                if (TryDeserializ(filmJsonPath, ref films) && TryDeserializ(filmScreeningJsonPath, ref filmScreening))
                {
                    FilmsPoster filmsPoster = new(films);
                    Basket basket = BuyTickets(filmScreening, filmsPoster);
                    basket.Save(basketJsonPath);
                }
                ConsoleMessages.MessageCompletionProgram();
            }
            else
            {
                ConsoleMessages.MessageIncorrectInput();
            }
        }
        public static bool TryDeserializ<T>(string path, ref T element) // нужно вынести текст ошибок в свой класс ошибок
                                                                        // Возможно нужно перенести этот метод в класс Data в папке Data
            where T : new()
        {
            try
            {
                string textJson = File.ReadAllText(path);
                element = JsonSerializer.Deserialize<T>(textJson)!;
                var a = new T(); // WTF
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
        public static bool DataIsCorrect(string[] args) // Возможно нужно перенести этот метод в класс Data в папке Data
        {
            if (args.Length == 3)
            {
                return args.All(x => x.Contains(".json"));
            }
            return false;
        }
        public static Basket BuyTickets(List<FilmScreening> filmScreenings, FilmsPoster filmsPoster) // Нужно избавиться от list<filmscreeening> у меня не получилось(
        {
            Basket basket = new();
            bool flagBuyTickets = true;
            while (flagBuyTickets)
            {
                // Выбор фильма
                FilmScreeningSchedule filmScreeningsInOneFilm = ChooseFilmScreeingInCertainFilm(filmScreenings, filmsPoster);
                // Выбор даты
                FilmScreeningSchedule filmScreeningsInOneDate = ChooseFilmScreeingInCertainDate(filmScreeningsInOneFilm);
                // Выбор времени
                FilmScreening filmScreeningInCertainTime = ChooseFilmScreeningsInCertainTime(filmScreeningsInOneDate);

                if (filmScreeningInCertainTime.IsPlacesNotEmpty())
                {
                    ConsoleMessages.OutputCountPlace(filmScreeningInCertainTime);
                    if (ConsoleMessages.PoolYesOrNo("Купить билет"))
                    {
                        basket.AddTicket(filmScreeningInCertainTime);
                        ConsoleMessages.MessageTicketPurchased();
                    }
                    ConsoleMessages.MessageCheck();
                    flagBuyTickets = !ConsoleMessages.PoolYesOrNo("Закончить");
                }
                else
                {
                    ConsoleMessages.MessagePlaceNotExist();
                }
            }
            return basket;
        }
        public static FilmScreeningSchedule ChooseFilmScreeingInCertainFilm(List<FilmScreening> filmScreenings, FilmsPoster filmsPoster)
        {
            FilmScreeningSchedule FilmScreenings = new();
            do
            {
                ConsoleMessages.MessageNamesAllFilms(filmsPoster); 
                Film film = FilmsPoster.ChooseFilm(filmsPoster.Films); 

                FilmScreenings.FindFilmScriningsByName(film.Name, filmScreenings); // Метод бизнес логики
                if (FilmScreenings.IsFilmScreeningsNotNull()) // Метод бизнес логики
                    ConsoleMessages.MessageInfo(film); 
                else
                    ConsoleMessages.MessageFilmNotExist(); 
            }
            while (!FilmScreenings.IsFilmScreeningsNotNull()); // Метод бизнес логики
            return FilmScreenings;
        }
        public static FilmScreeningSchedule ChooseFilmScreeingInCertainDate(FilmScreeningSchedule filmScreenings)
        {
            bool flagChooseDate = true;
            DateOnly certainDataFilmSreening = new();
            while (flagChooseDate)
            {
                filmScreenings.DatesOfFilmScreenings = new();
                filmScreenings.FindDatesFilmScreenings(); // Метод бизнес логики
                ConsoleMessages.OutputDateFilmScreening(filmScreenings.DatesOfFilmScreenings); 

                certainDataFilmSreening = filmScreenings.ChoiseDateFilmScreening(); // Метод бизнес логики
                flagChooseDate = !ConsoleMessages.PoolYesOrNo("Оставить выбранную дату"); 
            }
            filmScreenings.FindFilmScreeningByDate(certainDataFilmSreening); // Метод бизнес логики
            return filmScreenings;
        }
        public static FilmScreening ChooseFilmScreeningsInCertainTime(FilmScreeningSchedule filmScreenings)
        {
            FilmScreening filmScreeningInCertainTime;
            bool flagChooseTime;
            do
            {
                ConsoleMessages.OutputTimeFilmScreening(filmScreenings.FilmScreenings); 
                filmScreeningInCertainTime = filmScreenings.ChoiseFilmScreeningByTime(); // Метод бизнес логики
                flagChooseTime = !ConsoleMessages.PoolYesOrNo("Оставить выбранное время"); 
            } while (flagChooseTime);
            return filmScreeningInCertainTime;
        }
    }
}