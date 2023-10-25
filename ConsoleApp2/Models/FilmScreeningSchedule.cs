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
        public List<DateOnly> DatesOfFilmScreenings { get; set; } = new();          // я напихал везде эти
                                                                                    // private set и не уверен что это вообще нужно
        public List<FilmScreening> FilmScreenings { get; private set; } = new();
        public FilmScreeningSchedule() { } // Странно, что у нас есть пустые конструкторы классов
        public FilmScreeningSchedule(List<FilmScreening> filmScreenings) // 0 ссылок на конструктор
        {
            FilmScreenings = filmScreenings;
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
        public bool IsFilmScreeningsNotNull()
        {
            return FilmScreenings.Count != 0;
        }
        public void FindDatesFilmScreenings()
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
                return filmScreening.IsDatesEqual(date);                // не понятно, можно ли как то так сделать,
                                                                        // чтоб мы могли dне передавать параметры,
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
        public FilmScreening ChoiseFilmScreeningByTime()
        {
            return ConsoleMessages.ChooseEl(FilmScreenings);
        }
    }
}
