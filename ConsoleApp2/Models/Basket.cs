

namespace CIS.Models
{
    internal class Basket 
    {
        public List<Ticket> Tickets { get; private set; } = new List<Ticket>();
        public int NumberTickets { get; private set; } = 0;
        public int Price { get; private set; } = 0;
        public void AddTicket(Show show, List<Place> places)
        {
            foreach (var place in places)
            {
                Tickets.Add(new Ticket(show.Name, show.Date, show.Time, place));
                Price += place.Price;
                NumberTickets++;
            }
        }
    }
}
