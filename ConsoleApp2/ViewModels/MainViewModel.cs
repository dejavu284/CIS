using CIS.Data;
using CIS.Models;
using CIS.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            do
            {
                //выбор кинотеатра
                Cinema cinema = ChoiseCinema(cinemas);

                //ConsoleMessages.OutputSeatings(cinema.Schedule.Shows[0].Seating.Places);

                // Выбор фильма
                Schedule showsInOneFilm = ChooseShowInCertainFilm(cinema.Schedule, cinema.Poster);
                // Выбор даты
                Schedule showsInOneDate = ChooseShowInCertainDate(showsInOneFilm);
                // Выбор времени
                Show showInCertainTime = ChooseShowsInCertainTime(showsInOneDate);
                // Выбор места
                List<Place> places = ChoosePlaces(showInCertainTime);

                if (ConsoleMessages.PoolYesOrNo("Купить билет"))
                {
                    basket.AddTicket(showInCertainTime, places);
                    ConsoleMessages.MessageTicketPurchased();
                }
                ConsoleMessages.MessageCheck(basket);

            } while (!ConsoleMessages.PoolYesOrNo("Закончить"));
            return basket;
        }
        public static List<Place> ChoosePlaces(Show showInCertainTime)
        {
            List<Place> places = new();
            do
            {
                Place place = ChoosePlace(showInCertainTime);
                places.Add(place);
            } while(ConsoleMessages.PoolYesOrNo("Хотите забранировать еще одно место"));
            return places;
        }
        public static Place ChoosePlace(Show showInCertainTime)
        {
            ConsoleMessages.OutputSeatings(showInCertainTime.Seating.Places);
            int row;
            int col;
            do
            {
                ConsoleMessages.MessageBookingRequeast();
                row = ConsoleMessages.ChooseRow(showInCertainTime);
                col = ConsoleMessages.ChooseCol(showInCertainTime, row);
                ConsoleMessages.MessagePlaceCost(showInCertainTime, row, col);
            } while (!ConsoleMessages.PoolYesOrNo("Забранировать выбранное место"));
            int price = showInCertainTime.Seating.Places[row][col];
            Place place = new(row, col, price);
            showInCertainTime.Seating.Places[row][col] = -1;
            return place;
        }
            public static Cinema ChoiseCinema(List<Cinema> cinemas)
        {
            Cinema cinema;
            do
            {
                ConsoleMessages.OutputCinemas(cinemas);
                cinema = ConsoleMessages.ChooseEl(cinemas, "кинотеатра");
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
                film = ConsoleMessages.ChooseEl(filmsPoster.Films, "показа");

                scheduleWithOneFilm = schedule.FindByName(film.Name); 
                if (scheduleWithOneFilm.IsNull()) 
                    ConsoleMessages.MessageFilmNotExist();
                else
                    ConsoleMessages.MessageInfo(film);
            }
            while (scheduleWithOneFilm.IsNull());
            return scheduleWithOneFilm;
        }
        public static Schedule ChooseShowInCertainDate(Schedule shows)
        {
            DateOnly certainDateShow;
            do
            {
                ConsoleMessages.OutputDateShow(shows.Dates);
                certainDateShow = ConsoleMessages.ChooseEl(shows.Dates, "даты");
            } while (!ConsoleMessages.PoolYesOrNo("Оставить выбранную дату"));
            shows.FindShowByDate(certainDateShow);
            return shows;
        }
        public static Show ChooseShowsInCertainTime(Schedule shows)
        {
            Show showInCertainTime;
            do
            {
                ConsoleMessages.OutputTimeShow(shows.Shows);
                showInCertainTime = ConsoleMessages.ChooseEl(shows.Shows, "времени");
            } while (!ConsoleMessages.PoolYesOrNo("Оставить выбранное время"));
            return showInCertainTime;
        }
    }
}
