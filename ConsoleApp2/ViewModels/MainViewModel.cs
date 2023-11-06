using CIS.Data;
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
        public static void StartProgram(string[] args)
        {
            if (WorkingData.DataIsCorrect(args))
            {
                WorkingData data = new(args);
                List<Cinema> cinemas = new();

                if (data.TryDeserializ(data.CinemasJsonPath, ref cinemas))
                {
                    Basket basket = MainViewModel.BuyTickets(cinemas);
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
        public static Basket BuyTickets(List<Cinema> cinemas)
        {
            Basket basket = new();
            bool flagBuyTickets = true;
            while (flagBuyTickets)
            {
                Cinema cinema = ChoiseCinema(cinemas);
                // Выбор фильма
                Schedule showsInOneFilm = ChooseShowInCertainFilm(cinema.Schedule, cinema.Poster);
                // Выбор даты
                Schedule showsInOneDate = ChooseShowInCertainDate(showsInOneFilm);
                // Выбор времени
                Show showInCertainTime = ChooseShowsInCertainTime(showsInOneDate);

                if (showInCertainTime.IsPlacesNotEmpty())
                {
                    ConsoleMessages.OutputCountPlace(showInCertainTime);
                    if (ConsoleMessages.PoolYesOrNo("Купить билет"))
                    {
                        basket.AddTicket(showInCertainTime);
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
            } while (!ConsoleMessages.PoolYesOrNo("Оставить выбранный кинотеарт"));
            return cinema;
        }
        public static Schedule ChooseShowInCertainFilm(Schedule schedule, Poster filmsPoster)
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
        public static Schedule ChooseShowInCertainDate(Schedule shows)
        {
            DateOnly certainDataShow = new();
            do
            {
                ConsoleMessages.OutputDateShow(shows.Dates);
                certainDataShow = ConsoleMessages.ChooseEl(shows.Dates); // Метод бизнес логики
            } while (!ConsoleMessages.PoolYesOrNo("Оставить выбранную дату"));
            shows.FindShowByDate(certainDataShow); // Метод бизнес логики
            return shows;
        }
        public static Show ChooseShowsInCertainTime(Schedule shows)
        {
            Show showInCertainTime;
            do
            {
                ConsoleMessages.OutputTimeShow(shows.Shows);
                showInCertainTime = ConsoleMessages.ChooseEl(shows.Shows); // Метод бизнес логики
            } while (!ConsoleMessages.PoolYesOrNo("Оставить выбранное время"));
            return showInCertainTime;
        }
    }
}
