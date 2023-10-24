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
        public List<DateOnly> DatesOfFilmScreenings { get; private set; } = new(); // я напихал везде эти
                                                                                   // private set и не уверен что это вообще нужно
        public List<FilmScreening> FilmScreenings { get; private set; } = new();
        public FilmScreeningSchedule() { }
        public FilmScreeningSchedule(List<FilmScreening> filmScreenings) 
        {
            FilmScreenings = filmScreenings;
        }
        public void ChooseFilmScreeingInCertainFilm(List<FilmScreening> filmScreenings, FilmsPoster filmsPoster)
        {
            do
            {
                ConsoleMessages.MessageNamesAllFilms(filmsPoster); // перенести в ViewModels (управление)
                Film film = Film.ChooseFilm(filmsPoster.Films); // перенести в ViewModels (управление)

                FindFilmScriningsByName(film.Name, filmScreenings);
                if (IsFilmScreeningsNotNull())
                    ConsoleMessages.MessageInfo(film); // перенести в ViewModels (управление)
                else
                    ConsoleMessages.MessageFilmNotExist(); // перенести в ViewModels (управление)
            }
            while (!IsFilmScreeningsNotNull());
        }
        public static Film ChooseFilm(List<Film> films)
        {
            return ConsoleMessages.ChooseEl(films);
        }
        public void FindFilmScriningsByName(string filmName, List<FilmScreening> filmScreenings)
        {
            foreach (FilmScreening filmScreening in filmScreenings)
            {
                if (filmName == filmScreening.Name)
                {
                    FilmScreenings.Add(filmScreening);
                }
            }
        }
        private bool IsFilmScreeningsNotNull()
        {
            return FilmScreenings.Count != 0;
        }

        public void ChooseFilmScreeingInCertainDate()
        {
            bool flagChooseDate = true;
            while (flagChooseDate)
            {
                FindDatesFilmScreenings(); // перенести в ViewModels (управление)

                ConsoleMessages.OutputDateFilmScreening(DatesOfFilmScreenings); // это должно быть в ViewModels

                DateOnly certainDataFilmSreening = ChoiseDateFilmScreening();
                FindFilmScreeningByDate(certainDataFilmSreening);
                flagChooseDate = !ConsoleMessages.PoolYesOrNo("Оставить выбранную дату"); // должно быть в ViewModels
            }
        }
        private void FindDatesFilmScreenings()
        {
            foreach (FilmScreening filmScreening in FilmScreenings)
            {
                if (!IsDatesRepeating(filmScreening))
                {
                    DatesOfFilmScreenings.Add(filmScreening.Date);
                }
            }
        }
        public bool IsDatesRepeating(FilmScreening filmScreening)
        {
            foreach (DateOnly date in DatesOfFilmScreenings)
            {
                return FilmScreening.IsDatesEqual(date, filmScreening); // не понятно, можно ли как то так сделать,
                                                                        // чтоб мы могли не передавать параметры,
                                                                        // а вызывать этот метод у объекта класса,
                                                                        // и обращаться к его полям
            }
            return false;
        }
        public DateOnly ChoiseDateFilmScreening()
        {
            return ConsoleMessages.ChooseEl(DatesOfFilmScreenings);
        }
        public void FindFilmScreeningByDate(DateOnly datesFilmScreenings)
        {
            List<FilmScreening> filmScreeningsTemp = new ();

            for (int i = 0; i < FilmScreenings.Count; i++)
            {
                if (datesFilmScreenings == FilmScreenings[i].Date)
                {
                    filmScreeningsTemp.Add(FilmScreenings[i]);
                }
            }
            FilmScreenings = filmScreeningsTemp;
        }

        public FilmScreening ChooseFilmScreeningsInCertainTime()
        {
            FilmScreening filmScreeningInCertainTime;
            bool flagChooseTime;
            do
            {
                ConsoleMessages.OutputTimeFilmScreening(FilmScreenings); // должно быть во ViewModels
                filmScreeningInCertainTime = ChoiseFilmScreeningByTime();
                flagChooseTime = !ConsoleMessages.PoolYesOrNo("Оставить выбранное время"); // должно быть во ViewModels
            } while (flagChooseTime);
            return filmScreeningInCertainTime;
        }
        public FilmScreening ChoiseFilmScreeningByTime()
        {
            return ConsoleMessages.ChooseEl(FilmScreenings);
        }
    }
}
