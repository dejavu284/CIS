using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp2.Views;

namespace ConsoleApp2.Models
{
    internal class FilmScreeningSchedule
    {
        public static List<FilmScreening> ChooseFilmScreeingInCertainFilm(Dictionary<string, List<FilmScreening>> filmScreening, FilmsPoster filmsPoster)
        {
            List<FilmScreening> filmScreeningsInOneFilm;
            do
            {
                filmsPoster.MessageNamesAllFilms();
                Film film = Film.ChooseFilm(filmsPoster.Films);

                filmScreeningsInOneFilm = FindFilmScriningsByName(film.Name, filmScreening);
                if (IsFilmScreeningsNotNull(filmScreeningsInOneFilm))
                    film.MessageInfo();
                else
                    Console.WriteLine("К сожалению, фильм не идет в кинотеатре\nВыберете другой фильм\n");
            }
            while (!IsFilmScreeningsNotNull(filmScreeningsInOneFilm));

            return filmScreeningsInOneFilm;
        }
        public static List<FilmScreening> FindFilmScriningsByName(string filmName, Dictionary<string, List<FilmScreening>> filmScreenings)
        {
            List<FilmScreening> thisFilmScrinings = new();
            foreach (KeyValuePair<string, List<FilmScreening>> filmScreening in filmScreenings)
            {
                if (filmName == filmScreening.Key)
                {
                    thisFilmScrinings = filmScreening.Value;
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
                OutputDateFilmScreening(datesFilmScreenings);
                DateOnly certainDataFilmSreening = ChoiseDateFilmScreening(datesFilmScreenings);
                filmScreeningsInCertainDay = FindFilmScreeningByDate(certainDataFilmSreening, filmScreeningsInOneFilm);
                flagChooseDate = !FilmScreening.PoolYesOrNo("Оставить выбранную дату");
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
        public static bool IsDatesRepeating(List<DateOnly> datesFilmScreenings, FilmScreening filmScreening)
        {
            foreach (DateOnly data in datesFilmScreenings)
            {
                if (IsDatesEqual(data, filmScreening))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsDatesEqual(DateOnly data, FilmScreening filmScreening)
        {
            return data == filmScreening.Date;
        }
        public static void OutputDateFilmScreening(List<DateOnly> datesFilmScreenings)
        {
            Console.WriteLine("Даты показа фильма:\n");
            for (int i = 0; i < datesFilmScreenings.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, datesFilmScreenings[i]);
            }
            Console.WriteLine();
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
