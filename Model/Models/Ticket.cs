namespace Model.Models
{
    public class Ticket
    {
        public Ticket(int idCinema, Show show, Place place)
        {
            Show = show;
            IdCinema = idCinema;

            Place = place;
        }
        public string Name { get { return Show.Name; }}
        public DateOnly Date { get { return Show.Date; } }
        public TimeOnly Time { get { return Show.Time; } }
        public Place Place { get; }
        public int Cod { get { return CreateRandomCod(); } }

        private Show  Show{ get; }
        
        public int IdCinema { get; }    
        public int IdShow { get { return Show.Id; } }

        private static int CreateRandomCod() // хорошо или плохо?
        {
            Random random = new();
            int res = random.Next(1000, 9999);
            return res;
        }
    }

}
