
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
                WorkingData data = new(args);
                List<Film> films = new();
                List<FilmScreening> filmScreenings = new();

                if (data.TryDeserializ(data.FilmJsonPath, ref films) && data.TryDeserializ(data.FilmScreeningJsonPath, ref filmScreenings))
                {
                    Poster filmsPoster = new(films);
                    Schedule schedule = new(filmScreenings);
                    Basket basket = BuyTickets(schedule, filmsPoster);
                    WorkingData.Save(data.BasketJsonPath, basket);
                    ConsoleMessages.MessageCompletionProgram();
                }
                else
                    ConsoleMessages.MessageIncorrectInput();
            }
            else
            {
                ConsoleMessages.MessageIncorrectInput();
            }
        }

        public static Basket BuyTickets(Schedule schedule, Poster filmsPoster)
        {
            Basket basket = new();
            bool flagBuyTickets = true;
            while (flagBuyTickets)
            {
                // Выбор фильма
                Schedule filmScreeningsInOneFilm = ChooseFilmScreeingInCertainFilm(schedule, filmsPoster);
                // Выбор даты
                Schedule filmScreeningsInOneDate = ChooseFilmScreeingInCertainDate(filmScreeningsInOneFilm);
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
        public static Schedule ChooseFilmScreeingInCertainFilm(Schedule schedule, Poster filmsPoster)
        {
            Schedule scheduleWithOneFilm;
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
        public static Schedule ChooseFilmScreeingInCertainDate(Schedule filmScreenings)
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
        public static FilmScreening ChooseFilmScreeningsInCertainTime(Schedule filmScreenings)
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