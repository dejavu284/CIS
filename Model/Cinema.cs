namespace CinemaModel
{
    public class Cinema
    {
        public Cinema(string name, string address,float rating, int id, Schedule schedule, List<Hall> halls, Poster poster)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Имя кинотеатра должно быть не пустым");
            else if (id < 0) throw new ArgumentException("id не может быть меньше нуля");
            else if (string.IsNullOrEmpty(address)) throw new ArgumentException("адрес кинотеатра должен быть не пустым");
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
        public string Address { get; }
        public int Id { get; }
        public float Rating { get; }
        public Schedule Schedule { get; }
        public List<Hall> Halls { get; }
        public Poster Poster { get; }

        public void BookingPlace(int idShow, Place place)
        {
            Schedule.BookingPlace(idShow, place);
        }
    }
}
