
using CIS.Models;
using CIS.Views;
using CIS.Data;

namespace CIS.ViewModels
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (WorkingData.DataIsCorrect(args))
            {
                string currentDirectory = $"{Environment.CurrentDirectory}";
                string filmJsonPath = currentDirectory + "\\Data\\" + args[0];
                string filmScreeningJsonPath = currentDirectory + "\\Data\\" + args[1];
                string basketJsonPath = currentDirectory + "\\Data\\" + args[2];

                List<Film> films = new();
                List<FilmScreening> filmScreening = new();

                if (WorkingData.TryDeserializ(filmJsonPath, ref films) && WorkingData.TryDeserializ(filmScreeningJsonPath, ref filmScreening))//перенести в класс Data
                {
                    FilmsPoster filmsPoster = new(films);
                    Basket basket = BuyTickets(filmScreening, filmsPoster);
                    WorkingData.Save(basketJsonPath, basket);//перенести в класс Data
                }
                ConsoleMessages.MessageCompletionProgram();
            }
            else
            {
                ConsoleMessages.MessageIncorrectInput();
            }
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
                    ConsoleMessages.MessageCheck(basket);
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