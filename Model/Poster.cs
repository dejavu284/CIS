namespace CinemaModel
{
    public class Poster
    {
        public Poster(List<Film> films)
        {
            Films = films;
        }
        public List<Film> Films { get; }
        public int Count { get { return Films.Count; } }
    }
}
