using System.Data;
using System.Text.Json;
using File = System.IO.File;
using ConsoleApp2.Models;
using ConsoleApp2.Views;

namespace ConsoleApp2.ViewModels
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
                    FilmsPoster filmsPoster = new(films);
                    BuyTickets(filmScreening, filmsPoster);
                    Basket.Save(basketJsonPath);
                }
                Console.WriteLine("\nСпасибо за покупку, приходите ещё");
                Console.ReadKey();
            }
            else
            {
                ConsoleMessages.MessageIncorrectInput();
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
                return args.All(x => x.Contains(".json"));
            }
            return false;
        }
        public static Basket BuyTickets(Dictionary<string, List<FilmScreening>> filmScreenings, FilmsPoster filmsPoster)
        {
            Basket basket = new();
            bool flagBuyTickets = true;
            while (flagBuyTickets)
            {
                // Выбор фильма
                List<FilmScreening> filmScreeningsInOneFilm = FilmScreeningSchedule.ChooseFilmScreeingInCertainFilm(filmScreenings, filmsPoster);
                // Выбор даты
                List<FilmScreening> filmScreeningsInCertainDate = FilmScreeningSchedule.ChooseFilmScreeingInCertainDate(filmScreeningsInOneFilm);
                // Выбор времени
                FilmScreening filmScreeningInCertainTime = FilmScreening.ChooseFilmScreeningsInCertainTime(filmScreeningsInCertainDate);

                if (FilmScreening.IsPlacesNotEmpty(filmScreeningInCertainTime))
                {
                    FilmScreening.OutputCountPlace(filmScreeningInCertainTime);
                    if (FilmScreening.PoolYesOrNo("Купить билет"))
                    {
                        Basket.AddTicket(filmScreeningInCertainTime);
                        ConsoleMessages.MessageTicketPurchased();
                    }
                    ConsoleMessages.MessageCheck();
                    flagBuyTickets = !FilmScreening.PoolYesOrNo("Закончить");
                }
            }
            return basket;
        }
    }
}