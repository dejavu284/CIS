using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIS.Views;

namespace CIS.Models
{
    internal class FilmScreeningSchedule
    {
        public List<DateOnly> DatesOfFilmScreenings { get; private set; }
        public List<FilmScreening> FilmScreenings { get; private set; }
        FilmScreeningSchedule(List<FilmScreening> filmScreenings) 
        {
            this.FilmScreenings = filmScreenings;
        }
        public static List<FilmScreening> ChooseFilmScreeingInCertainFilm(List<FilmScreening> filmScreenings, FilmsPoster filmsPoster)
        {
            List<FilmScreening> filmScreeningsInOneFilm;
            do
            {
                filmsPoster.MessageNamesAllFilms(); // перенести в ConsoleMessages
                Film film = Film.ChooseFilm(filmsPoster.Films);

                filmScreeningsInOneFilm = FindFilmScriningsByName(film.Name, filmScreenings);
                if (IsFilmScreeningsNotNull(filmScreeningsInOneFilm))
                    film.MessageInfo();
                else
                    ConsoleMessages.MessageFilmNotExist();
            }
            while (!IsFilmScreeningsNotNull(filmScreeningsInOneFilm));

            return filmScreeningsInOneFilm;
        }
        public static List<FilmScreening> FindFilmScriningsByName(string filmName, List<FilmScreening> filmScreenings)
        {
            List<FilmScreening> thisFilmScrinings = new();
            foreach (FilmScreening filmScreening in filmScreenings)
            {
                if (filmName == filmScreening.Name)
                {
                    thisFilmScrinings.Add(filmScreening);
                }
            }
            return thisFilmScrinings;
        }
        public static bool IsFilmScreeningsNotNull(List<FilmScreening> filmScreenings)
        {
            return filmScreenings.Count != 0;
        }

        public static List<FilmScreening> ChooseFilmScreeingInCertainDate(List<FilmScreening> filmScreeningsInOneFilm)
        {
            bool flagChooseDate = true;
            List<FilmScreening> filmScreeningsInCertainDay = new();
            while (flagChooseDate)
            {
                List<DateOnly> datesFilmScreenings = FindDatesFilmScreenings(filmScreeningsInOneFilm);
                ConsoleMessages.OutputDateFilmScreening(datesFilmScreenings);
                DateOnly certainDataFilmSreening = ChoiseDateFilmScreening(datesFilmScreenings);
                filmScreeningsInCertainDay = FindFilmScreeningByDate(certainDataFilmSreening, filmScreeningsInOneFilm);
                flagChooseDate = !ConsoleMessages.PoolYesOrNo("Оставить выбранную дату");
            }
            return filmScreeningsInCertainDay;
        }
        public static List<DateOnly> FindDatesFilmScreenings(List<FilmScreening> filmScreenings)
        {
            List<DateOnly> datesFilmScreenings = new();
            foreach (FilmScreening filmScreening in filmScreenings)
            {
                if (!IsDatesRepeating(datesFilmScreenings, filmScreening))
                {
                    datesFilmScreenings.Add(filmScreening.Date);
                }
            }
            return datesFilmScreenings;
        }
        public static bool IsDatesRepeating(List<DateOnly> datesFilmScreenings, FilmScreening filmScreening) // возможно стоит переписать в 4 строчки: return IsDatesEqual(...)
        {
            foreach (DateOnly data in datesFilmScreenings)
            {
                return FilmScreening.IsDatesEqual(data, filmScreening);
            }
            return false;
        }
        public static DateOnly ChoiseDateFilmScreening(List<DateOnly> allDateFilmScreenings)//??
        {
            return ConsoleMessages.ChooseEl(allDateFilmScreenings);
        }
        public static List<FilmScreening> FindFilmScreeningByDate(DateOnly datesFilmScreenings, List<FilmScreening> allFilmScreenings)
        {
            List<FilmScreening> filmScreeningsTemp = new List<FilmScreening>();
            for (int i = 0; i < allFilmScreenings.Count; i++)
            {
                if (datesFilmScreenings == allFilmScreenings[i].Date)
                {
                    filmScreeningsTemp.Add(allFilmScreenings[i]);
                }
            }
            return filmScreeningsTemp;
        }

    }
}
