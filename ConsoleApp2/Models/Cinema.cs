
namespace CIS.Models
{
    internal class Cinema
    {
        public Cinema(string name, string address, int id, Schedule schedule, List<Hall> halls, Poster poster)
        {
            Name = name;
            Address = address;
            Id = id;
            Schedule = schedule;
            Halls = halls;
            Poster = poster;
        }

        public string Name { get; }
        public string Address { get; }
        public int Id { get; } 
        public Schedule Schedule { get;}
        public List<Hall> Halls { get; }
        public Poster Poster { get;}

        public void BookingPlace(int idShow,Place place)
        {
            Schedule.Shows[Schedule.FindShowIndexById(idShow)].Seating.BookingPlace(place);
        }
    }
}
