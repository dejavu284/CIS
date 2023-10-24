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
        public static bool DataIsCorrect(string[] args)
        {
            if (args.Length == 3)
            {
                return args.All(x => x.Contains(".json"));
            }
            return false;
        }
        public static Basket BuyTickets(List<FilmScreening> filmScreenings, FilmsPoster filmsPoster)
        {
            Basket basket = new();
            bool flagBuyTickets = true;
            while (flagBuyTickets)
            {
                // Выбор фильма
                FilmScreeningSchedule filmScreeningsInOneFilm = new();
                filmScreeningsInOneFilm.ChooseFilmScreeingInCertainFilm(filmScreenings, filmsPoster);
                // Выбор даты
                filmScreeningsInOneFilm.ChooseFilmScreeingInCertainDate();
                // Выбор времени
                FilmScreening filmScreeningInCertainTime = filmScreeningsInOneFilm.ChooseFilmScreeningsInCertainTime();

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
    }
}