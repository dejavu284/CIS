namespace CinemaModel
{
    public class Cinema
    {
        public Cinema(string name, Address address,float rating, int id, Schedule schedule, List<Hall> halls, Poster poster)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Имя кинотеатра должно быть не пустым");
            else if (id < 0) throw new ArgumentException("id не может быть меньше нуля");
            else if (rating < 0) throw new ArgumentException("рейтинг не может быть меньше нуля");
            else if (!СheckCorrespondenceBetweenSeatingAndHalls(schedule.Shows,halls)) throw new ArgumentException("места в рассадке не соотносятся с местами в залах");
            else
            {
                Name = name;
                Address = address;
                Id = id;
                Schedule = schedule;
                Halls = halls;
                Poster = poster;
                Rating = rating;
            }
        }

        public string Name { get; }
        public Address Address { get; }
        public int Id { get; }
        public float Rating { get; }
        public Schedule Schedule { get; }
        public List<Hall> Halls { get; }
        public Poster Poster { get; }

        public void BookingPlace(int idShow, Place place)
        {
            Schedule.BookingPlace(idShow, place);
        }
        private bool СheckCorrespondenceBetweenSeatingAndHalls (List<Show> shows, List<Hall> halls)
        {
            for (int i = 0; i < shows.Count; i++)
            {
                Hall? hall = halls.FirstOrDefault(x => x.Id == shows[i].Seating.IdHall);
                if (hall == null || !CorrectSeatingAngHall(shows[i].Seating, hall))
                    return false;
            }
            return true;
        }
        private bool CorrectSeatingAngHall(Seating seating, Hall hall)
        {
            if(seating.Places.Length != hall.CountRows)
                return false;
            for (int i = 0; i < seating.Places.Length; i++)
            {
                if (seating.Places[i].Length != hall.Layout[i].Length)
                    return false;
            }    
            return true;
        }
    }
}
