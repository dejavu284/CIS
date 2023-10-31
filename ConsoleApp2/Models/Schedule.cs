
namespace CIS.Models
{
    internal class Schedule
    {
        public Schedule(List<FilmScreening> filmScreenings) 
        {
            FilmScreenings = filmScreenings;
        }
        public List<DateOnly> Dates { get { return FindDates(FilmScreenings); }}

        public List<FilmScreening> FilmScreenings { get; private set; } = new();

        public int Count { get { return FilmScreenings.Count; } }

        
        public Schedule FindByName(string filmName)
        {
            List<FilmScreening> filmScreenings = new();
            foreach (FilmScreening filmScreening in FilmScreenings)
            {
                if (filmName == filmScreening.Name)
                {
                    filmScreenings.Add(filmScreening);
                }
            }
            return new(filmScreenings);
        }
        public bool IsNull()
        {
            return Count == 0;
        }
        private List<DateOnly> FindDates(List<FilmScreening> filmScreenings)
        {
            List<DateOnly> dates = new List<DateOnly>();
            foreach (FilmScreening filmScreening in filmScreenings)
            {
                if (!IsDatesRepeating(filmScreening.Date, dates))
                {
                    dates.Add(filmScreening.Date);
                }
            }
            return dates;
        }
        private bool IsDatesRepeating(DateOnly date,List<DateOnly> dates)
        {
            foreach (DateOnly thisDate in dates)
                if (thisDate == date)
                    return true;       
            return false;
        }
        public Schedule FindFilmScreeningByDate(DateOnly datesFilmScreenings)
        {
            List<FilmScreening> filmScreeningsTemp = new ();
            for (int i = 0; i < FilmScreenings.Count; i++)
            {
                if (datesFilmScreenings == FilmScreenings[i].Date)
                {
                    filmScreeningsTemp.Add(FilmScreenings[i]);
                }
            }
            return new(filmScreeningsTemp);
        }
    }
}
