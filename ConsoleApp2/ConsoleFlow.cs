﻿using CinemaModel;
using View;

namespace MainFlow
{
    internal class ConsoleFlow
    {
        public static Basket BuyTickets(List<Cinema> cinemas)
        {
            Basket basket = new();
            do
            {
                //выбор кинотеатра
                Cinema cinema = ChoiseCinema(cinemas);
                // Выбор фильма
                Schedule showsInOneFilm = ChooseShowInCertainFilm(cinema.Schedule, cinema.Poster);
                // Выбор даты
                Schedule showsInOneDate = ChooseShowInCertainDate(showsInOneFilm);
                // Выбор времени
                Show showInCertainTime = ChooseShowsInCertainTime(showsInOneDate);

                Hall hallInCertainShow = ChooseHallInCertainShow(showInCertainTime, cinema.Halls);
                
                // Выбор места
                List <Place> places = ChoosePlaces(showInCertainTime,hallInCertainShow );

                if (ConsoleMessages.PoolYesOrNo("Купить билет"))
                {
                    for (int i = 0; i < places.Count; i++)
                    {
                        Ticket ticket = new Ticket(cinema.Id,cinema.Name, showInCertainTime, places[i]);
                        basket.AddTicket(ticket);
                    }
                    ConsoleMessages.MessageTicketPurchased();
                }
                ConsoleMessages.MessageBacketInfo(basket);

            } while (!ConsoleMessages.PoolYesOrNo("Закончить"));
            return basket;
        }

        private static Hall ChooseHallInCertainShow(Show showInCertainTime, List<Hall> halls)
        {
            foreach (Hall hall in halls)
            {
                if (hall.Id == showInCertainTime.Seating.IdHall)
                    return hall;
            }
            throw new Exception("Не удалось найти зал кинотеатра у выбранного показа");
        }

        public static List<Place> ChoosePlaces(Show showInCertainTime,Hall hall)
        {
            List<Place> places = new();
            do
            {
                Place place = ChoosePlace(showInCertainTime,hall);
                places.Add(place);
            } while (ConsoleMessages.PoolYesOrNo("Хотите забранировать еще одно место"));
            return places;
        }
        public static Place ChoosePlace(Show showInCertainTime, Hall hall)
        {
            ConsoleMessages.OutputSeatings(showInCertainTime.Seating.Places);
            int indexRow;
            int indexColum;
            do
            {
                ConsoleMessages.MessageBookingRequeast();
                indexRow = ConsoleMessages.ChooseIndexRow(showInCertainTime);
                indexColum = ConsoleMessages.ChooseIndexColum(showInCertainTime, indexRow);
                ConsoleMessages.MessagePlaceCost(showInCertainTime, indexRow, indexColum);
            } while (!ConsoleMessages.PoolYesOrNo("Забранировать выбранное место"));
            int price = showInCertainTime.Seating.Places[indexRow][indexColum];
            Place place = new Place(indexRow, indexColum,hall.Layout[indexRow][indexColum], price);
            showInCertainTime.BookingPlaces(place);
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
                if (scheduleWithOneFilm.IsEmpty())
                    ConsoleMessages.MessageFilmNotExist();
                else
                    ConsoleMessages.MessageFilmInfo(film);
            }
            while (scheduleWithOneFilm.IsEmpty());
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
            shows.FindByDate(certainDateShow);
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
