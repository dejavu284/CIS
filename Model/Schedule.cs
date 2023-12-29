namespace CinemaModel
{
    public class Schedule
    {
        public Schedule(List<Show> shows)
        {
            Shows = shows;
        }
        public List<DateOnly> Dates { get { return FindDates(Shows); } }

        public List<Show> Shows { get; private set; } = new();

        public int Count { get { return Shows.Count; } }


        public Schedule FindByName(string filmName)
        {
            List<Show> shows = new();
            foreach (Show show in Shows)
            {
                if (filmName == show.Name)
                {
                    shows.Add(show);
                }
            }
            return new(shows);
        }
        public bool IsEmpty()
        {
            return Count == 0;
        }
        private List<DateOnly> FindDates(List<Show> shows)
        {
            List<DateOnly> dates = new List<DateOnly>();
            foreach (Show show in shows)
            {
                if (!IsDatesRepeating(show.Date, dates))
                {
                    dates.Add(show.Date);
                }
            }
            return dates;
        }
        private bool IsDatesRepeating(DateOnly date, List<DateOnly> dates)
        {
            foreach (DateOnly thisDate in dates)
                if (thisDate == date)
                    return true;
            return false;
        }
        public Schedule FindByDate(DateOnly datesShows)
        {
            List<Show> showsTemp = new();
            for (int i = 0; i < Shows.Count; i++)
            {
                if (datesShows == Shows[i].Date)
                {
                    showsTemp.Add(Shows[i]);
                }
            }
            return new(showsTemp);
        }

        private int FindShowIndexById(int id)
        {
            int index = -1;
            for (int i = 0; i < Shows.Count; i++)
            {
                if (Shows[i].Id == id)
                {
                    return i;
                }
            }
            return index;
        }

        public void BookingPlace(int idShow, Place place)
        {
            int indexShow = FindShowIndexById(idShow);
            Shows[indexShow].BookingPlaces(place);
        }
        
    }
}
