using CIS.Models;
using CIS.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS.ViewModels
{
    internal class MainViewModel
    {
        public static Basket BuyTickets(List<Cinema> cinemas)
        {
            Basket basket = new();
            bool flagBuyTickets = true;
            while (flagBuyTickets)
            {
                Cinema cinema = ChoiseCinema(cinemas);
                // Выбор фильма
                Schedule filmScreeningsInOneFilm = ChooseFilmScreeingInCertainFilm(cinema.Schedule, cinema.Poster);
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
        public static Cinema ChoiseCinema(List<Cinema> cinemas)
        {
            Cinema cinema;
            do
            {
                ConsoleMessages.OutputCinemas(cinemas);
                cinema = ConsoleMessages.ChooseEl(cinemas);
                ConsoleMessages.MessageInfoCinema(cinema);
            } while (ConsoleMessages.PoolYesOrNo("Выбрать другой кинотеарт?"));
            return cinema;
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
