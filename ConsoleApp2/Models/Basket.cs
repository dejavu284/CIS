

namespace CIS.Models
{
    internal class Basket 
    {
        public List<Ticket> Tickets { get; private set; } = new List<Ticket>();
        public int NumberTickets { get; private set; } = 0;
        public int Price { get; private set; } = 0;
        public void AddTicket(FilmScreening filmScreening)
        {
            Tickets.Add(new Ticket(filmScreening.Name, filmScreening.Date, filmScreening.Time, filmScreening.Price));
            Price += filmScreening.Price;
            NumberTickets++;
        }
    }
}
