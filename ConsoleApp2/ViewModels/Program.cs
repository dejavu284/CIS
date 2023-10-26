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

                if (TryDeserializ(filmJsonPath, ref films) && TryDeserializ(filmScreeningJsonPath, ref filmScreening))//перенести в класс Data
                {
                    FilmsPoster filmsPoster = new(films);
                    Basket basket = BuyTickets(filmScreening, filmsPoster);
                    basket.Save(basketJsonPath);//перенести в класс Data
                }
                ConsoleMessages.MessageCompletionProgram();
            }
            else
            {
                ConsoleMessages.MessageIncorrectInput();
            }
        }
        public static bool TryDeserializ<T>(string path, ref T element) //перенести в класс Data
            where T : new()
        {
            try
            {
                string textJson = File.ReadAllText(path);
                element = JsonSerializer.Deserialize<T>(textJson)!;
                //var a = new T(); // WTF
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
            FilmScreeningSchedule schedule = new(filmScreenings);
            FilmScreeningSchedule scheduleWithOneFilm;
            Film film;
            do
            {
                ConsoleMessages.MessageNamesAllFilms(filmsPoster); 
                film = ConsoleMessages.ChooseEl(filmsPoster.Films);

                scheduleWithOneFilm = schedule.FindByName(film.Name); // Метод бизнес логики
                if (scheduleWithOneFilm.IsNull()) // Метод бизнес логики
                    ConsoleMessages.MessageFilmNotExist();
                else
                    ConsoleMessages.MessageInfo(film);
            }
            while (scheduleWithOneFilm.IsNull()); // Метод бизнес логики
            return scheduleWithOneFilm;
        }
        public static FilmScreeningSchedule ChooseFilmScreeingInCertainDate(FilmScreeningSchedule filmScreenings)
        {
            DateOnly certainDataFilmSreening = new();
            do
            {
                ConsoleMessages.OutputDateFilmScreening(filmScreenings.Dates);
                certainDataFilmSreening = ConsoleMessages.ChooseEl(filmScreenings.Dates); // Метод бизнес логики
            } while (!ConsoleMessages.PoolYesOrNo("Оставить выбранную дату"));
            filmScreenings.FindFilmScreeningByDate(certainDataFilmSreening); // Метод бизнес логики
            return filmScreenings;
        }
        public static FilmScreening ChooseFilmScreeningsInCertainTime(FilmScreeningSchedule filmScreenings)
        {
            FilmScreening filmScreeningInCertainTime;
            do
            {
                ConsoleMessages.OutputTimeFilmScreening(filmScreenings.FilmScreenings); 
                filmScreeningInCertainTime = ConsoleMessages.ChooseEl(filmScreenings.FilmScreenings); // Метод бизнес логики
            } while (!ConsoleMessages.PoolYesOrNo("Оставить выбранное время"));
            return filmScreeningInCertainTime;
        }
    }
}